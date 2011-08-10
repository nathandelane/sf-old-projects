package net.phyer.server;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;

public class CapitalizationReflector implements Runnable {

    private Socket connection;

    public CapitalizationReflector(Socket connection) {
	System.out.println(String.format("Added connection for %1$s", connection.getInetAddress().getHostAddress()));

	this.connection = connection;
    }

    public void run() {
	while (true) {
	    BufferedReader inToServer = null;
	    DataOutputStream outToClient = null;

	    try {
		System.out.println("Waiting...");

		if (inToServer == null && outToClient == null) {
		    inToServer = new BufferedReader(new InputStreamReader(connection.getInputStream()));
		    outToClient = new DataOutputStream(connection.getOutputStream());
		}

		String clientSentence = inToServer.readLine();

		if (clientSentence.trim().equals("quit")) {
		    close();

		    break;
		}

		outToClient.writeBytes(String.format("%1$s\n\n", clientSentence.toUpperCase()));
	    } catch (IOException e) {
		e.printStackTrace();
	    }
	}
    }

    public void close() throws IOException {
	System.out.println("Closing connection.");

	connection.close();
    }

}
