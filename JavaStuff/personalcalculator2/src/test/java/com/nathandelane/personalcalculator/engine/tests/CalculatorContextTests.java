package com.nathandelane.personalcalculator.engine.tests;

import junit.framework.TestCase;

import org.junit.Test;

import com.nathandelane.personalcalculator.engine.CalculatorContext;
import com.nathandelane.personalcalculator.engine.Expression;

/**
 * Tests the CalculatorContext class and methods.
 * @author nathanlane
 *
 */
public class CalculatorContextTests extends TestCase {

    /**
     * Tests getValue(String name) after setting a value.
     */
    @Test
    public void testGetValue() {
	String name = "$";
	String result = "42";
	CalculatorContext.get().setValue(name, result);

	String getResult = (String)CalculatorContext.get().getValue(name);

	assertEquals(result, getResult);
    }

    /**
     * Tests getValue(Class clazz, String name) after setting a value.
     */
    @Test
    public void testGetValueAs() {
	String name = "$";
	String result = "1025.293";
	CalculatorContext.get().setValue(name, result);

	String getResult = CalculatorContext.get().getValueAs(String.class, name);

	assertEquals(result, getResult);
    }

    /**
     * Tests setValue(String name, Object value) by setting the same name-value pair
     * twice with a different value.
     */
    @Test
    public void testSetValue() {
	String name = "$variable";
	String value = "293";
	CalculatorContext.get().setValue(name, value);

	assertEquals(value, CalculatorContext.get().getValueAs(String.class, name));

	value = "True";
	CalculatorContext.get().setValue(name, value);

	assertEquals(value, CalculatorContext.get().getValueAs(String.class, name));
    }

    /**
     * Tests whether the expression history contains the expression I entered into it
     * after adding it.
     */
    @Test
    public void testAddExpressionToHistory() {
	Expression expression = new Expression("24+13 / 12");
	CalculatorContext.get().addExpressionToHistory(expression);
	boolean expressionFound = false;

	for (Expression nextExpression : CalculatorContext.get().getExpressionHistory()) {
	    if (expression.equals(nextExpression)) {
		expressionFound = true;

		break;
	    }
	}

	assertTrue(expressionFound);
    }

    /**
     * Tests whether the error history contains the error I entered into it
     * after adding it.
     */
    @Test
    public void testAddErrorToHistory() {
	Error error = new Error("Testing errors");
	CalculatorContext.get().addErrorToHistory(error);
	boolean errorFound = false;

	for (Error nextError : CalculatorContext.get().getErrorHistory()) {
	    if (error.getMessage().equals(nextError.getMessage())) {
		errorFound = true;

		break;
	    }
	}

	assertTrue(errorFound);
    }

    /**
     * Tests the singleton get() method.
     */
    @Test
    public void testGet() {
	CalculatorContext context = CalculatorContext.get();

	assertNotNull(context);
    }

}
