package net.phyer.games.graphics;

import java.awt.Dimension;
import java.util.ArrayList;
import java.util.List;

/**
 * Manages {@link Sprite}s, determining whether or not they need to be drawn.
 * @author nathanlane
 *
 */
public final class SpriteManager {

  private final Dimension resolution;
  private final List<Sprite> sprites;

  public SpriteManager(final Dimension resolution) {
    this.resolution = resolution;
    sprites = new ArrayList<Sprite>();
  }

  /**
   * Adds a sprite to the sprite list.
   * @param sprite
   */
  public void addSprite(final Sprite sprite) {
    sprites.add(sprite);
  }

  /**
   * Removes a Sprite if it exists in the sprite list
   * @param sprite
   * @return
   */
  public boolean removeSprite(final Sprite sprite) {
    return sprites.remove(sprite);
  }

  /**
   * Removes a Sprite if it exists in the sprite list and returns it.
   * @param spriteIndex
   * @return
   */
  public Sprite removeSpriteAt(final int spriteIndex) {
    return sprites.remove(spriteIndex);
  }

  /**
   * Gets an array of {@link Sprite}s that are valid to be rendered.
   * @return
   */
  public Sprite[] getSprites() {
    final List<Sprite> validSprites = new ArrayList<Sprite>();

    for (Sprite nextSprite : sprites) {
      Sprite.SpriteLocation location = nextSprite.getLocation();

      if (location.getHorizontalLocation() < resolution.getWidth() || location.getVerticalLocation() < resolution.getHeight()) {
        validSprites.add(nextSprite);
      }
    }

    return (Sprite[])validSprites.toArray();
  }

}
