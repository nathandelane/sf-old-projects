package net.phyer.games.barricade.state;

import java.awt.Dimension;
import java.awt.Graphics2D;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

import javax.swing.ImageIcon;

import net.phyer.games.GameLoop;
import net.phyer.games.graphics.SpriteManager;

/**
 * Gets a concrete {@link GameLoop} for the game's current state.
 * @author nathanlane
 *
 */
public final class GameLoopFactory {

  private static final Dimension WINDOW_DIMENSIONS = new Dimension(740, 480);
  private static final Map<GameState, GameLoop> gameLoopDictionary = new HashMap<GameState, GameLoop>() {

    private static final long serialVersionUID = 1L;

    {
      put(GameState.Experiment, new AbstractGameLoop(GameState.Experiment) {

        private ImageIcon background;

        public void preload() {
          final String resourcePath = getGameState().getStateDescriptor().getBackground().getResourcePath();
          final URL url = ClassLoader.getSystemResource(resourcePath);

          background = new ImageIcon(url);
        }

        public void run(Graphics2D graphics2d, SpriteManager spriteManager) {
          graphics2d.drawImage(background.getImage(), 0, 0, null);
        }

      });
    }
  };

  public static GameLoop generateGameLoop(final GameState gameState) {
    return gameLoopDictionary.get(gameState);
  }

  /**
   * Represents an abstract game loop implementation to be used by the {@link GameLoopFactory}.
   * @author nathanlane
   *
   */
  private static abstract class AbstractGameLoop implements GameLoop {

    private final GameState gameState;

    protected AbstractGameLoop(final GameState gameState) {
      this.gameState = gameState;
    }

    protected GameState getGameState() {
      return gameState;
    }

    public abstract void run(Graphics2D graphics2d, SpriteManager spriteManager);

  }

}
