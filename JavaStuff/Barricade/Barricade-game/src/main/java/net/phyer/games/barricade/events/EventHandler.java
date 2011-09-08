package net.phyer.games.barricade.events;

import java.awt.Window;

/**
 * Represents an event handler for a specific device or set of devices.
 * @author nathanlane
 *
 */
public interface EventHandler {

  /**
   * Adds this EventHandler to a window.
   * @param window
   */
  void addHandlerToWindow(final Window window);

  /**
   * Removes this EventHandler from a window.
   * @param window
   */
  void removeHandlerFromWindow(final Window window);

}
