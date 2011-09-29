package net.phyer.games.barricade.state;

/**
 * Represents information about a game state, specific to Barricade.
 * @author nathanlane
 *
 */
public class StateDescriptor {

  private final BackgroundImage background;

  public StateDescriptor(final BackgroundImage background) {
    this.background = background;
  }

  public BackgroundImage getBackground() {
    return background;
  }

}
