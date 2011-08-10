package net.phyer.server.configuration;

import java.io.IOException;

public final class TeamChatConfiguration {

    private static final String LISTENING_PORT_PROPERTY_NAME = "listeningPort";

    private static TeamChatConfiguration instance;

    private final Configuration config;

    private TeamChatConfiguration() throws IOException {
	this.config = Configuration.get();
    }

    /**
     * Gets the listening port from the configured file.
     * @return
     * @throws NumberFormatException
     * @throws IOException
     */
    public static int listeningPort() throws NumberFormatException, IOException {
	return Integer.parseInt((String)TeamChatConfiguration.get().config.getProperty(TeamChatConfiguration.LISTENING_PORT_PROPERTY_NAME));
    }

    private static TeamChatConfiguration get() throws IOException {
	if (TeamChatConfiguration.instance == null) {
	    TeamChatConfiguration.instance = new TeamChatConfiguration();
	}

	return TeamChatConfiguration.instance;
    }

}
