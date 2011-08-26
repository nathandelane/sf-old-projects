package net.phyer.games.graphics;

/**
 * This class represents an animation.
 * @author nathanlane
 *
 */
public class Animation {

  private final ImageMap imageMap;
  
  private int

  public Animation(final ImageMap imageMap) {
    if (imageMap == null || imageMap.isEmpty()) {
      throw new IllegalArgumentException(AnimationMessages.IMAGE_MAP_CANNOT_BE_NULL_MESSAGE);
    }

    this.imageMap = imageMap;
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
