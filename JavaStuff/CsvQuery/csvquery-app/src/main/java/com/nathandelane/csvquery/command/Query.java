package com.nathandelane.csvquery.command;

import java.math.BigInteger;
import java.util.Map;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.nathandelane.csvquery.file.CsvFile;
import com.nathandelane.csvquery.file.CsvFileRepository;
import com.nathandelane.csvquery.messaging.Messenger;

@AutoProperty
public class Query implements ICommand {

  private static final String CSV_FILE_NAME_PARAMETER_NAME = "CSV_FILE_NAME";

  private final BigInteger serialVersionUid = new BigInteger("fe3d021a909e49b3b61e0e7e3c46e95d", 16);

  public String[] getParameterNames() {
    return new String[] { CSV_FILE_NAME_PARAMETER_NAME, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
  }

  public void execute(Map<String, String> parameters) {
    if (parameters.isEmpty()) {
      throw new IllegalArgumentException("A query cannot be empty.");
    }

    final String fileName = parameters.get(CSV_FILE_NAME_PARAMETER_NAME);

    if (CsvFileRepository.getInstance().fileIsLoaded(fileName)) {
      final CsvFile csvFile = CsvFileRepository.getInstance().getFile(fileName);

      if (parameters.get("0") != null) {
        if (parameters.get("0").equalsIgnoreCase("max")) {
          if (parameters.get("1") != null) {
            final String columnName = parameters.get("1");
            final String maxValue = csvFile.max(columnName);

            Messenger.output(maxValue);
          }
          else {
            Messenger.error("Column name is required for max function.");
          }
        }
        else if (parameters.get("0").equalsIgnoreCase("min")) {
          if (parameters.get("1") != null) {
            final String columnName = parameters.get("1");
            final String minValue = csvFile.min(columnName);

            Messenger.output(minValue);
          }
          else {
            Messenger.error("Column name is required for max function.");
          }
        }
        else {
          Messenger.error(String.format("Unrecognized query: %1$s.", parameters.get("0")));
        }
      }
      else {
        Messenger.error("No valid query was provided.");
      }
    }
    else {
      Messenger.error(String.format("Cannot query %1$s because it is not loaded.", fileName));
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
