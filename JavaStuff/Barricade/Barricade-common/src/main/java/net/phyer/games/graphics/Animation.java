package net.phyer.games.graphics;

/**
 * This class represents an animation.
 * @author nathanlane
 *
 */
public class Animation implements Runnable {

  private final String name;
  private final ImageMap imageMap;

  public Animation(final String name, final ImageMap imageMap) {
    if (imageMap == null || imageMap.isEmpty()) {
      throw new IllegalArgumentException(AnimationMessages.IMAGE_MAP_CANNOT_BE_NULL_MESSAGE);
    }

    this.name = name;
    this.imageMap = imageMap;
  }

  /**
   * Gets the name of this animation.
   * @return
   */
  public String getName() {
    return name;
  }

  public void run() {
    // TODO: Render the animation, and transform the sprite at the same time.
  }

  /**
   * This class contains messages for {@link ImageMap}.
   * @author nathanlane
   *
   */
  private final class AnimationMessages {

    public static final String IMAGE_MAP_CANNOT_BE_NULL_MESSAGE = "ImageMap cannot be null or empty.";

  }

}
