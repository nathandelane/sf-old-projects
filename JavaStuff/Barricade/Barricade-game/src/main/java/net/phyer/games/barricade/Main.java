package net.phyer.games.barricade;

import javax.swing.SwingUtilities;

import net.phyer.games.GameWindow;

/**
 * Main entry point for Barricade.
 * @author nathanlane
 *
 */
public final class Main {

  /**
   * @param args
   */
  public static void main(String[] args) {
    final Runnable launchGame = new Runnable() {

      public void run() {
        final GameWindow gameWindow = new GameWindow(null);
        gameWindow.setTitle("Phyersoft-BARRICADE Press F11 for window or fullscreen");
        gameWindow.setVisible(true);
      }

    };

    SwingUtilities.invokeLater(launchGame);
  }

}
