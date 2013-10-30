package com.nathandelane.csvquery.command;

import java.math.BigInteger;
import java.util.Map;

import com.nathandelane.csvquery.file.CsvFile;
import com.nathandelane.csvquery.file.CsvFileRepository;
import com.nathandelane.csvquery.messaging.Messenger;

public class Columns implements ICommand {

  private static final String CSV_FILE_NAME_PARAMETER_NAME = "CSV_FILE_NAME";

  private final BigInteger serialVersionUid = new BigInteger("17c70a3d8f45475f91b6c9212ee0e99f", 16);

  public String[] getParameterNames() {
    return new String[] { CSV_FILE_NAME_PARAMETER_NAME };
  }

  public void execute(Map<String, String> parameters) {
    if (parameters.isEmpty()) {
      throw new IllegalArgumentException("FileName may not be null.");
    }

    final String fileName = parameters.get(CSV_FILE_NAME_PARAMETER_NAME);

    if (CsvFileRepository.getInstance().fileIsLoaded(fileName)) {
      final CsvFile csvFile = CsvFileRepository.getInstance().getFile(fileName);

      Messenger.output(String.format("%1$s", csvFile.columns().toString()));
    }
  }

  public BigInteger getServialVersionUid() {
    return serialVersionUid;
  }

}
