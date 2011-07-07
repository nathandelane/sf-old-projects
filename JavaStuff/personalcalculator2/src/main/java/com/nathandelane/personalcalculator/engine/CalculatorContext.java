/**
 * Personal Calculator is a text-mode calculator and functional programming
 * system.
 * Copyright (C) 2011 Nathandelane, Nathandelane.com
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

package com.nathandelane.personalcalculator.engine;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.InvalidPropertiesFormatException;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Properties;

/**
 * Global context for the calculator. This context maintains a history of expressions and results, including the
 * last result and some constants and definitions for custom functions. All values are stored as Object and must
 * be cast into their respective types.
 * @author nathanlane
 *
 */
public final class CalculatorContext {

    public static final String UNITS_KEY = "units";
    public static final String PROMPT_KEY = "prompt";
    public static final String CONFIGURATION_FILE_NAME = "PersonalCalculator.xml";

    private static final String CONFIGURATION_INVALID_PROPERTIES_EXCEPTION_MESSAGE = String.format("Error: Your %1$s file has an invalid format.", CalculatorContext.CONFIGURATION_FILE_NAME);
    private static final String CONFIGURATION_FILE_NOT_FOUND_EXCEPTION_MESSAGE = String.format("Warning: Configuration file (%1$s) not found. Continuing on with defaults...", CalculatorContext.CONFIGURATION_FILE_NAME);
    private static final String CONFIGURATION_IO_EXCEPTION_MESSAGE = String.format("Warning: Could not read configuration file (%1$s) for some reason. Continuing on with defaults...", CalculatorContext.CONFIGURATION_FILE_NAME);

    private static CalculatorContext instance;

    private Map<String, Object> context;
    private List<Expression> expressionHistory;
    private List<Error> errorHistory;
    private PromptModel promptModel;

    private CalculatorContext() {
	this.context = new HashMap<String, Object>();
	this.expressionHistory = new ArrayList<Expression>();
	this.errorHistory = new ArrayList<Error>();

	try {
	    Properties properties = new Properties();
	    properties.loadFromXML(new FileInputStream("PersonalCalculator.xml"));
	    promptModel = new PromptModel(properties.getProperty(CalculatorContext.PROMPT_KEY));
	} catch (InvalidPropertiesFormatException e) {
	    System.err.println(CalculatorContext.CONFIGURATION_INVALID_PROPERTIES_EXCEPTION_MESSAGE);
	} catch (FileNotFoundException e) {
	    System.err.println(CalculatorContext.CONFIGURATION_FILE_NOT_FOUND_EXCEPTION_MESSAGE);
	} catch (IOException e) {
	    System.err.println(CalculatorContext.CONFIGURATION_IO_EXCEPTION_MESSAGE);
	}
    }

    /**
     * Gets a value from the context.
     * @param name Name of the value.
     * @return Value associated with name, null if no reference exists.
     */
    public Object getValue(String name) {
	return this.context.get(name);
    }

    /**
     * Gets a value from the context for the key name and casts it to type T.
     * @param <T>
     * @param clazz Type to cast the value to.
     * @param name Name of the value.
     * @return Value associated with name, null if no reference exists.
     */
    @SuppressWarnings("unchecked")
    public <T> T getValueAs(Class<T> clazz, String name) {
	return (T)this.context.get(name);
    }

    /**
     * Sets a value in the context.
     * @param name Name of the value.
     * @param value Value to be set.
     */
    public void setValue(String name, Object value) {
	this.context.put(name, value);
    }

    /**
     * Adds an expression to the expression history.
     * @param expression
     */
    public void addExpressionToHistory(Expression expression) {
	this.expressionHistory.add(expression);
    }

    /**
     * Gets the complete expression history.
     * @return
     */
    public List<Expression> getExpressionHistory() {
	return this.expressionHistory;
    }

    /**
     * Adds an error to the error history.
     * @param error
     */
    public void addErrorToHistory(Error error) {
	this.errorHistory.add(error);
    }

    /**
     * Gets the complete error history.
     * @return
     */
    public List<Error> getErrorHistory() {
	return this.errorHistory;
    }

    /**
     * Gets the prompt model.
     * @return
     */
    public PromptModel getPromptModel() {
	return this.promptModel;
    }

    /**
     * Sets the prompt model.
     * @param prompt
     */
    public void setPromptModel(String prompt) {
	this.promptModel = new PromptModel(prompt);
    }

    /**
     * Sets the prompt model.
     * @param promptModel
     */
    public void setPromptModel(PromptModel promptModel) {
	this.promptModel = promptModel;
    }

    /**
     * Gets this singleton instance for CalculatorContext.
     * @return
     */
    public static CalculatorContext get() {
	if (CalculatorContext.instance == null) {
	    CalculatorContext.instance = new CalculatorContext();
	}

	return CalculatorContext.instance;
    }

}
