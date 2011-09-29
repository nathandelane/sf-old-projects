package net.phyer.games.barricade.state;

import net.phyer.games.GameContext;
import net.phyer.games.barricade.graphics.BarricadeSprite;

/**
 * Represents the game context for Barricade.
 * @author nathanlane
 *
 */
public final class BarricadeGameContext {

  private static final String PLAYER_ONE_BOT_NAME = "Player1Bot";
  private static final GameContext context = GameContext.getContext();

  public static BarricadeSprite getPlayerOneBot() {
    return (BarricadeSprite) context.getObject(PLAYER_ONE_BOT_NAME);
  }

  public static void setPlayerOneBot() {

  }

}
