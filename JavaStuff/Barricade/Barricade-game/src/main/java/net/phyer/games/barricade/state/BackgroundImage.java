package net.phyer.games.barricade.state;

/**
 * Represents all of the background images available for this game.
 * @author nathanlane
 *
 */
public enum BackgroundImage {

  HallwayOrange("backgrounds/Barr-hallwayOrange.png"),
  MistyHighway("backgrounds/Barr-Hiway77Misty.png");

  private final String resourcePath;

  private BackgroundImage(final String resourcePath) {
    this.resourcePath = resourcePath;
  }

  public String getResourcePath() {
    return resourcePath;
  }

}
