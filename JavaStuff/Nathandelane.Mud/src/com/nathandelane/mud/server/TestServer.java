/**
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

package com.nathandelane.mud.server;

import java.io.*;
import java.net.*;
import java.util.*;

public class TestServer {
	
	protected final static int port = 19999;
	
	private static ServerSocket serverSocket;
	private static Socket connection;
	private static boolean isFirstClient;
	private static StringBuffer process;
	private static String timeStamp;
	
	public static void main(String[] args) {
		try {
			serverSocket = new ServerSocket(TestServer.port);
			
			System.out.println("Mud Server initialized...");
			
			int nextCharacter;
			
			while (true) {
				connection = serverSocket.accept();
				
				BufferedInputStream inputStream = new BufferedInputStream(connection.getInputStream());
				InputStreamReader inputReader = new InputStreamReader(inputStream);
				
				process = new StringBuffer();
				
				while ((nextCharacter = inputReader.read()) != 13) {
					process.append((char)nextCharacter);
				}
				
				System.out.println(process);
				
				try {
					Thread.sleep(10000);
				} catch(Exception e) {
					e.printStackTrace();
				}
				
				timeStamp = (new Date()).toString();
				
				String returnCode = "Mud Server responded at " + timeStamp + "\n";
				BufferedOutputStream outputStream = new BufferedOutputStream(connection.getOutputStream());
				OutputStreamWriter outputWriter = new OutputStreamWriter(outputStream, "US-ASCII");
				
				outputWriter.write(returnCode);
				outputWriter.flush();
				
				connection.close();
			}
		} catch (IOException ioe) {
			ioe.printStackTrace();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
