package net.phyer.games.barricade;

import java.awt.Dimension;

import javax.swing.SwingUtilities;

import net.phyer.games.GameWindow;

/**
 * Main entry point for Barricade.
 * @author nathanlane
 *
 */
public final class Main {

  private static final Dimension WINDOW_DIMENSIONS = new Dimension(640, 480);

  /**
   * @param args
   */
  public static void main(String[] args) {
    final Runnable launchGame = new Runnable() {

      public void run() {
        final GameWindow gameWindow = new GameWindow(WINDOW_DIMENSIONS, null);
        gameWindow.setTitle("Phyersoft-BARRICADE Press F11 for window or fullscreen");
        gameWindow.setVisible(true);
      }

    };

    SwingUtilities.invokeLater(launchGame);
  }

}
