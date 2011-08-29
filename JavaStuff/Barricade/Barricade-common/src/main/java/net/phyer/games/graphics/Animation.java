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

  public synchronized void addFrame(final AnimationFrame frame) {
    frames.add(frame);
    totalDuration += frame.getDuration();
  }

  public synchronized void addFrame(final Image image, final int duration) {
    final AnimationFrame newAnimationFrame = new AnimationFrame(image, duration);

    frames.add(newAnimationFrame);
    totalDuration += newAnimationFrame.getDuration();
  }

  public synchronized Image getImage() {
    Image currentImage = null;

    if (frames.size() > 0) {
      currentImage = frames.get(currentFrame).getImage();
    }

    return currentImage;
  }

  public synchronized void start() {
    elapsedTime = 0;
    currentFrame = 0;
  }

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

    public Image getImage() {
      return image;
    }

    public int getDuration() {
      return duration;
    }

  }

}
