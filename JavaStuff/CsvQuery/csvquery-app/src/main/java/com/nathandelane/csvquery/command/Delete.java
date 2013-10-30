package com.nathandelane.csvquery.command;

import java.math.BigInteger;
import java.util.Map;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.nathandelane.csvquery.file.CsvFileRepository;
import com.nathandelane.csvquery.messaging.Messenger;

@AutoProperty
public class Delete implements ICommand {

  private static final String FILE_NAME_PARAMETER_NAME = "FILE_NAME";

  private final BigInteger serialVersionUid = new BigInteger("1ffd2dbc2fd0412b85e847c313f3e628", 16);

  public String[] getParameterNames() {
    return new String[] { FILE_NAME_PARAMETER_NAME };
  }

  public void execute(Map<String, String> parameters) {
    if (parameters.isEmpty()) {
      throw new IllegalArgumentException("FileName may not be null.");
    }

    final String fileName = parameters.get(FILE_NAME_PARAMETER_NAME);

    if (CsvFileRepository.getInstance().fileIsLoaded(fileName)) {
      CsvFileRepository.getInstance().removeFile(fileName);

      Messenger.output(String.format("Successfully removed %1$s from repositry.", fileName));
    }
    else {
      Messenger.error(String.format("File %1$s is not currently loaded.", fileName));
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
