package com.nathandelane.csvquery;

import static org.junit.Assert.assertEquals;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;

import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.log4j.spi.LoggingEvent;
import org.junit.Test;

import com.google.common.base.Predicate;
import com.google.common.collect.Iterables;
import com.google.inject.internal.Lists;
import com.google.inject.internal.Nullable;

public class CsvDataSourceTests {

  private static final String FIELD_SEPARATOR = ",";

  private final InputStream threeRowsThreeColumns = new ByteArrayInputStream("One,Two,Three\n1,2,3\n3,4,5\n5,6,7".getBytes());
  private final InputStream threeRowsThreeColumnsNeedTrimming = new ByteArrayInputStream("One, Two, Three\n1, 2, 3\n3, 4, 5\n5, 6, 7".getBytes());
  private final InputStream threeRowsThreeColumnsThreeMalformedColumns = new ByteArrayInputStream("One,Two\n1,2,3\n3,4,5\n5,6,7".getBytes());
  private final InputStream threeRowsThreeColumnsOneMalformedColumn = new ByteArrayInputStream("One,Two,Three\n1,2\n3,4,5\n5,6,7".getBytes());
  private final InputStream threeRowsNoHeaders = new ByteArrayInputStream("\n1,2,3\n3,4,5\n5,6,7".getBytes());

  @Test
  public void testCsvDataSourceContainsExpectedNumberOfRows() throws IOException {
    final CsvDataSource dataSource = new CsvDataSource(threeRowsThreeColumns, FIELD_SEPARATOR);

    assertEquals("CsvDataSource did not contain the expected number of rows.", 3, dataSource.numberOfRows());
  }

  @Test
  public void testCsvDataSourceContainsExpectedNumberOfColumns() throws IOException {
    final CsvDataSource dataSource = new CsvDataSource(threeRowsThreeColumns, FIELD_SEPARATOR);

    assertEquals("CsvDataSource did not contain the expected number of columns.", 3, dataSource.numberOfColumns());
  }

  @Test
  public void testCsvDataSourceTrimmingExpectedNumberOfRows() throws IOException {
    CsvDataSource dataSource = new CsvDataSource(threeRowsThreeColumnsNeedTrimming, FIELD_SEPARATOR);

    assertEquals("CsvDataSource did not contain the expected number of rows.", 3, dataSource.numberOfRows());
  }

  @Test
  public void testCsvDataSourceTrimmingExpectedNumberOfColumns() throws IOException {
    CsvDataSource dataSource = new CsvDataSource(threeRowsThreeColumnsNeedTrimming, FIELD_SEPARATOR);

    assertEquals("CsvDataSource did not contain the expected number of columns.", 3, dataSource.numberOfColumns());
  }

  @Test
  public void testCsvDataSourceMalformedColumns() throws IOException {
    final MockAppender mockAppender = new MockAppender();

    Logger.getLogger(CsvDataSource.class).addAppender(mockAppender);

    new CsvDataSource(threeRowsThreeColumnsThreeMalformedColumns, FIELD_SEPARATOR);

    Iterable<LoggingEvent> errorLoggingEvents = Iterables.filter(mockAppender.getLogEntries(), new Predicate<LoggingEvent>() {

      public boolean apply(@Nullable LoggingEvent input) {
        return input.getLevel().equals(Level.ERROR);
      }

    });

    int loggingEventsActualCount = Lists.<LoggingEvent>newArrayList(errorLoggingEvents).size();

    assertEquals("The expected number of errors was not the same as the actual number of errors.", 3, loggingEventsActualCount);
  }

  @Test
  public void testCsvDataSourceOneMalformedColumnOfThree() throws IOException {
    final MockAppender mockAppender = new MockAppender();

    Logger.getLogger(CsvDataSource.class).addAppender(mockAppender);

    new CsvDataSource(threeRowsThreeColumnsOneMalformedColumn, FIELD_SEPARATOR);

    Iterable<LoggingEvent> errorLoggingEvents = Iterables.filter(mockAppender.getLogEntries(), new Predicate<LoggingEvent>() {

      public boolean apply(@Nullable LoggingEvent input) {
        return input.getLevel().equals(Level.ERROR);
      }

    });

    int loggingEventsActualCount = Lists.<LoggingEvent>newArrayList(errorLoggingEvents).size();

    assertEquals("The expected number of errors was not the same as the actual number of errors.", 1, loggingEventsActualCount);
  }

  @Test(expected = IOException.class)
  public void testCsvDataSourceThreeRowsNoHeaders() throws IOException {
    CsvDataSource dataSource = new CsvDataSource(threeRowsNoHeaders, FIELD_SEPARATOR);
  }


}
