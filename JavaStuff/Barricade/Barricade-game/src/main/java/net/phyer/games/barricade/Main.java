package net.phyer.games.barricade;

import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.Image;
import java.awt.Point;
import java.awt.Toolkit;

import javax.swing.SwingUtilities;

import net.phyer.games.GameWindow;
import net.phyer.games.barricade.state.GameLoopFactory;
import net.phyer.games.barricade.state.GameState;

/**
 * Main entry point for Barricade.
 * @author nathanlane
 *
 */
public final class Main {

  private static final Dimension WINDOW_DIMENSIONS = new Dimension(740, 480);
  private static final String WINDOW_TITLE_BAR_MESSAGE = "Phyersoft-BARRICADE Press F11 for window or fullscreen";

  private final GameWindow gameWindow;

  private Main() {
    gameWindow = new GameWindow(WINDOW_DIMENSIONS, null);

    final Runnable launchGame = new Runnable() {

      public void run() {
        gameWindow.setTitle(WINDOW_TITLE_BAR_MESSAGE);
        gameWindow.setCursor(getInvisibleCursor());
        gameWindow.setGameLoop(GameLoopFactory.generateGameLoop(GameState.HALLWAY_ORANGE));
        gameWindow.setVisible(true);
      }

    };

    SwingUtilities.invokeLater(launchGame);
  }

  /**
   * @param args
   */
  public static void main(String[] args) {
    new Main();
  }

  /**
   * Gets an invisible cursor.
   * @return
   */
  private static Cursor getInvisibleCursor() {
    final Image cursorImage = Toolkit.getDefaultToolkit().createImage(new byte[] { });
    final Cursor cursor = Toolkit.getDefaultToolkit().createCustomCursor(cursorImage, new Point(0, 0), "Invisible Cursor");

    return cursor;
  }

}
