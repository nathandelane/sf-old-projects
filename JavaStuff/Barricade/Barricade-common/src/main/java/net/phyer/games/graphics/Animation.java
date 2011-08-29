package net.phyer.games.graphics;

import java.awt.Image;
import java.util.ArrayList;
import java.util.List;

/**
 * This class represents an animation.
 * @author nathanlane
 *
 */
public final class Animation {

  private final List<AnimationFrame> frames;

  private long totalDuration;
  private long elapsedTime;
  private int currentFrame;

  public Animation() {
    frames = new ArrayList<AnimationFrame>();
    totalDuration = 0;
    elapsedTime = 0;
    currentFrame = 0;
  }

  /**
   * Adds an {@link AnimationFrame} to the animation.
   * @param frame
   */
  public synchronized void addFrame(final AnimationFrame frame) {
    frames.add(frame);
    totalDuration += frame.getDuration();
  }

  /**
   * Adds an {@link AnimationFrame} to the animation.
   * @param image
   * @param duration
   */
  public synchronized void addFrame(final Image image, final int duration) {
    final AnimationFrame newAnimationFrame = new AnimationFrame(image, duration);

    frames.add(newAnimationFrame);
    totalDuration += newAnimationFrame.getDuration();
  }

  /**
   * Gets the current image of the animation.
   * @return
   */
  public synchronized Image getImage() {
    Image currentImage = null;

    if (frames.size() > 0) {
      currentImage = frames.get(currentFrame).getImage();
    }

    return currentImage;
  }

  /**
   * Starts the animation.
   */
  public synchronized void start() {
    elapsedTime = 0;
    currentFrame = 0;
  }

  /**
   * Updates the animation.
   * @param elapsedTime
   */
  public synchronized void update(final long elapsedTime) {
    if (frames.size() > 1) {
      this.elapsedTime += elapsedTime;

      if (this.elapsedTime >= totalDuration) {
        this.elapsedTime %= totalDuration;

        currentFrame = 0;
      }

      while (this.elapsedTime > frames.get(currentFrame).getDuration()) {
        currentFrame++;
      }
    }
  }

  /**
   * Tests whether this and another object are the same. They are the same if they
   * contain the same {@link AnimationFrame}s.
   */
  @Override
  public boolean equals(final Object other) {
    boolean result = true;

    if (other instanceof Animation) {
      final Animation otherAnimation = (Animation)other;

      if (otherAnimation.frames.size() == this.frames.size()) {
        for (AnimationFrame nextOtherFrame : otherAnimation.frames) {
          if (!this.frames.contains(nextOtherFrame)) {
            result = false;

            break;
          }
        }
      }
      else {
        result = false;
      }
    }
    else {
      result = false;
    }

    return result;
  }

  /**
   * Represents a frame of animation including its image and duration that it is on-screen.
   * @author nathanlane
   *
   */
  public class AnimationFrame {

    private final Image image;
    private final int duration;

    public AnimationFrame(final Image image, final int duration) {
      this.image = image;
      this.duration = duration;
    }

    /**
     * Gets the image for this AnimationFrame.
     * @return
     */
    public Image getImage() {
      return image;
    }

    /**
     * Gets the duration for this AnimationFrame.
     * @return
     */
    public int getDuration() {
      return duration;
    }

    /**
     * Tests whether this and another object are the same. Two {@link AnimationFrame} objects are the
     * same if they contain the same image and have the same duration.
     */
    @Override
    public boolean equals(final Object other) {
      boolean result = true;

      if (other instanceof AnimationFrame) {
        final AnimationFrame otherFrame = (AnimationFrame)other;

        if (!otherFrame.image.equals(this.image)) {
          result = false;
        }
        else if (otherFrame.duration != this.duration) {
          result = false;
        }
      }
      else {
        result = false;
      }

      return result;
    }

  }

}
