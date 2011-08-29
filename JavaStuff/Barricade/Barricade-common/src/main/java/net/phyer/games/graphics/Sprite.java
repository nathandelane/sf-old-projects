package net.phyer.games.graphics;

/**
 * Generic sprite.
 * @author nathanlane
 *
 */
public class Sprite {

  private static final long serialVersionUID = 3327200074402945772L;

  private final Animation animation;

  private float horizontalLocation;
  private float verticalLocation;

  public Sprite(final Animation animation) {
    this.animation = animation;
    horizontalLocation = 0.0f;
    verticalLocation = 0.0f;
  }

  public void update(final long elapsedTime) {
    animation.update(elapsedTime);
  }

  public SpriteLocation getLocation() {
    return new SpriteLocation(horizontalLocation, verticalLocation);
  }

  public final class SpriteLocation {

    private final float horizontalLocation;
    private final float verticalLocation;

    public SpriteLocation(final float horizontalLocation, final float verticalLocation) {
      this.horizontalLocation = horizontalLocation;
      this.verticalLocation = verticalLocation;
    }

    public float getHorizontalLocation() {
      return horizontalLocation;
    }

    public float getVerticalLocation() {
      return verticalLocation;
    }

  }

}
