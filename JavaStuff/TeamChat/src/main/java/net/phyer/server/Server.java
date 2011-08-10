package net.phyer.server;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.HashMap;
import java.util.Map;

import net.phyer.server.configuration.TeamChatConfiguration;

public final class Server {

    private static ServerSocket serverSocket;

    private final Map<InetAddress, Thread> connectionPool;

    private ServerStatus serverStatus;

    public Server() throws NumberFormatException, IOException {
	Server.serverSocket = new ServerSocket(TeamChatConfiguration.listeningPort());

	System.out.println(String.format("Listening on port %1$s...", TeamChatConfiguration.listeningPort()));

	this.connectionPool = new HashMap<InetAddress, Thread>();
	this.serverStatus = ServerStatus.STARTING;
    }

    /**
     * Sets the status of the server.
     * @param serverStatus
     */
    public void setStatus(ServerStatus serverStatus) {
	this.serverStatus = serverStatus;
    }

    public void run() {
	this.serverStatus = ServerStatus.RUNNING;

	while (this.serverStatus != ServerStatus.STOPPING) {
	    try {
		final Socket connection = Server.serverSocket.accept();

		CapitalizationReflector newReflector = new CapitalizationReflector(connection);

		Thread thread = new Thread(newReflector);
		thread.start();

		this.connectionPool.put(connection.getInetAddress(), thread);
	    } catch (IOException e) {
		e.printStackTrace();
	    }
	}

	for (InetAddress nextReflector : connectionPool.keySet()) {
	    System.out.println(String.format("Purging connection for %1$s...", nextReflector.getHostAddress()));

	    this.connectionPool.get(nextReflector).isInterrupted();
	    this.connectionPool.remove(nextReflector);
	}

	this.serverStatus = ServerStatus.STOPPED;
    }

}
