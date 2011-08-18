package net.phyer.systems.messaging.teamchat;

import java.util.HashMap;
import java.util.Map;

public final class Arguments {

  private static Arguments instance;

  private final Map<String, Object> arguments;

  private Arguments(final String[] args) {
    this.arguments = new HashMap<String, Object>();

    parseArgumentsIntoMap(args);
  }

  @SuppressWarnings("unchecked")
  public <T> T getAs(Class<T> clazz, String name) {
    T returnValue = null;

    if (arguments.containsKey(name)) {
      returnValue = (T)arguments.get(name);
    }

    return returnValue;
  }

  private void parseArgumentsIntoMap(final String[] args) {
    for (String nextArg : args) {

    }
  }

  public static Arguments parse(final String[] args) {
    Arguments.instance = new Arguments(args);

    return Arguments.instance;
  }

}
