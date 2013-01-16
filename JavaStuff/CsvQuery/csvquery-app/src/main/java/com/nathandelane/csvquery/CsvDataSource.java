package com.nathandelane.csvquery;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import org.apache.log4j.Logger;
import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.google.common.base.Predicate;
import com.google.common.collect.Iterables;
import com.google.inject.internal.Lists;
import com.google.inject.internal.Maps;

@AutoProperty
public class CsvDataSource implements Iterable<Map<String, String>> {

  private static final Logger LOGGER = Logger.getLogger(CsvDataSource.class);

  private final List<Map<String, String>> csvDataTable;
  private final CsvDataSourceHeader header;

  public CsvDataSource(InputStream inputStream, String fieldSeparator) throws IOException {
    if (inputStream == null) {
      throw new IllegalArgumentException("InputStream may not be null.");
    }
    if (fieldSeparator == null || fieldSeparator.equals("")) {
      throw new IllegalArgumentException("FieldSeparator may not be null.");
    }

    csvDataTable = Lists.newArrayList();
    header = new CsvDataSourceHeader(inputStream, fieldSeparator);

    loadData(inputStream, fieldSeparator);
  }

  @Override
  public Iterator<Map<String, String>> iterator() {
    return csvDataTable.iterator();
  }

  public Iterable<Map<String, String>> filter(Predicate<Map<String, String>> predicate) {
    return Iterables.filter(csvDataTable, predicate);
  }

  public int numberOfRows() {
    return csvDataTable.size();
  }

  public int numberOfColumns() {
    return header.numberOfColumns();
  }

  public int numberOfRows(Predicate<Map<String, String>> predicate) {
    int size = 0;

    final Iterable<Map<String, String>> lines = Iterables.filter(csvDataTable, predicate);
    final Iterator<Map<String, String>> iterator = lines.iterator();

    while (iterator.hasNext()) {
      size += 1;
    }

    return size;
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

  private void loadData(InputStream inputStream, String fieldSeparator) throws IOException {
    final BufferedReader br = new BufferedReader(new InputStreamReader(inputStream));

    if (header != null && header.numberOfColumns() > 0) {
      String nextLine = br.readLine(); // Reading in header again, because I reset the stream in the
                                       // CsvDataSourceHeader.

      while ((nextLine = br.readLine()) != null) {
        if (nextLine != null && !nextLine.equals(""))
          ;

        try {
          final String[] fieldValues = nextLine.split(fieldSeparator);

          if (fieldValues == null || fieldValues.length == 0) {
            throw new IOException("Could not find any headers in the first line of input stream.");
          }
          if (fieldValues.length != header.numberOfColumns()) {
            throw new IOException("Number of columns does not match number of column headers.");
          }

          final Map<String, String> nextRow = Maps.newHashMap();

          for (int fieldIndex = 0; fieldIndex < header.numberOfColumns(); fieldIndex++) {
            final String columnHeader = header.get(fieldIndex);
            final String value = fieldValues[fieldIndex];

            nextRow.put(columnHeader, value.trim());
          }

          csvDataTable.add(nextRow);
        }
        catch (IOException ex) {
          LOGGER.error(ex.getMessage(), ex);
        }
      }
    }
    else {
      throw new IOException(String.format("CsvDataSourceHeader was either null or contained no columns: %1$s", header));
    }
  }

}
