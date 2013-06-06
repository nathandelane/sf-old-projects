package com.nathandelane.euler.app.algorithms;

import java.util.List;

import com.google.common.collect.Lists;

public class TestPrimes implements IAlgorithm<Long, Boolean> {

  private static final List<Long> PRIMES = Lists.newArrayList(2L, 3L, 5L, 7L, 11L, 13L, 17L, 19L, 23L, 29L, 31L, 37L,
    41L, 43L, 47L, 53L, 59L, 61L, 67L, 73L, 79L, 83L, 89L, 97L, 101L, 103L, 107L, 109L, 113L, 127L, 131L, 137L, 139L,
    149L, 151L, 157L, 163L, 167L, 173L, 179L, 181L, 191L, 193L, 197L, 199L, 211L, 223L, 227L, 229L, 233L, 239L, 241L,
    251L, 257L, 263L, 269L, 271L, 277L, 281L, 283L, 293L, 307L, 311L, 313L, 317L, 331L, 337L, 347L, 349L, 353L, 359L,
    367L, 373L, 379L, 383L, 389L, 397L, 401L, 409L, 419L, 421L, 431L, 433L, 439L, 443L, 449L, 457L, 461L, 463L, 467L,
    479L, 487L, 491L, 499L, 503L, 509L, 521L, 523L, 541L, 547L, 557L, 563L, 569L, 571L, 577L, 587L, 593L, 599L, 601L,
    607L, 613L, 617L, 619L, 631L, 641L, 643L, 647L, 653L, 659L, 661L, 673L, 677L, 683L, 691L, 701L, 709L, 719L, 727L,
    733L, 739L, 743L, 751L, 757L, 761L, 769L, 773L, 787L, 797L, 809L, 811L, 821L, 823L, 827L, 829L, 839L, 853L, 857L,
    859L, 863L, 877L, 881L, 883L, 887L, 907L, 911L, 919L, 929L, 937L, 941L, 947L, 953L, 967L, 971L, 977L, 983L, 991L,
    997L, 1009L, 1013L, 1019L, 1021L, 1031L, 1033L, 1039L, 1049L, 1051L, 1053L, 1061L, 1063L, 1069L, 1087L, 1091L,
    1093L, 1097L, 1103L, 1109L, 1117L, 1123L, 1129L, 1151L, 1153L, 1163L, 1171L, 1181L, 1187L, 1193L, 1201L, 1213L,
    1217L, 1223L, 1229L, 1231L, 1237L, 1249L, 1259L, 1277L, 1279L, 1283L, 1289L, 1291L, 1297L, 1301L, 1303L, 1307L,
    1319L, 1321L, 1327L, 1361L, 1367L, 1373L, 1381L, 1399L, 1409L, 1423L, 1427L, 1429L, 1433L, 1439L, 1447L, 1451L,
    1453L, 1459L, 1471L, 1481L, 1483L, 1487L, 1489L, 1493L, 1499L, 1511L, 1523L, 1531L, 1543L, 1549L, 1553L, 1559L,
    1567L, 1571L, 1579L, 1583L, 1597L, 1601L, 1607L, 1609L, 1613L, 1619L, 1621L, 1627L, 1637L, 1657L, 1663L, 1667L,
    1669L, 1693L, 1697L, 1699L, 1709L, 1721L, 1723L, 1733L, 1741L, 1747L, 1753L, 1759L, 1777L, 1783L, 1787L, 1789L,
    1801L, 1811L, 1823L, 1831L, 1847L, 1861L, 1867L, 1871L, 1873L, 1877L, 1879L, 1889L, 1901L, 1907L, 1913L, 1931L,
    1933L, 1949L, 1951L, 1973L, 1979L, 1987L, 1993L, 1997L, 1999L, 2003L, 2011L, 2017L, 2027L, 2029L, 2039L, 2053L,
    2063L, 2069L, 2081L, 2083L, 2087L, 2089L, 2099L, 2111L, 2113L, 2129L, 2131L, 2137L, 2141L, 2143L, 2153L, 2161L,
    2179L, 2203L, 2207L, 2213L, 2221L, 2237L, 2239L, 2243L, 2251L, 2267L, 2269L, 2273L, 2281L, 2287L, 2293L, 2297L,
    2309L, 2311L, 2333L, 2339L, 2341L, 2347L, 2351L, 2357L, 2371L, 2377L, 2381L, 2383L, 2389L, 2393L, 2399L, 2411L,
    2417L, 2423L, 2437L, 2441L, 2447L, 2459L, 2467L, 2473L, 2477L, 2503L, 2521L, 2531L, 2539L, 2543L, 2549L, 2551L,
    2557L, 2579L, 2591L, 2593L, 2609L, 2617L, 2621L, 2633L, 2647L, 2657L, 2659L, 2663L, 2671L, 2677L, 2683L, 2687L,
    2689L, 2693L, 2699L, 2707L, 2711L, 2713L, 2719L, 2729L, 2731L, 2741L, 2749L, 2753L, 2767L, 2777L, 2789L, 2791L,
    2797L, 2801L, 2803L, 2819L, 2833L, 2837L, 2843L, 2851L, 2857L, 2861L, 2879L, 2887L, 2897L, 2903L, 2909L, 2917L,
    2927L, 2939L, 2953L, 2957L, 2963L, 2969L, 2971L, 2999L, 3001L, 3011L, 3019L, 3023L, 3037L, 3041L, 3049L, 3061L,
    3067L, 3079L, 3083L, 3089L, 3109L, 3119L, 3121L, 3137L, 3163L, 3167L, 3169L, 3181L, 3187L, 3191L, 3203L, 3209L,
    3217L, 3221L, 3229L, 3251L, 3253L, 3257L, 3259L, 3271L, 3299L, 3301L, 3307L, 3313L, 3319L, 3323L, 3329L, 3331L,
    3343L, 3347L, 3359L, 3361L, 3371L, 3373L, 3389L, 3391L, 3407L, 3413L, 3433L, 3449L, 3457L, 3461L, 3463L, 3467L,
    3469L, 3491L, 3499L, 3511L, 3517L, 3527L, 3529L, 3533L, 3539L, 3541L, 3547L, 3557L, 3559L, 3571L);

  @Override
  public Boolean execute(Long value) {
    boolean isPrime = false;

    if (value != null && value > 1) {
      if (PRIMES.contains(value)) {
        isPrime = true;
      }
      else if (value > PRIMES.get(PRIMES.size() - 1)) {
        Long counter = PRIMES.get(PRIMES.size() - 1);

        while (counter < value) {
          counter++;

          if (counter % 2 == 1) {
            boolean innerIsPrime = true;

            for (Long nextPrime : PRIMES) {
              if (counter == value) {
                System.out.println(String.format("%1$s %% %2$s = %3$s", counter, nextPrime, (counter % nextPrime)));
              }

              if (counter == value) {
                System.out.println(String.format("%1$s %% %2$s = %3$s", counter, nextPrime, (counter % nextPrime)));
              }

              if (counter == value) {
                System.out.println(String.format("%1$s %% %2$s = %3$s", counter, nextPrime, (counter % nextPrime)));
              }

              if (counter % nextPrime == 0L) {
                innerIsPrime = false;

                break;
              }
            }

            if (innerIsPrime) {
              PRIMES.add(counter);
            }
          }
        }

        if (PRIMES.contains(value)) {
          isPrime = true;
        }
      }
    }

    return isPrime;
  }

}
