package com.nathandelane.csvquery;

import java.util.List;

import org.apache.log4j.AppenderSkeleton;
import org.apache.log4j.spi.LoggingEvent;
import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.google.inject.internal.Lists;

@AutoProperty
public class MockAppender extends AppenderSkeleton {

  private final List<LoggingEvent> loggingEvents;

  public MockAppender() {
    loggingEvents = Lists.newArrayList();
  }

  public MockAppender(boolean isActive) {
    super(isActive);

    loggingEvents = Lists.newArrayList();
  }

  public void clearEntries() {
    loggingEvents.clear();
  }

  public List<LoggingEvent> getLogEntries() {
    return loggingEvents;
  }

  @Override
  public void close() {
    // No-op
  }

  @Override
  public boolean requiresLayout() {
    return false;
  }

  @Override
  protected void append(LoggingEvent loggingEvent) {
    loggingEvents.add(loggingEvent);
  }

  @Override
  public boolean equals(Object obj) {
    return Pojomatic.equals(this, obj);
  }

  @Override
  public int hashCode() {
    return Pojomatic.hashCode(this);
  }

  @Override
  public String toString() {
    return Pojomatic.toString(this);
  }

}
