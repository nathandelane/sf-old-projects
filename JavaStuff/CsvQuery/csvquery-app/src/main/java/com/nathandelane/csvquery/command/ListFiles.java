package com.nathandelane.csvquery.command;

import java.math.BigInteger;
import java.util.Map;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.nathandelane.csvquery.file.CsvFileRepository;
import com.nathandelane.csvquery.messaging.Messenger;

@AutoProperty
public class ListFiles implements ICommand {

  private final BigInteger serialVersionUid = new BigInteger("ac27e13cc8d245ae89e029a786ceece5", 16);

  public String[] getParameterNames() {
    return new String[] {};
  }

  public void execute(Map<String, String> parameters) {
    final String[] filesNames = CsvFileRepository.getInstance().fileNames();

    if (filesNames.length == 0) {
      Messenger.output("There are currently no files loaded - use load command to load a CSV file.");
    }
    else {
      for (String nextFileName : filesNames) {
        Messenger.output(nextFileName);
      }
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
