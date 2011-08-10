package net.phyer.server;

import java.io.IOException;

public final class Main {

    /**
     * Main program entry point - obvious, na?
     * @param args
     * @throws IOException
     */
    public static void main(String[] args) throws IOException {
	Server server = new Server();
	server.run();
    }

}
