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

/**
 * This class represents the main MUD server controller.
 * @author lanathan
 *
 */
public class MudServer implements Runnable {
	
	private static ServerDescriptor serverDescriptor;
	
	private ServerSocket serverSocket;
	private Socket clientBinding;
	private Logger logger;
	
	/**
	 * Constructor to initialize the server.
	 */
	private MudServer(ServerDescriptor serverDescriptor) {
		if (MudServer.serverDescriptor == null) {
			MudServer.serverDescriptor = serverDescriptor;
		}
		
		this.logger = Logger.getInstance();
	}
	
	/**
	 * @see java.lang.Runnable
	 */
	public void run() {
		try {
			this.serverSocket = new ServerSocket(MudServer.serverDescriptor.port);
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * Server entry point
	 * @param String[] args
	 */
	public static void main(String[] args) {
		
	}

}
