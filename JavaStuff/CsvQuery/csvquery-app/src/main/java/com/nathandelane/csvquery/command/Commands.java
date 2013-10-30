package com.nathandelane.csvquery.command;

import java.util.Map;

import com.google.common.collect.Maps;

public final class Commands {

  private static final Map<String, Command> MAP = Maps.newHashMap();

  static
  {
    MAP.put("q", new Command("q", "q: quits csvquery.", "q (quit):\nquits csvquery.", Quit.class));
    MAP.put("help", new Command("help", "help: displays this help message.", "help:\ndisplays this help message\n\nhelp <command>: displays detailed help.", Help.class));
    MAP.put("load", new Command("load", "load: loads a CSV file by its path.", "load <path> <has-headers> <column-delimiter>:\nloadhelps a CSV file at <path>.\n<has-headers> must be true or false.\n<column-delimiter> may be \\t (tab), or another printable character.", LoadCsvFile.class));
    MAP.put("list", new Command("list", "list: lists all currently loaded files.", "list:\nlists all currently loaded files.", ListFiles.class));
    MAP.put("delete", new Command("delete", "delete: removes a CSV file from the repository.", "delete <file-name>:\ndeletes the file named <file-name> from the repository.", Delete.class));
    MAP.put("query", new Command("query", "query: queries a CSV file.", "query <file-name> <command>:\nqueries a CSV file.\ncommands: max min", Query.class));
    MAP.put("columns", new Command("columns", "columns: lists the columns of a CSV file.", "columns <file-name>:\nlists the columns of a CSV file.", Columns.class));
  }

  public static class Command {

    private final String commandString;
    private final String helpString;
    private final String detailedHelpString;
    private final Class<? extends ICommand> commandClazz;

    private Command(String commandString, String helpString, String detailedHelpString, Class<? extends ICommand> commandClazz) {
      this.commandString = commandString;
      this.helpString = helpString;
      this.detailedHelpString = detailedHelpString;
      this.commandClazz = commandClazz;
    }

    public String getCommandString() {
      return commandString;
    }

    public String getHelpString() {
      return helpString;
    }

    public String getDetailedHelpString() {
      return detailedHelpString;
    }

    public Class<? extends ICommand> getCommandClass() {
      return commandClazz;
    }

  }

  public static boolean commandExists(String command) {
    return MAP.containsKey(command);
  }

  public static String[] getAvailableCommands() {
    return MAP.keySet().toArray(new String[] {});
  }

  public static Command getCommandByName(String command) {
    return MAP.get(command);
  }

  private Commands() { }

}
