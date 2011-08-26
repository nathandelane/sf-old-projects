package net.phyer.games;

import java.awt.Graphics;
import java.awt.Graphics2D;

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

  /**
   * Paints the window.
   */
  @Override
  public void paint(final Graphics graphics) {
    super.paint(graphics);

    if (gameLoop != null) {
      final Graphics2D graphics2D = (Graphics2D)graphics;

      gameLoop.run(graphics2D);
    }
  }

}
