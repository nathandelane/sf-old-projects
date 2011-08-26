package net.phyer.games;

import java.awt.Graphics2D;

/**
 * Represents a threadless game loop.
 * @author nathanlane
 *
 */
public interface GameLoop {

  void run(final Graphics2D graphics2D);

}
