package net.phyer.games.barricade.state;

/**
 * Keeps contracts for all of the game states in the game.
 * @author nathanlane
 *
 */
public enum GameState {

  MISTY_HIGHWAY(new StateDescriptor(BackgroundImage.MistyHighway)),
  HALLWAY_ORANGE(new StateDescriptor(BackgroundImage.HallwayOrange));

  private final StateDescriptor stateDescriptor;

  private GameState(final StateDescriptor stateDescriptor) {
    this.stateDescriptor = stateDescriptor;
  }

  public StateDescriptor getStateDescriptor() {
    return stateDescriptor;
  }

}
