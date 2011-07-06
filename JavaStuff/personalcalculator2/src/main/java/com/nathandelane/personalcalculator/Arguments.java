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

package com.nathandelane.personalcalculator;

import java.util.HashMap;
import java.util.Map;

import com.nathandelane.personalcalculator.engine.CalculatorContext;

/**
 * Parses command-line arguments into a context-expressive format ready for personal calculator to consume.
 * @author nathanlane
 *
 */
public final class Arguments {

    private static Arguments instance;

    private Map<String, ArgumentDefinition> argumentDefinitions;
    private CalculatorContext context;

    private Arguments() {
	this.argumentDefinitions = new HashMap<String, ArgumentDefinition>();
	this.context = CalculatorContext.get();
    }

    /**
     * Sets a definition for an argument.
     * @param argument
     * @param argumentDefinition
     */
    public void addArgumentDefinition(String argument, ArgumentDefinition argumentDefinition) {
	this.argumentDefinitions.put(argument, argumentDefinition);
    }

    /**
     * Sets all argument definitions.
     * @param argumentDefinitions
     */
    public void addArgumentDefinitions(Map<String, ArgumentDefinition> argumentDefinitions) {
	this.argumentDefinitions = argumentDefinitions;
    }

    /**
     * Gets the Argument Definition for a specified argument.
     * @param argument
     * @return
     * @throws ArgumentException
     */
    public ArgumentDefinition getArgumentDefinition(String argument) throws ArgumentException {
	if (!this.argumentDefinitions.containsKey(argument)) {
	    throw new ArgumentException(String.format("Argument value for argument (%1$s) does not have a definition", argument));
	}

	return this.argumentDefinitions.get(argument);
    }

    /**
     * Removes an Argument Definition from the arguments list.
     * @param argument
     * @return
     */
    public ArgumentDefinition removeArgumentDefinition(String argument) {
	return this.argumentDefinitions.get(argument);
    }

    /**
     * Removes all Argument Definitions from the arguments list.
     */
    public void clearAllDefinitions() {
	this.argumentDefinitions.clear();
    }

    /**
     * Parses a collection of arguments, executing their argument definitions.
     * @param args
     * @throws CliArgumentException
     */
    public void parseArguments(String[] args) throws CliArgumentException {
	for (String nextArg : args) {
	    executeArgumentDefinition(nextArg);
	}
    }

    /**
     * Executes the argument definition if the argument is defined or throws a CliArgumentException if it isn't.
     * @param argument
     * @throws CliArgumentException
     */
    private void executeArgumentDefinition(String argument) throws CliArgumentException {
	if (this.argumentDefinitions.containsKey(argument)) {
	    this.argumentDefinitions.get(argument).execute(argument);
	} else {
	    throw new CliArgumentException(String.format("The command-line argument \"%1$s\" is not recognized as a valid argument.", argument));
	}
    }

    /**
     * Gets a copy of the reference to the singleton instance.
     * @return
     */
    public static Arguments getArgumentParser() {
	if (Arguments.instance == null) {
	    Arguments.instance = new Arguments();
	}

	return Arguments.instance;
    }

}
