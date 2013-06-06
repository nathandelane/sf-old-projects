package com.nathandelane.euler.app;

import java.util.Calendar;

import com.nathandelane.euler.app.algorithms.IAlgorithm;
import com.nathandelane.euler.app.algorithms.TestPrimes;

/**
 * This class starts the euler-application
 */
public final class EulerAppLauncher {

  public static void main(String [] args) {
    runApp(args);
  }

  /**
   * Application specific run logic.
   * @param args command line arguments
   */
  private static void runApp(String [] args) {
    final IAlgorithm<Long, Boolean> isPrime = new TestPrimes();
    final Long value = 1000053L;
    final Long startTime = Calendar.getInstance().getTimeInMillis();

    System.out.println(String.format("The value %1$s %2$s prime.", value, (isPrime.execute(value) ? "is" : "is not")));

    final Long endTime = Calendar.getInstance().getTimeInMillis();

    System.out.println(String.format("Total time to determine whether %1$s was prime is %2$sms", value, (endTime - startTime)));
  }

  private EulerAppLauncher() {}

}

