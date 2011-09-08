package net.phyer.games.barricade;

import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.Point;
import java.awt.Toolkit;
import java.net.URL;

import javax.swing.ImageIcon;
import javax.swing.SwingUtilities;

import net.phyer.games.GameLoop;
import net.phyer.games.GameWindow;
import net.phyer.games.graphics.SpriteManager;

/**
 * Main entry point for Barricade.
 * @author nathanlane
 *
 */
public final class Main {

  private static final Dimension WINDOW_DIMENSIONS = new Dimension(740, 480);
  private static final String WINDOW_TITLE_BAR_MESSAGE = "Phyersoft-BARRICADE Press F11 for window or fullscreen";

  /**
   * @param args
   */
  public static void main(String[] args) {
    final Runnable launchGame = new Runnable() {

      public void run() {
        final GameWindow gameWindow = new GameWindow(WINDOW_DIMENSIONS, null);
        gameWindow.setTitle(WINDOW_TITLE_BAR_MESSAGE);
        gameWindow.setCursor(getInvisibleCursor());
        gameWindow.setVisible(true);
        gameWindow.showDebugPanel();

        URL url = ClassLoader.getSystemResource("backgrounds/Barr-Hiway77Misty.png");

        if (url != null) {
          final ImageIcon background = new ImageIcon(url);

          final GameLoop gameLoop = new GameLoop() {

            public void run(Graphics2D graphics2d, SpriteManager spriteManager) {
              graphics2d.drawImage(background.getImage(), 0, 0, null);
            }

          };

          gameWindow.setGameLoop(gameLoop);
        }
      }

    };

    SwingUtilities.invokeLater(launchGame);
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
