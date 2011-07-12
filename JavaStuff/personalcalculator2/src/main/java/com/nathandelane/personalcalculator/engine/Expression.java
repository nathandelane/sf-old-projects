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

/**
 * Expression is a data class for historical purposes.
 * @author nathanlane
 *
 */
public class Expression {

    private String expression;

    public Expression(String expression) {
	this.expression = expression;
    }

    /**
     * Returns a string representation of this expression.
     */
    @Override
    public String toString() {
	return this.expression;
    }

    /**
     * Returns whether this expression is the same as another expression.
     * @param other
     * @return
     */
    @Override
    public boolean equals(Object other) {
	boolean result = false;

	if (other instanceof Expression) {
	    if (this.expression.equals(((Expression)other).expression)) {
		result = true;
	    }
	}

	return result;
    }

    /**
     * Returns a hashcode for this expression.
     */
    @Override
    public int hashCode() {
	return (31 * this.expression.hashCode());
    }

}
