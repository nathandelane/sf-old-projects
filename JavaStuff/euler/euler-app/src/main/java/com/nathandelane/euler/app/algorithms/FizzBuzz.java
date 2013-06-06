package com.nathandelane.euler.app.algorithms;


public class FizzBuzz implements IAlgorithm<Integer, String> {

  @Override
  public String execute(Integer value) {
    final StringBuilder sb = new StringBuilder();

    if (value % 3 == 0) {
      sb.append("Fizz");
    }
    if (value % 5 == 0) {
      sb.append("Buzz");
    }
    if (((value %3) + (value % 5)) > 0) {
      sb.append(value.toString());
    }

    return sb.toString();
  }

}
