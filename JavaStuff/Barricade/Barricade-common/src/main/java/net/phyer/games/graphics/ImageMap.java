package net.phyer.games.graphics;

import java.awt.Image;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

/**
 * Represents a mapping of images to indices for an animation.
 * @author nathanlane
 *
 */
public final class ImageMap {

  private final Map<Integer, Image> mappings;

  private int currentFrame;

  /**
   * Creates a new empty ImageMap.
   */
  public ImageMap() {
    mappings = new HashMap<Integer, Image>();
    currentFrame = 0;
  }

  /**
   * Creates an ImageMap with the given frames in their ordinal order in the array.
   * @param frames
   */
  public ImageMap(final Image[] frames) {
    mappings = new HashMap<Integer, Image>();

    int frameCounter = 0;

    for (Image nextFrame : frames) {
      mappings.put(frameCounter, nextFrame);

      frameCounter++;
    }
  }

  /**
   * Puts a frame onto the end of the ImageMap.
   * @param frame
   */
  public void putFrame(final Image frame) {
    Integer[] indices = (Integer[])mappings.keySet().toArray();
    Arrays.sort(indices);

    final int nextFrameIndex = (Math.max(indices[0], indices[(indices.length - 1)]) + 1);

    mappings.put(nextFrameIndex, frame);
  }

  /**
   * Puts a frame into the mix at a certain location. If the order is greater than the size then it will be placed
   * at the end of the collection. If it is less than zero then it will go on the front of the collection.
   * @param order
   * @param frame
   */
  public void putFrame(final int order, final Image frame) {
    if (order <= 0) {
      putAtFront(frame);
    }
    else if (order > mappings.size()) {
      putAtEnd(frame);
    }
    else if (order > 0 && order < mappings.size()) {
      putAtMiddle(order, frame);
    }
  }

  /**
   * Gets the next frame in the sequence.
   * @return
   */
  public Image getNextFrame() {
    Image nextImage = null;

    final int maxOrder = getMaxOrder();

    while(!mappings.containsKey(currentFrame)) {
      if (currentFrame > maxOrder) {
        currentFrame = 0;
      }
      else {
        currentFrame++;
      }
    }

    nextImage = mappings.get(currentFrame);

    currentFrame++;

    if (currentFrame >= mappings.size()) {
      currentFrame = 0;
    }

    return nextImage;
  }

  /**
   * Gets whether this ImageMap is empty.
   * @return
   */
  public boolean isEmpty() {
    return mappings.isEmpty();
  }

  /**
   * Gets the maximum order in the ImageMap.
   * @return
   */
  private int getMaxOrder() {
    final int maxOrder = getMaxOrder();

    return maxOrder;
  }

  /**
   * Puts a new frame at the front of the ImageMap order.
   * @param frame
   */
  private void putAtFront(final Image frame) {
    if (!mappings.containsKey(0)) {
      mappings.put(0, frame);
    }
    else {
      final int maxOrder = getMaxOrder();

      for (int mappingsIndex = maxOrder; mappingsIndex >= 0; mappingsIndex--) {
        if (mappings.containsKey(mappingsIndex)) {
          final Image nextImage = mappings.get(mappingsIndex);

          mappings.put((mappingsIndex + 1), nextImage);
        }
      }

      mappings.put(0, frame);
    }
  }

  /**
   * Puts a new frame at the back of the ImageMap order.
   * @param frame
   */
  private void putAtEnd(final Image frame) {
    if (!mappings.containsKey(mappings.size())) {
      mappings.put(mappings.size(), frame);
    }
    else {
      Integer[] keys = (Integer[])mappings.keySet().toArray();

      Arrays.sort(keys);

      final int newIndex = Math.max(keys[0], keys[(keys.length)]) - 1;

      mappings.put(newIndex, frame);
    }
  }

  /**
   * Puts a new frame in the middle of the ImageMap order, shifting everything after it to the right.
   * @param order
   * @param frame
   */
  private void putAtMiddle(final int order, final Image frame) {
    if (!mappings.containsKey(order)) {
      mappings.put(order, frame);
    }
    else {
      Integer[] keys = (Integer[])mappings.keySet().toArray();

      Arrays.sort(keys);

      for (int mappingsIndex = Math.max(keys[0], keys[(keys.length - 1)]); mappingsIndex >= order; mappingsIndex--) {
        if (mappings.containsKey(mappingsIndex)) {
          final Image nextImage = mappings.get(mappingsIndex);

          mappings.put((mappingsIndex + 1), nextImage);
        }
      }

      if (mappings.size() > keys.length) {
        mappings.put(order, frame);
      }
    }
  }

}
