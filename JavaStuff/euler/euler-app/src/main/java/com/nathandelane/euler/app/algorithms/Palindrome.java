package com.nathandelane.euler.app.algorithms;

public class Palindrome implements IAlgorithm<String, Boolean> {

  @Override
  public Boolean execute(String value) {
    Boolean result = true;
    char[] valueArray = value.toCharArray();

    for (int valueIndex = 0; valueIndex < (value.length() / 2); valueIndex++) {
      final int endIndex = (valueArray.length - 1 - valueIndex);
      final char leftValue = valueArray[valueIndex];
      final char rightValue = valueArray[endIndex];

      if (leftValue != rightValue) {
        result = false;

        break;
      }
    }

    return result;
  }

}
