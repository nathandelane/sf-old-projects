package com.nathandelane.euler.app.algorithms;

/**
 * <p>
 * If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
 * </p><p>
 * Find the sum of all the multiples of 3 or 5 below 1000.
 * </p>
 * @author nathanlane
 *
 */
public class MultiplesOfThreeAndFive implements IAlgorithm<Integer, String> {

  @Override
  public String execute(Integer maximum) {
    if (maximum <= 0) {
      return "0";
    }

    Long result = 0L;

    for (int i = 0; i < maximum; i++) {
      if (i % 3 == 0 || i % 5 == 0) {
        result += i;
      }
    }

    return result.toString();
  }

}
