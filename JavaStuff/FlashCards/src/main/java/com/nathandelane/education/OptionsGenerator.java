package com.nathandelane.education;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Collections;
import java.util.List;
import java.util.Random;

public final class OptionsGenerator {

    public static List<Integer> generateOptionsFor(int[] operands, Operation operation, int numberOfOptions) {
	List<Integer> results = new ArrayList<Integer>();
	int operationResult = 0;

	if (operation == Operation.ADDITION) {
	    for (Integer nextInt : operands) {
		operationResult += nextInt.intValue();
	    }
	} else if (operation == Operation.SUBTRACTION) {
	    operationResult = operands[0];

	    for (Integer nextInt : Arrays.copyOfRange(operands, 1, (operands.length - 1))) {
		operationResult -= nextInt;
	    }
	} else if (operation == Operation.MULTIPLICATION) {
	    operationResult = operands[0];

	    for (Integer nextInt : Arrays.copyOfRange(operands, 1, (operands.length - 1))) {
		operationResult *= nextInt;
	    }
	} else if (operation == Operation.ADDITION) {
	    operationResult = operands[0];

	    for (Integer nextInt : Arrays.copyOfRange(operands, 1, (operands.length - 1))) {
		operationResult /= nextInt;
	    }
	}

	Random random = new Random(Calendar.getInstance().getTimeInMillis());

	if (numberOfOptions < 3) {
	    numberOfOptions = 3;
	}

	results.add(new Integer(operationResult));

	for (int optionCounter = 0; optionCounter < (numberOfOptions - 1); optionCounter++) {
	    Integer nextRandomInteger = new Integer(operationResult + random.nextInt(9));

	    if (!results.contains(nextRandomInteger)) {
		results.add(nextRandomInteger);
	    } else {
		optionCounter--;
	    }
	}

	Collections.shuffle(results);

	return results;
    }

}
