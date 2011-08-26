package net.phyer.games.graphics;

/**
 * Represents an object that is collision detectable.
 * @author nathanlane
 *
 */
public interface CollisionDetectable {

  /**
   * Detects a collision with another CollisionDetectable.
   * @param other
   * @return
   */
  boolean collidesWith(final CollisionDetectable other);

}
