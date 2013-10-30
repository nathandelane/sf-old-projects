package com.nathandelane.csvquery.command;

import java.math.BigInteger;
import java.util.Map;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

import com.nathandelane.csvquery.messaging.Messenger;

@AutoProperty
public class Help implements ICommand {

  private final BigInteger serialVersionUid = new BigInteger("6ccd342360a74e02abd0d1fd852cda82", 16);

  public String[] getParameterNames() {
    return new String[] { "COMMAND" };
  }

  public void execute(Map<String, String> parameters) {
    if (parameters.isEmpty()) {
      final String[] availableCommands = Commands.getAvailableCommands();

      for (String nextCommand : availableCommands) {
        Messenger.output(Commands.getCommandByName(nextCommand).getHelpString());
      }
    }
    else {
      if (parameters.size() == 1) {
        final String command = parameters.get("COMMAND");

        if (Commands.commandExists(command)) {
          Messenger.output(Commands.getCommandByName(command).getDetailedHelpString());
        }
        else {
          Messenger.output(String.format("no help info for command %1$s.", command));
        }
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
