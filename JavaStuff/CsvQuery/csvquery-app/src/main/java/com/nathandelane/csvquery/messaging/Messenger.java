package com.nathandelane.csvquery.messaging;

public class Messenger {

  public static void output(String value) {
    System.out.println(value);
  }

  public static void error(String value) {
    System.err.println(value);
  }

  public static void prompt(String prompt) {
    System.out.print(prompt);
  }

}
