package net.phyer.games.barricade.graphics;

import java.util.HashMap;
import java.util.Map;

import net.phyer.games.graphics.Animation;

/**
 * Factory for animations.
 * @author nathanlane
 *
 */
public final class AnimationFactory {

  private static final Map<String, Animation> animationDictionary = new HashMap<String, Animation>();

  public static Animation getAnimationForLegend(final String legendName) {
    Animation response = null;

    if (!animationDictionary.containsKey(legendName)) {
      response = loadAnimationByLegend(legendName);
    }

    return response;
  }

  private static Animation loadAnimationByLegend(final String legendName) {
    return null;
  }

}
