package net.phyer.games.barricade.state;

import java.awt.BufferCapabilities;
import java.awt.Graphics2D;
import java.awt.GraphicsConfiguration;
import java.net.URL;

import javax.swing.ImageIcon;

import net.phyer.games.GameLoop;
import net.phyer.games.graphics.SpriteManager;

public final class DefaultGamePlayGameLoop implements GameLoop {

  private final GameState gameState;

  private ImageIcon background;

  public DefaultGamePlayGameLoop(final GameState gameState) {
    this.gameState = gameState;
  }

  public void preload() {
    final String resourcePath = gameState.getStateDescriptor().getBackground().getResourcePath();
    final URL url = ClassLoader.getSystemResource(resourcePath);

    background = new ImageIcon(url);
  }

  public void run(Graphics2D graphics2d, SpriteManager spriteManager) {
    GraphicsConfiguration graphicsConfig = graphics2d.getDeviceConfiguration();
    BufferCapabilities bufferCaps = graphicsConfig.getBufferCapabilities();

    graphics2d.drawImage(background.getImage(), 0, 0, null);
  }

}
