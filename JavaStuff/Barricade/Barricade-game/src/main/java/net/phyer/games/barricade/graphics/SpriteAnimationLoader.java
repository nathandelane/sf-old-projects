package net.phyer.games.barricade.graphics;

import java.util.HashMap;
import java.util.Map;

import net.phyer.games.graphics.Animation;

/**
 * Loads a sprite an creates an animation.
 * @author nathanlane
 *
 */
public class SpriteAnimationLoader {

  private static SpriteAnimationLoader instance;

  private final Map<String, Animation> loadedAnimations;

  private SpriteAnimationLoader() {
    loadedAnimations = new HashMap<String, Animation>();
  }

  public Animation loadAnimation(final String spriteName) {
    Animation queriedAnimation = null;

    if (loadedAnimations.containsKey(spriteName)) {
      queriedAnimation = loadedAnimations.get(spriteName);
    }
    else {

    }

    return queriedAnimation;
  }

  public static Animation loadAnimationFor(final String spriteName) {
    if (SpriteAnimationLoader.instance == null) {
      SpriteAnimationLoader.instance = new SpriteAnimationLoader();
    }

    final Animation queriedAnimation = SpriteAnimationLoader.instance.loadAnimation(spriteName);

    return queriedAnimation;
  }

}
