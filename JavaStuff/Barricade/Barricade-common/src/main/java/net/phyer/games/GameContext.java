package net.phyer.games;

import java.util.HashMap;
import java.util.Map;

/**
 * Represents contextual data required by the game.
 * @author nathanlane
 *
 */
public class GameContext {

  private static final GameContext instance = new GameContext();

  private final Map<String, Object> context;

  private GameContext() {
    context = new HashMap<String, Object>();
  }

  public void putObject(final String name, final Object object) {
    context.put(name, object);
  }

  public Object getObject(final String name) {
    return context.get(name);
  }

  public static GameContext getContext() {
    return instance;
  }

}
