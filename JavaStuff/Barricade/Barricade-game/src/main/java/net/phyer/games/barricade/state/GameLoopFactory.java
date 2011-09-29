package net.phyer.games.barricade.state;

import java.util.HashMap;
import java.util.Map;

import net.phyer.games.GameLoop;

/**
 * Gets a concrete {@link GameLoop} for the game's current state.
 * @author nathanlane
 *
 */
public final class GameLoopFactory {

//  private static final Dimension WINDOW_DIMENSIONS = new Dimension(740, 480);
  /**
   * GameLoop dictionary based on the game state.
   */
  private static final Map<GameState, GameLoop> gameLoopDictionary = new HashMap<GameState, GameLoop>() {

    private static final long serialVersionUID = 1L;

    {
      // HallwayOrange
      put(GameState.HALLWAY_ORANGE, new DefaultGamePlayGameLoop(GameState.HALLWAY_ORANGE));

      // MistyHighway
      put(GameState.MISTY_HIGHWAY, new DefaultGamePlayGameLoop(GameState.MISTY_HIGHWAY));
    }
  };

  public static GameLoop generateGameLoop(final GameState gameState) {
    return gameLoopDictionary.get(gameState);
  }

}
