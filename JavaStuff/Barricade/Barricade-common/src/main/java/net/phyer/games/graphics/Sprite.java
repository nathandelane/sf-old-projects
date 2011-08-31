package net.phyer.games.graphics;

import java.awt.Image;

/**
 * Generic sprite.
 * @author nathanlane
 *
 */
public class Sprite {

  private static final long serialVersionUID = 3327200074402945772L;

  private static Integer spriteCounter;

  private final Animation animation;

  private int id;
  private SpriteLocation location;
  private SpriteVelocity velocity;

  public Sprite(final Animation animation) {
    if (Sprite.spriteCounter == null) {
      Sprite.spriteCounter = 0;
    }
    else {
      Sprite.spriteCounter++;
    }

    id = Sprite.spriteCounter;
    this.animation = animation;
    location = new SpriteLocation(0.0f, 0.0f);
  }

  /**
   * Updates the sprite.
   * @param elapsedTime
   */
  public void update(final long elapsedTime) {
    if (velocity.getHorizontalVelocity() != 0 || velocity.getVerticalVelocity() !=  0) {
      final double horizontalLocation = location.getHorizontalLocation() + (velocity.getHorizontalVelocity() * (double)elapsedTime);
      final double verticalLocation = location.getVerticalLocation() + (velocity.getVerticalVelocity() * (double)elapsedTime);

      location = new SpriteLocation(horizontalLocation, verticalLocation);
    }

    animation.update(elapsedTime);
  }

  /**
   * Gets this sprite's ID.
   * @return
   */
  public int getId() {
    return id;
  }

  /**
   * Gets the current location.
   * @return
   */
  public SpriteLocation getLocation() {
    return location;
  }

  /**
   * Sets the current location.
   * @param location
   */
  public void setLocation(final SpriteLocation location) {
    this.location = location;
  }

  /**
   * Gets the current velocity.
   * @return
   */
  public SpriteVelocity getVelocity() {
    return velocity;
  }

  /**
   * Sets the current velocity.
   * @param velocity
   */
  public void setVelocity(final SpriteVelocity velocity) {
    this.velocity = velocity;
  }

  /**
   * Gets the sprite's current image.
   * @return
   */
  public Image getImage() {
    return animation.getImage();
  }

  /**
   * Gets the current width of the sprite based on the image.
   * @return
   */
  public int getWidth() {
    return animation.getImage().getWidth(null);
  }

  /**
   * Gets the current height of the sprite based on the image.
   * @return
   */
  public int getHeight() {
    return animation.getImage().getHeight(null);
  }

  /**
   * Tests whether this and another object are equal. They are only equal if their ID is the
   * same.
   */
  @Override
  public boolean equals(final Object other) {
    boolean result = true;

    if (other instanceof Sprite) {
      final Sprite otherSprite = (Sprite)other;

      if (otherSprite.id != this.id) {
        result = false;
      }
    }
    else {
      result = false;
    }

    return result;
  }

  /**
   * Location of a {@link Sprite}.
   * @author nathanlane
   *
   */
  public final class SpriteLocation {

    private final double horizontalLocation;
    private final double verticalLocation;

    public SpriteLocation(final double horizontalLocation, final double verticalLocation) {
      this.horizontalLocation = horizontalLocation;
      this.verticalLocation = verticalLocation;
    }

    /**
     * Gets the x-location value.
     * @return
     */
    public double getHorizontalLocation() {
      return horizontalLocation;
    }

    /**
     * Gets the y-location value.
     * @return
     */
    public double getVerticalLocation() {
      return verticalLocation;
    }

  }

  /**
   * Velocity of a {@link Sprite}.
   * @author nathanlane
   *
   */
  public final class SpriteVelocity {

    private final double horizontalVelocity;
    private final double verticalVelocity;

    public SpriteVelocity(final double horizontalLocation, final double verticalLocation) {
      this.horizontalVelocity = horizontalLocation;
      this.verticalVelocity = verticalLocation;
    }

    /**
     * Gets the x-velocity value.
     * @return
     */
    public double getHorizontalVelocity() {
      return horizontalVelocity;
    }

    /**
     * Gets the y-velocity value.
     * @return
     */
    public double getVerticalVelocity() {
      return verticalVelocity;
    }

  }

}
