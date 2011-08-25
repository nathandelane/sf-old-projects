package net.phyer.games;

import java.awt.Graphics;

import javax.swing.JFrame;

/**
 * This class represents a concrete game window.
 * @author nathanlane
 *
 */
public final class GameWindow extends JFrame {

  private static final long serialVersionUID = -7527056665085333128L;

  private final GameLoop gameLoop;

  /**
   * Creates an instance of GameWindow.
   */
  public GameWindow(final GameLoop gameLoop) {
    this.gameLoop = gameLoop;

    setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    setSize(640, 480);
  }

  @Override
  public void update(final Graphics g) {

  }

}
