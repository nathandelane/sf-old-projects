package com.nathandelane.csvquery;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.Map;

import com.google.common.collect.Maps;
import com.nathandelane.csvquery.command.Commands;
import com.nathandelane.csvquery.command.ICommand;
import com.nathandelane.csvquery.command.Quit;
import com.nathandelane.csvquery.messaging.Messenger;

public class Main {

  private static final String PROMPT = ">";

  private Main() {
    // No-op
  }

  private void executeCommandLoop() throws IOException {
    final BufferedReader inputReader = new BufferedReader(new InputStreamReader(System.in));

    String input = "";

    while (!input.trim().equals("q")) {
      Messenger.prompt(String.format("\n%1$s ", PROMPT));

      input = inputReader.readLine();

      if (input != null && !input.trim().equals("")) {
        final String[] inputFields = input.split("\\s+");
        final String commandString = inputFields[0].trim();

        ICommand command = null;

        if (Commands.commandExists(commandString)) {
          try {
            command = Commands.getCommandByName(commandString).getCommandClass().newInstance();
          }
          catch (Exception e) {
            Messenger.error(e.getMessage());
          }
        }

        if (command != null && !command.equals(Quit.DEFAULT)) {
          try {
            final String[] inputFieldArguments = (inputFields.length > 1 ? Arrays.copyOfRange(inputFields, 1, inputFields.length) : new String[] {});
            final Map<String, String> mappedInputFields = (inputFieldArguments.length > 0 ? mapInputFields(command, inputFieldArguments) : Maps.<String, String>newHashMap());

            command.execute(mappedInputFields);
          }
          catch (Exception e) {
            Messenger.output(String.format("Error: %1$s\n", e.getMessage()));
          }
        }
        else if (command != null && command.equals(Quit.DEFAULT)) {
          Messenger.output("Goodbye.");
        }
        else {
          Messenger.output(String.format("Did not understand command %1$s.", inputFields[0]));
        }
      }
    }

    inputReader.close();
  }

  private Map<String, String> mapInputFields(ICommand command, String[] inputFields) {
    if (command == null) {
      throw new IllegalArgumentException("Command may not be null.");
    }
    else if (inputFields == null || inputFields.length == 0) {
      throw new IllegalArgumentException("InputFields may not by null or empty.");
    }
    else if (inputFields.length > command.getParameterNames().length) {
      throw new IllegalArgumentException("Number of arguments does not match required number of arguments.");
    }

    final Map<String, String> parameters = Maps.newHashMap();
    final String[] parameterNames = command.getParameterNames();

    for (int index = 0; index < Math.min(inputFields.length, parameterNames.length); index++) {
      parameters.put(parameterNames[index], inputFields[index]);
    }

    return parameters;
  }

  /**
   * @param args
   */
  public static void main(String[] args) {
    final Main main = new Main();
    try {
      main.executeCommandLoop();
    }
    catch (IOException e) {
      // TODO Auto-generated catch block
      e.printStackTrace();
    }
  }

}
