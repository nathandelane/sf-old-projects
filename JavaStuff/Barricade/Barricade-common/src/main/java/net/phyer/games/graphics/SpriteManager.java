package net.phyer.games.graphics;

import java.util.ArrayList;
import java.util.List;

/**
 * Manages {@link Sprite}s, determining whether or not they need to be drawn.
 * @author nathanlane
 *
 */
public class SpriteManager {

  private final List<Sprite> sprites;

  public SpriteManager() {
    sprites = new ArrayList<Sprite>();
  }

  public void addSprite(final Sprite sprite) {
    sprites.add(sprite);
  }

}
