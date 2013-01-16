package com.nathandelane.csvquery;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Iterator;
import java.util.List;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.google.inject.internal.Lists;

/**
 * Represents a CSV data source header, or the column headers of a CSV file.
 * @author nathanlane
 *
 */
@AutoProperty
public class CsvDataSourceHeader implements Iterable<String> {

  private final List<String> headers;

  public CsvDataSourceHeader(InputStream inputStream, String fieldSeparator) throws IOException {
    if (inputStream == null) {
      throw new IllegalArgumentException("InputStream may not be null.");
    }
    if (fieldSeparator == null || fieldSeparator.equals("")) {
      throw new IllegalArgumentException("FieldSeparator may not be null.");
    }

    headers = Lists.newArrayList();

    loadHeader(inputStream, fieldSeparator);

    inputStream.reset();
  }

  public String get(int index) {
    return headers.get(index);
  }

  public int numberOfColumns() {
    return headers.size();
  }

  @Override
  public Iterator<String> iterator() {
    return headers.iterator();
  }

  private void loadHeader(InputStream inputStream, String fieldSeparator) throws IOException {
    final BufferedReader br = new BufferedReader(new InputStreamReader(inputStream));
    final String headerLine = br.readLine();

    if (headerLine == null || headerLine.equals("")) {
      throw new IOException("Could not read header line.");
    }

    final String[] columnNames = headerLine.split(fieldSeparator);

    if (columnNames == null || columnNames.length == 0) {
      throw new IOException("Could not find any headers in the first line of input stream.");
    }

    for (String nextColumnName : columnNames) {
      headers.add(nextColumnName.trim());
    }
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
