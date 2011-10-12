package net.phyer.games.barricade.graphics;

import java.awt.Image;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

import javax.swing.ImageIcon;

/**
 * Represents the model of a sprite represented by a legend and a map.
 * @author nathanlane
 *
 */
public final class SpriteModel {

  public class SpriteLegend {

    private static final String MAP_LOCATION = "mapLocation";
    private static final String FRAMES = "frames";
    private static final String ACTIONS = "actions";

    private final Map<String, Object> legendObjects;

    public SpriteLegend(final String legendName) throws IOException {
      legendObjects = new HashMap<String, Object>();

      final BufferedReader legendReader = new BufferedReader(new InputStreamReader(ClassLoader.getSystemResourceAsStream(legendName)));

      if (legendReader != null) {
        readLegendFile(legendReader);
      }
    }

    public Object get(final String objectName) {
      return legendObjects.get(objectName);
    }

    private void readLegendFile(final BufferedReader legendReader) throws IOException {
      String nextLine = null;

      while ((nextLine = legendReader.readLine()) != null) {
        if (nextLine.startsWith(MAP_LOCATION)) {
          storeMapLocation(nextLine);
        }
        else if (nextLine.startsWith(FRAMES)) {
          storeFrames(nextLine);
        }
      }
    }

    private void storeMapLocation(final String mapLocationLine) {
      final String[] mapLocationParts = mapLocationLine.split(":");

      if (mapLocationParts.length == 2) {
        final String mapLocation = mapLocationParts[1];

        legendObjects.put(MAP_LOCATION, mapLocation);
      }
    }

    private void storeFrames(final String framesLine) {
      final String[] framesLineParts = framesLine.split(":");

      if (framesLineParts.length == 2) {
        final String[] frames = framesLineParts[1].split(";");

        for (String nextFrame : frames) {
          final String[] frameParts = nextFrame.split("[=,]{1}");
        }
      }
    }

    public class Frame {

      private final String name;
      private final int left;
      private final int top;
      private final int width;
      private final int height;

      public Frame(String name, int left, int top, int width, int height) {
        this.name = name;
        this.left = left;
        this.top = top;
        this.width = width;
        this.height = height;
      }

      public String getName() {
        return name;
      }

      public int getLeft() {
        return left;
      }

      public int getTop() {
        return top;
      }

      public int getWidth() {
        return width;
      }

      public int getHeight() {
        return height;
      }

    }

  }

  public class SpriteMap {

    private final Image map;

    public SpriteMap(final String mapName) {
      final URL mapUrl = ClassLoader.getSystemResource(mapName);
      final ImageIcon mapImage = new ImageIcon(mapUrl);

      map = mapImage.getImage();
    }

    public final Image getImage() {
      return map;
    }

  }

  /**
   * Exception to be thrown when there is an exception loading the sprite model.
   * @author nathanlane
   *
   */
  public class SpriteModelInvalidException extends RuntimeException {

    private static final long serialVersionUID = 1L;

    public SpriteModelInvalidException() {
      super();
    }

    public SpriteModelInvalidException(String message) {
      super(message);
    }

    public SpriteModelInvalidException(Throwable cause) {
      super(cause);
    }

    public SpriteModelInvalidException(String message, Throwable cause) {
      super(message, cause);
    }

  }

}
