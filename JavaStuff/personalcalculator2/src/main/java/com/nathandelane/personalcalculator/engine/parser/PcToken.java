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

package com.nathandelane.personalcalculator.engine.parser;

/**
 * Default token definition for personal calculator.
 * @author nathanlane
 *
 */
public class PcToken {

    public static final PcToken NULL_TOKEN = new PcToken("", PcTokenType.NULL);

    private String value;
    private PcTokenType type;

    public PcToken(String value, PcTokenType type) {
	this.value = value;
	this.type = type;
    }

    /**
     * Gets the type of this token.
     * @return
     */
    public PcTokenType getType() {
	return this.type;
    }

    /**
     * Gets the value of this token.
     * @return
     */
    public String getValue() {
	return this.value;
    }

    /**
     * Returns a string-representation of PcToken including its value and type.
     */
    @Override
    public String toString() {
	return String.format("{ Value=\"%1$s\", Type=\"%1$s\" }", this.value, this.type);
    }

    /**
     * Determines whether this PcToken is equal in value to another PcToken.
     * @param other
     * @return
     */
    public boolean equals(PcToken other) {
	return (this.value.equals(other.value) && this.type == other.type);
    }

}
