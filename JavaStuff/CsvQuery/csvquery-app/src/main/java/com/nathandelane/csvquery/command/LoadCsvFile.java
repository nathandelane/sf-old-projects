package com.nathandelane.csvquery.command;

import java.io.File;
import java.math.BigInteger;
import java.util.Map;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.nathandelane.csvquery.file.CsvFile;
import com.nathandelane.csvquery.file.CsvFileRepository;
import com.nathandelane.csvquery.messaging.Messenger;

@AutoProperty
public class LoadCsvFile implements ICommand {

  private static final String FILE_PATH_PARAMETER_NAME = "FILE_PATH";
  private static final String HAS_HEADERS_PARAMETER_NAME = "HAS_HEADERS";
  private static final String COLUMN_DELIMITER_PARAMETER_NAME = "COLUMN_DELIMITER";

  private final BigInteger serialVersionUid = new BigInteger("2c6d0b5dcef84bbd8e7bed751e09030f", 16);

  public String[] getParameterNames() {
    return new String[] { FILE_PATH_PARAMETER_NAME, HAS_HEADERS_PARAMETER_NAME, COLUMN_DELIMITER_PARAMETER_NAME };
  }

  public void execute(Map<String, String> parameters) throws IllegalArgumentException {
    if (parameters.size() < getParameterNames().length) {
      throw new IllegalArgumentException("Must provide a file path, has headers, and column delimiter.");
    }

    final String filePath = parameters.get(FILE_PATH_PARAMETER_NAME);
    final File file = new File(filePath);

    if (file.exists()) {
      final boolean hasHeaders = Boolean.parseBoolean(parameters.get(HAS_HEADERS_PARAMETER_NAME));
      final String columnDelimiter = parameters.get(COLUMN_DELIMITER_PARAMETER_NAME);
      final CsvFile csvFile = CsvFile.loadFile(filePath, hasHeaders, columnDelimiter);
      final String name = ((file.getName().indexOf(".") > -1) ? file.getName().split("\\.")[0] : file.getName());

      CsvFileRepository.getInstance().addFile(name, csvFile);

      Messenger.output(String.format("Successfully loaded %1$s.", file.getName()));
    }
  }

  public BigInteger getServialVersionUid() {
    return serialVersionUid;
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

}
