package net.phyer.games.barricade;

import java.awt.Dimension;
import java.awt.Graphics2D;
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

  /**
   * @param args
   */
  public static void main(String[] args) {
    final Runnable launchGame = new Runnable() {

      public void run() {
        final GameWindow gameWindow = new GameWindow(WINDOW_DIMENSIONS, null);
        gameWindow.setTitle("Phyersoft-BARRICADE Press F11 for window or fullscreen");
        gameWindow.setVisible(true);

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

}
