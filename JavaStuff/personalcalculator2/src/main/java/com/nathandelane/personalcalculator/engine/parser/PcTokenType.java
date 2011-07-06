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

public enum PcTokenType {

    NULL,
    NUMBER,
    SUBTRACTION_OPERATOR,
    NEGATION_OPERATOR,
    ADDITION_OPERATOR,
    DIVISION_OPERATOR,
    MULTIPLICATION_OPERATOR,
    DIV_MOD_OPERATOR,
    MODULUS_OPERATOR,
    POWER_OPERATOR,
    XOR_BITWISE_OPERATOR,
    OR_BITWISE_OPERATOR,
    AND_BITWISE_OPERATOR,
    OR_LOGICAL_OPERATOR,
    AND_LOGICAL_OPERATOR,
    ASSIGNMENT_OPERATOR,
    EQUALITY_BOOLEAN_OPERATOR,
    INEQUALITY_BOOLEAN_OPERATOR,
    LESS_THAN_BOOLEAN_OPERATOR,
    GREATER_THAN_BOOLEAN_OPERATOR,
    LESS_THAN_OR_EQUAL_TO_BOOLEAN_OPERATOR,
    GREATER_THAN_OR_EQUAL_TO_BOOLEAN_OPERATOR,
    LEFT_PARENTHESIS,
    RIGHT_PARENTHESIS,
    ARGUMENT_DELIMITER,
    EXPRESSION_DELIMITER,
    RANGE_DELIMITER,
    LEFT_SQUARE_BRACKET,
    RIGHT_SQUARE_BRACKET,
    FUNCTION,
    VARIABLE,
    NEW_LINE_DELIMITER,
    FACTORIAL_FUNCTION

}
