package net.phyer.games.graphics;

import java.awt.Image;
import java.util.HashMap;
import java.util.Map;

public final class ImageMap {

  private final Map<Integer, Image> mappings;

  private int currentFrame;

  public ImageMap() {
    mappings = new HashMap<Integer, Image>();
    currentFrame = 0;
  }

  public Image getNextFrame() {
    Image nextImage = null;

    if (mappings.containsKey(currentFrame)) {

    }

    return nextImage;
  }

  public boolean isEmpty() {
    return mappings.isEmpty();
  }

}
