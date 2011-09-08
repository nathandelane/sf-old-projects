package net.phyer.games;

import java.awt.DisplayMode;
import java.awt.GraphicsDevice;
import java.awt.GraphicsEnvironment;
import java.awt.Window;

/**
 * Manages the window whether it is in full-screen-exclusive mode or windowed.
 * @author nathanlane
 *
 */
public final class FullScreenManager {

  private static FullScreenManager instance;

  private final GraphicsEnvironment graphicsEnvironment;
  private final GraphicsDevice graphicsDevice;
  private final DisplayMode defaultDisplayMode;

  private FullScreenManager() {
    graphicsEnvironment = GraphicsEnvironment.getLocalGraphicsEnvironment();
    graphicsDevice = graphicsEnvironment.getDefaultScreenDevice();
    defaultDisplayMode = graphicsDevice.getDisplayMode();
  }

  public synchronized boolean isFullScreenSupported() {
    return graphicsDevice.isFullScreenSupported();
  }

  public synchronized DisplayMode[] getAvailableDisplayModes() {
    return graphicsDevice.getDisplayModes();
  }

  public synchronized void setFullScreenExclusive(final Window window, final DisplayMode displayMode) {
    graphicsDevice.setFullScreenWindow(window);
    graphicsDevice.setDisplayMode(displayMode);
  }

  public synchronized void setDefaultDisplayMode() {
    graphicsDevice.setDisplayMode(defaultDisplayMode);
  }

  public static FullScreenManager getInstance() {
    if (FullScreenManager.instance == null) {
      FullScreenManager.instance = new FullScreenManager();
    }

    return FullScreenManager.instance;
  }

}
