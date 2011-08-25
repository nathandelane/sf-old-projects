package net.phyer.games;

import javax.swing.JFrame;

/**
 * This class represents a concrete game window.
 * @author nathanlane
 *
 */
public final class GameWindow extends JFrame {

  private static final long serialVersionUID = -7527056665085333128L;

  private static GameWindow instance;

  /**
   * Creates an instance of GameWindow.
   */
  private GameWindow() {

  }

  /**
   * Gets a singleton instance of the {@link GameWindow}.
   * @return
   */
  public static GameWindow getInstance() {
    if (GameWindow.instance == null) {
      GameWindow.instance = new GameWindow();
    }

    return GameWindow.instance;
  }

}
