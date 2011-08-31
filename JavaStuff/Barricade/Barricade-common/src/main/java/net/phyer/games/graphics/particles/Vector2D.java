package net.phyer.games.graphics.particles;

/**
 * Represents magnitude, position, and direction.
 * @author nathanlane
 *
 */
public final class Vector2D {

  private final double horizontalComponent;
  private final double verticalComponent;

  public Vector2D(final double horizontalComponent, final double verticalComponent) {
    this.horizontalComponent = horizontalComponent;
    this.verticalComponent = verticalComponent;
  }

  /**
   * Gets the horizontal component of the vector point.
   * @return
   */
  public double getHorizontalComponent() {
    return horizontalComponent;
  }

  /**
   * Gets the vertical component of the vector point.
   * @return
   */
  public double getVerticalComponent() {
    return verticalComponent;
  }

  /**
   * Adds two Vector2D objects together.
   * @param other
   * @return
   */
  public Vector2D add(final Vector2D other) {
    return new Vector2D((double)(this.horizontalComponent + other.horizontalComponent), (double)(this.verticalComponent + other.verticalComponent));
  }

  /**
   * Subtracts another Vector2D object from this Vector2D object.
   * @param other
   * @return
   */
  public Vector2D subtract(final Vector2D other) {
    return new Vector2D((double)(this.horizontalComponent - other.horizontalComponent), (double)(this.verticalComponent - other.verticalComponent));
  }

  /**
   * Multiplies another Vector2D object with this Vector2D object.
   * @param other
   * @return
   */
  public Vector2D multiply(final Vector2D other) {
    return new Vector2D((double)(this.horizontalComponent * other.horizontalComponent), (double)(this.verticalComponent * other.verticalComponent));
  }

  /**
   * Returns a stringified version of this Vector2D object.
   */
  @Override
  public String toString() {
    return String.format("{ Horizontal: %1$s, Vertical: %2$s }", horizontalComponent, verticalComponent);
  }

  /**
   * Divides this Vector2D object with another Vector2D object.
   * @param other
   * @throws IllegalArgumentException
   * @return
   */
  public Vector2D divide(final Vector2D other) throws IllegalArgumentException {
    if (other.horizontalComponent == 0 || other.verticalComponent == 0) {
      throw new IllegalArgumentException("One part of the other Vector2D object is 0. Divide by zero error.");
    }

    return new Vector2D((double)(this.horizontalComponent / other.horizontalComponent), (double)(this.verticalComponent / other.verticalComponent));
  }

  /**
   * Determines whether this Vector2D object is equal to another object.
   */
  @Override
  public boolean equals(final Object other) {
    boolean result = false;

    if (other instanceof Vector2D) {
      final Vector2D otherInstance = (Vector2D)other;

      if (otherInstance.horizontalComponent == this.horizontalComponent && otherInstance.verticalComponent == this.verticalComponent) {
        result = true;
      }
    }

    return result;
  }

}