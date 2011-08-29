package net.phyer.games;

import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Graphics2D;

import javax.swing.JFrame;

import net.phyer.games.graphics.SpriteManager;

/**
 * This class represents a concrete game window.
 * @author nathanlane
 *
 */
public final class GameWindow extends JFrame {

  private static final long serialVersionUID = -7527056665085333128L;

  private final SpriteManager spriteManager;

  private GameLoop gameLoop;

  /**
   * Creates an instance of GameWindow.
   */
  public GameWindow(final Dimension windowDimensions, final GameLoop gameLoop) {
    this.gameLoop = gameLoop;
    spriteManager = new SpriteManager(windowDimensions);

    setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    setSize(windowDimensions);
  }

  /**
   * Sets the title of the {@link GameWindow}.
   * @param title
   */
  public void setTitle(final String title) {
    super.setTitle(title);
  }

  public void setGameLoop(final GameLoop gameLoop) {
    this.gameLoop = gameLoop;
  }

  /**
   * Paints the window.
   */
  @Override
  public void paint(final Graphics graphics) {
    super.paint(graphics);

    if (gameLoop != null) {
      final Graphics2D graphics2D = (Graphics2D)graphics;

      gameLoop.run(graphics2D, spriteManager);
    }
  }

}
