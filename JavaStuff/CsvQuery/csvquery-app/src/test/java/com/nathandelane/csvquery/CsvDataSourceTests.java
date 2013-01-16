package com.nathandelane.csvquery;

import static org.junit.Assert.assertEquals;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;

import org.junit.Test;

public class CsvDataSourceTests {

  private static final String FIELD_SEPARATOR = ",";

  private final InputStream threeRowsThreeColumns = new ByteArrayInputStream("One,Two,Three\n1,2,3\n3,4,5\n5,6,7".getBytes());;
  private final InputStream threeRowsThreeColumnsNeedTrimming = new ByteArrayInputStream("One, Two, Three\n1, 2, 3\n3, 4, 5\n5, 6, 7".getBytes());;
  private final InputStream threeRowsThreeColumnsMalformedColumns = new ByteArrayInputStream("One,Two\n1,2,3\n3,4,5\n5,6,7".getBytes());
  private final InputStream threeRowsThreeColumnsMalformedRow = new ByteArrayInputStream("One,Two,Three\n1,2\n3,4,5\n5,6,7".getBytes());

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
    CsvDataSource dataSource = new CsvDataSource(threeRowsThreeColumnsMalformedColumns, FIELD_SEPARATOR);
  }

}
