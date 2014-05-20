package com.nathandelane.csvquery.file;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.BigDecimal;
import java.util.List;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.google.common.collect.Lists;
import com.nathandelane.csvquery.messaging.Messenger;

@AutoProperty
public class CsvFile {

  private final List<String> headers;
  private final List<List<String>> rows;

  private CsvFile() {
    headers = Lists.newArrayList();
    rows = Lists.newArrayList();
  }

  public String max(String columnName) {
    final List<String> values = getValues(columnName);

    if (isNumeric(values.get(0))) {
      return numericMax(values);
    }
    else {
      return stringMax(values);
    }
  }

  public String min(String columnName) {
    final List<String> values = getValues(columnName);

    if (isNumeric(values.get(0))) {
      return numericMin(values);
    }
    else {
      return stringMin(values);
    }
  }

  public String getRowsWhereColumnLessThan(String columnName, String maxValue) {
    final StringBuilder sb = new StringBuilder("");
    final int columnIndex = headers.indexOf(columnName);

    for (List<String> nextRow : rows) {
      if (isNumeric(maxValue)) {
        final BigDecimal d = new BigDecimal(maxValue);
        final BigDecimal val = new BigDecimal(nextRow.get(columnIndex));

        if (val.compareTo(d) < 0) {
          sb.append(String.format("%1$s\n", nextRow));
        }
      }
      else {
        final String val = nextRow.get(columnIndex);

        if (val.compareTo(maxValue) < 0) {
          sb.append(String.format("%1$s\n", nextRow));
        }
      }
    }

    return sb.toString();
  }

  public String getRowsWhereColumnGreaterThan(String columnName, String maxValue) {
    final StringBuilder sb = new StringBuilder("");
    final int columnIndex = headers.indexOf(columnName);

    for (List<String> nextRow : rows) {
      if (isNumeric(maxValue)) {
        final BigDecimal d = new BigDecimal(maxValue);
        final BigDecimal val = new BigDecimal(nextRow.get(columnIndex));

        if (val.compareTo(d) > 0) {
          sb.append(String.format("%1$s\n", nextRow));
        }
      }
      else {
        final String val = nextRow.get(columnIndex);

        if (val.compareTo(maxValue) > 0) {
          sb.append(String.format("%1$s\n", nextRow));
        }
      }
    }

    return sb.toString();
  }

  public List<String> columns() {
    return headers;
  }

  @Override
  public boolean equals(Object other) {
    return Pojomatic.equals(this, other);
  }

  @Override
  public int hashCode() {
    return Pojomatic.hashCode(this);
  }

  @Override
  public String toString() {
    return Pojomatic.toString(this);
  }

  private List<String> getValues(String columnName) {
    final int columnIndex = headers.indexOf(columnName);
    final List<String> values = Lists.newArrayList();

    for (List<String> nextRow : rows) {
      values.add(nextRow.get(columnIndex));
    }

    return values;
  }

  private String numericMax(List<String> values) {
    BigDecimal max = BigDecimal.ZERO;

    for (String nextVal : values) {
      final BigDecimal possibleMax = new BigDecimal(nextVal);

      if (possibleMax.compareTo(max) == 1) {
        max = possibleMax;
      }
    }

    return max.toString();
  }

  private String stringMax(List<String> values) {
    String max = values.get(0);

    for (String nextVal : values) {
      if (nextVal.compareTo(max) == 1) {
        max = nextVal;
      }
    }

    return max;
  }

  private String numericMin(List<String> values) {
    BigDecimal min = new BigDecimal("" + Integer.MAX_VALUE);

    for (String nextVal : values) {
      final BigDecimal possibleMin = new BigDecimal(nextVal);

      if (possibleMin.compareTo(min) == -1) {
        min = possibleMin;
      }
    }

    return min.toString();
  }

  private String stringMin(List<String> values) {
    String min = values.get(0);

    for (String nextVal : values) {
      if (nextVal.compareTo(min) == -1) {
        min = nextVal;
      }
    }

    return min;
  }

  private boolean isNumeric(String val) {
    boolean result = true;

    try {
      new BigDecimal(val);
    }
    catch(NumberFormatException e) {
      result = false;
    }

    return result;
  }

  public static CsvFile loadFile(String filePath, boolean hasHeaders, String columnDelimiter) {
    final CsvFile csvFile = new CsvFile();

    try {
      final FileInputStream fis = new FileInputStream(filePath);
      final BufferedReader reader = new BufferedReader(new InputStreamReader(fis));

      if (hasHeaders) {
        loadHeaders(csvFile, reader.readLine(), columnDelimiter);
      }

      String nextLine = null;

      while ((nextLine = reader.readLine()) != null) {
        final String[] columnValues = nextLine.split(columnDelimiter);
        final List<String> nextRow = Lists.newArrayList(columnValues);

        csvFile.rows.add(nextRow);
      }

      reader.close();
    }
    catch (FileNotFoundException e) {
      Messenger.error(String.format("Could not find file using path %1$s.", filePath));
    }
    catch (IOException e) {
      Messenger.error("Could not read from CSV file.");
    }

    return csvFile;
  }

  private static void loadHeaders(CsvFile csvFile, String headerRow, String columnDelimiter) {
    if (headerRow != null) {
      final String[] headerLabels = headerRow.trim().split(columnDelimiter);

      for (String nextHeaderLabel : headerLabels) {
        final String headerLabel = nextHeaderLabel.replaceAll("\"", "");

        csvFile.headers.add(headerLabel);
      }
    }
  }

}
