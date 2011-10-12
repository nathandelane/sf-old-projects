package net.phyer.games.barricade.state;

import net.phyer.games.barricade.graphics.AnimationFactory;
import net.phyer.games.graphics.Animation;

/**
 * Represents the chosen bot.
 * @author nathanlane
 *
 */
public enum BotDecision {

  CANNON("Cannon");

  private final Animation animation;

  private BotDecision(final String legendName) {
    animation = AnimationFactory.getAnimationForLegend(legendName);
  }

  public Animation getAnimation() {
    return animation;
  }

}
