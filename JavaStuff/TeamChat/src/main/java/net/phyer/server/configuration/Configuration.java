package net.phyer.server.configuration;

import java.io.IOException;
import java.util.Properties;

public final class Configuration {

    private static final String configurationFileName = "TeamChat.xml";

    private static Configuration instance;

    private final Properties properties;

    private Configuration() throws IOException {
	properties = new Properties();
	properties.loadFromXML(ClassLoader.getSystemResourceAsStream(Configuration.configurationFileName));
    }

    public Object getProperty(String name) {
	Object property = null;

	if (this.properties.containsKey(name)) {
	    property = this.properties.get(name);
	}

	return property;
    }

    public static Configuration get() throws IOException {
	if (Configuration.instance == null) {
	    Configuration.instance = new Configuration();
	}

	return Configuration.instance;
    }

}
