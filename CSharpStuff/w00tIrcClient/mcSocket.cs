using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Obsidian
{
	/// <summary>
	/// Wraps the network I/O thread(s).
	/// </summary>
	public class NetworkThread : IDisposable
	{
		/// <summary>
		/// The maximum number of sockets controlled by any one thread.
		/// </summary>
		public const byte MAX_SOCKETS_PER_THREAD = 5;
		public class BufferedSocket 
		{
			/// <summary>
			/// The Socket itself. This should never really be manipulated directly except within the
			/// NetworkThread.
			/// </summary>
			public Socket sck;
			/// <summary>
			/// The send buffer. This data will be sent to the remote endpoint as soon as possible.
			/// </summary>
			public string sendbuffer = "";
			/// <summary>
			/// Data that has been received on this socket. Read from here, not from the socket.
			/// </summary>
			public string recvbuffer = "";
			/// <summary>
			/// If a socket error ever occurs, this is set to a reference to the Exception that occured.
			/// Very likely the Socket itself will be invalid when this is not null. Used to report back
			/// DNS errors, connection errors, and so forth.
			/// </summary>
			public Exception sckError;
			public BufferedSocket(Socket sck) { this.sck = sck; }
			private bool mPendingDisconn = false;
			public bool PendingDisconnect 
			{
				get 
				{
					return mPendingDisconn;
				}
			}
			public void Disconnect() 
			{
				mPendingDisconn = true;
			}
			public void SendData(string data) 
			{
				lock(this) 
				{
					this.sendbuffer += data;
				}
			}
			public int Available() 
			{
				lock(this) 
				{
					return this.recvbuffer.Length;
				}
			}
			public string GetData() 
			{
				string tmp;
				lock(this) 
				{
					if (this.recvbuffer.Length < 1) 
					{
						Monitor.Wait(this);
					}
					tmp = this.recvbuffer;
					this.recvbuffer = "";
				}
				return tmp;
			}
			public string GetData(int len) 
			{
				string tmp;
				lock(this) 
				{
					if (this.recvbuffer.Length < len) 
					{
						Monitor.Wait(this);
					}
					if (len <= this.recvbuffer.Length) 
					{
						tmp = this.recvbuffer;
						this.recvbuffer = "";
					}
					else 
					{
						tmp = this.recvbuffer.Substring(0, len);
						this.recvbuffer = this.recvbuffer.Substring(len);
					}
				}
				return tmp;
			}
			public string GetData(char[] Delimiter) 
			{
				string[] parts;
				lock(this) 
				{
					if (this.recvbuffer.IndexOfAny(Delimiter) < 0) 
					{
						Monitor.Wait(this);
					}
					parts = this.recvbuffer.Split(Delimiter, 2);
					this.recvbuffer = parts[1];
				}
				return parts[0];
			}
		}
		/// <summary>
		/// Represents a function to be called when a connection attempt succeeds for fails.
		/// This function runs within the network thread - so it MUST NOT BLOCK FOR ANY REASON.
		/// Ideally this will fork a new thread and do its work there.
		/// </summary>
		public delegate void ConnectCallback(BufferedSocket sck);
		private class ConnQueueItem 
		{
			public string address;
			public int port;
			public ConnectCallback cb;
			public ConnQueueItem(string address, int port, ConnectCallback cb) 
			{
				this.address = address;
				this.port = port;
				this.cb = cb;
			}
		}
		// sockets.Count + connectionqueue.Count MUST NOT exceed MAX_SOCKETS_PER_THREAD
		private List<BufferedSocket> sockets;
		private Queue<ConnQueueItem> connectionqueue;
		private Thread thread;
		/// <summary>
		/// Creates a new NetworkThread instance.
		/// </summary>
		public NetworkThread() 
		{
			// Set the ArrayList initial capacity to MAX_SOCKETS_PER_THREAD.
			sockets = new List<BufferedSocket>(MAX_SOCKETS_PER_THREAD);
			// Same here.
			connectionqueue = new Queue<ConnQueueItem>(MAX_SOCKETS_PER_THREAD);
			// The thread is only active when we actually have a socket to monitor.
		}
		/// <summary>
		/// Creates a new NetworkThread instance and starts a socket connecting to the given address and port.
		/// </summary>
		/// <param name="address">The address to which the initial socket will connect. This must be either a valid DNS hostname or a valid IP address.</param>
		/// <param name="port">The remote port to which the socket will connect.</param>
		/// <param name="cb">A function to be called when the connection succeeds or fails.</param>
		public NetworkThread(string address, int port, ConnectCallback cb) : this()
		{
			AddSocket(address, port, cb);
		}

		public void Dispose()
		{
			// Dispose is supposed to never throw, so we have to decide what to do
			// if they Dispose() while sockets are open. The sockets could still be usable
			// so what we'll do is if they haven't got any references to the sockets,
			// tough s*** :P .
			if (thread == null || (thread.ThreadState & ThreadState.Unstarted) != 0 || (thread.ThreadState & ThreadState.Stopped) != 0) 
			{
				// Don't bother.
				sockets = null;
				connectionqueue = null;
				return;
			}
			else if ((thread.ThreadState & ThreadState.WaitSleepJoin) != 0) 
			{
				thread.Interrupt();
			}
			thread.Abort();
			thread.Join();
			sockets = null;
			connectionqueue = null;
			thread = null;
		}
		
		/// <summary>
		/// Reports the number of connection slots available in this thread.
		/// </summary>
		/// <returns>How many more sockets this thread can handle (effectively: MAX_SOCKETS_PER_THREAD - its current load).</returns>
		public int AvailableSlot() 
		{
			if (sockets == null) throw new ObjectDisposedException("NetworkThread");
			return MAX_SOCKETS_PER_THREAD - (sockets.Count + connectionqueue.Count);
		}

		/// <summary>
		/// Adds a new socket to this thread. Throws an exception if we already have too many sockets.
		/// </summary>
		/// <param name="address">Address to connect to.</param>
		/// <param name="port">Port to connect to.</param>
		/// <param name="cb">A function to be called when the connection succeeds or fails.</param>
		public void AddSocket(string address, int port, ConnectCallback cb) 
		{
			if (sockets == null) throw new ObjectDisposedException("NetworkThread");
			lock(this) 
			{
				if (AvailableSlot() < 1) throw new Exception("Out of socket slots in this thread.");
				connectionqueue.Enqueue(new ConnQueueItem(address, port, cb));
				// If the thread is not running, start it.
				if (thread == null || (thread.ThreadState & ThreadState.Unstarted) != 0) 
				{
					thread = new Thread(new ThreadStart(SocketThread));
					thread.IsBackground = true;
					thread.Start();
				}
					// Otherwise, if the thread is sleeping, wake it up.
				else if ((thread.ThreadState & ThreadState.WaitSleepJoin) != 0) 
				{
					thread.Interrupt();
				}
			}
		}

		/// <summary>
		/// Removes a socket from this thread. Only closed sockets can be removed. Call Disconnect on
		/// the socket (and wait for sck to be set to null) before attempting to remove it.
		/// </summary>
		/// <param name="sck">The closed socket to be removed.</param>
		public void RemoveSocket(BufferedSocket sck) 
		{
			if (sockets == null) throw new ObjectDisposedException("NetworkThread");
			lock(this) 
			{
				if (sck == null) 
				{
					throw new ArgumentNullException("sck");
				}
				if (sck.sck != null && sck.sck.Connected) 
				{
					throw new InvalidOperationException("Cannot remove an active socket. Disconnect the socket first.");
				}
				sockets.Remove(sck);
			}
		}

		private void Dns_Resolve(IAsyncResult ar) 
		{
			Random rnd = new Random();
			IPHostEntry hostent;
			ConnQueueItem nextconn = (ConnQueueItem)(ar.AsyncState);
			while (!ar.IsCompleted) ar.AsyncWaitHandle.WaitOne(10, false);
			try 
			{
				hostent = Dns.EndGetHostEntry(ar);
			} 
			catch (Exception e2) 
			{
				// DNS Failed.
				BufferedSocket sck = new BufferedSocket(null);
				sck.sckError = e2;
				nextconn.cb(sck);
				return;
			}
			nextconn.address = hostent.AddressList[rnd.Next(hostent.AddressList.Length)].ToString();
			lock (this) 
			{
				connectionqueue.Enqueue(nextconn);
			}
		}

		private void SocketThread() 
		{
			try 
			{
				// Loop "forever".
				while(true) 
				{
					// Even a tiny sleep like this goes a long way to keeping us from using 100% CPU.
					try { Thread.Sleep(10); } 
					catch (ThreadInterruptedException) {}
					lock(this) 
					{
						if ((sockets.Count + connectionqueue.Count) < 1) continue;
						// Process EXACTLY ONE connection attempt each time through. If we must DNS,
						// then do so and then put the attempt back on the end.
						if (connectionqueue.Count > 0) 
						{
							ConnQueueItem nextconn = connectionqueue.Dequeue();
							IPAddress ip;
							try 
							{
								ip = IPAddress.Parse(nextconn.address);
								// We have IP - CONNECT!
								BufferedSocket sck = new BufferedSocket(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
								try 
								{
									sck.sck.Connect(new IPEndPoint(ip, nextconn.port));
									// CONNECTED! Notify the callback.
									nextconn.cb(sck);
									sockets.Add(sck);
								} 
								catch (Exception e) 
								{
									// Connect Failed :( Notify the callback. The callback doesn't need to try to remove - in fact it MUST NOT! I don't know what a double-lock might entail ;) .
									sck.sck = null;
									sck.sckError = e;
									nextconn.cb(sck);
								}
							}
							catch (Exception) 
							{
								// We must DNS.
								try 
								{
									Dns.BeginGetHostEntry(nextconn.address, new AsyncCallback(Dns_Resolve), nextconn);
								} 
								catch (Exception e2) 
								{
									// DNS Failed.
									BufferedSocket sck = new BufferedSocket(null);
									sck.sckError = e2;
									nextconn.cb(sck);
								}
							}
						}
						// Now process Socket buffers.
						foreach (BufferedSocket sck in sockets) 
						{
							lock (sck) 
							{
								// We shouldn't send more than about 1KB at a time, to avoid blocking
								// in Send().
								// If we're pending disconnect...
								if (sck.sck == null) continue;
								if (sck.PendingDisconnect) 
								{
									if (sck.sck.Poll(0, SelectMode.SelectWrite) && sck.sendbuffer.Length > 0) sck.sck.Send(Encoding.ASCII.GetBytes(sck.sendbuffer));
									sck.sendbuffer = "";
									sck.sck.Shutdown(SocketShutdown.Both);
									sck.sck.Close();
									sck.sck = null;
									continue;
								}
								if (sck.sendbuffer.Length > 0) 
								{
									byte[] data;
									if (sck.sendbuffer.Length > 1024) 
									{
										data = Encoding.ASCII.GetBytes(sck.sendbuffer.Substring(0, 1024));
										sck.sendbuffer = sck.sendbuffer.Substring(1024);
									} 
									else 
									{
										data = Encoding.ASCII.GetBytes(sck.sendbuffer);
										sck.sendbuffer = "";
									}
									sck.sck.Send(data);
								}
								if (sck.sck.Poll(0, SelectMode.SelectRead) && sck.sck.Available <= 0) 
								{
									// Remote endpoint disconnected.
									sck.sendbuffer = "";
									if (sck.sck.Poll(0, SelectMode.SelectWrite)) sck.sck.Shutdown(SocketShutdown.Send);
									sck.sck.Close();
									sck.sck = null;
								}
								else if (sck.sck.Available > 0) 
								{
									byte[] data = new byte[sck.sck.Available];
									sck.sck.Receive(data);
									sck.recvbuffer += Encoding.ASCII.GetString(data);
								}
								// In case someone had called Monitor.Wait() on this sck, pulse it.
								Monitor.PulseAll(sck);
							}
						}
					}
				}
			}
			catch (ThreadAbortException) 
			{

			}
		}
	}
}
