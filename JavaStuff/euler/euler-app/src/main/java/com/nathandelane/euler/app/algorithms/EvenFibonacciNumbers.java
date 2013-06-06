package com.nathandelane.euler.app.algorithms;

import java.math.BigInteger;

public class EvenFibonacciNumbers implements IAlgorithm<BigInteger, String> {

  @Override
  public String execute(BigInteger maximum) {
    final BigInteger TWO = new BigInteger("2");

    BigInteger i = BigInteger.ZERO;
    BigInteger last = BigInteger.ONE;
    BigInteger current = TWO;

    while (i.compareTo(maximum) < 0) {
      if (current.mod(TWO) == BigInteger.ZERO) {
        i = i.add(current);

        if (current.compareTo(TWO) > 0) {
          System.out.print(", ");
        }

        System.out.print(current.toString());
      }

      BigInteger temp = current;

      current = last.add(current);
      last = temp;
    }

    return i.toString();
  }

}
