package net.phyer.games.barricade.events;

import java.awt.Window;
import java.awt.event.KeyListener;

/**
 *
 * @author nathanlane
 *
 */
public abstract class AbstractKeyboardHandler implements EventHandler, KeyListener {

  protected AbstractKeyboardHandler() { }

  /**
   * {@inheritDoc}
   */
  public void addHandlerToWindow(final Window window) {
    window.addKeyListener(this);
  }

  /**
   * {@inheritDoc}
   */
  public void removeHandlerFromWindow(final Window window) {
    window.removeKeyListener(this);
  }

}
