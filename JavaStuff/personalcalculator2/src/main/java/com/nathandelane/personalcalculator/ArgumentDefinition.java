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

/**
 * An argument definition includes an argument and and execution method. Execution might set a context
 * value or display some text for example.
 * @author nathanlane
 *
 */
public abstract class ArgumentDefinition {

    private String argument;

    public ArgumentDefinition(String argument) {
	this.argument = argument;
    }

    /**
     * Executes the argument definition.
     */
    public abstract void execute(String argumentValue);

}
