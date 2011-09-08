package net.phyer.games.barricade.events;

import java.util.HashMap;
import java.util.Map;

/**
 * Factory that provides {@link EventHandler} implementations for the game window.
 * @author nathanlane
 *
 */
public final class EventHandlerFactory {

  private static final EventHandlerFactory instance = new EventHandlerFactory();

  private final Map<EventHandlerType, EventHandler> handlers;

  private EventHandlerFactory() {
    handlers = new HashMap<EventHandlerType, EventHandler>();
  }

  /**
   * Gets an event handler of a specific type.
   * @param eventHandlerType
   * @return Returns null if no {@link EventHandler} implementation exists for an {@link EventHandlerType}.
   */
  public static EventHandler getEventHandler(final EventHandlerType eventHandlerType) {
    EventHandler eventHandler = null;

    if (EventHandlerFactory.instance.handlers.containsKey(eventHandlerType)) {
      eventHandler = EventHandlerFactory.instance.handlers.get(eventHandlerType);
    }

    return eventHandler;
  }

}
