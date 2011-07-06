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

import java.util.HashMap;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Lexes or parses an expression into tokens, one at a time. The responsibility of the ITokenCollector is to
 * remove tokens from the expression so that the lexer doesn't duplicate tokens on inadvertently.
 * @author nathanlane
 *
 */
public final class PcLexer {

    /**
     * Lexer token definitions.
     */
    @SuppressWarnings("serial")
    private static final Map<Pattern, PcTokenType> tokens = new HashMap<Pattern, PcTokenType>() {
	{
	    put(Pattern.compile("^[\\d]+([\\.]{1}[\\d]*){0,1}"), PcTokenType.NUMBER);
	    put(Pattern.compile("^([-])"), PcTokenType.SUBTRACTION_OPERATOR);
	    put(Pattern.compile("^(\\+)"), PcTokenType.ADDITION_OPERATOR);
	    put(Pattern.compile("^(\\*\\*)"), PcTokenType.POWER_OPERATOR);
	    put(Pattern.compile("^(\\*)"), PcTokenType.MULTIPLICATION_OPERATOR);
	    put(Pattern.compile("^(//)"), PcTokenType.DIV_MOD_OPERATOR);
	    put(Pattern.compile("^(/)"), PcTokenType.DIVISION_OPERATOR);
	    put(Pattern.compile("^(%)"), PcTokenType.MODULUS_OPERATOR);
	    put(Pattern.compile("^(\\^)"), PcTokenType.XOR_BITWISE_OPERATOR);
	    put(Pattern.compile("^(&&)"), PcTokenType.AND_LOGICAL_OPERATOR);
	    put(Pattern.compile("^(\\|\\|)"), PcTokenType.OR_LOGICAL_OPERATOR);
	    put(Pattern.compile("^(&)"), PcTokenType.AND_BITWISE_OPERATOR);
	    put(Pattern.compile("^(\\|)"), PcTokenType.OR_BITWISE_OPERATOR);
	    put(Pattern.compile("^(==)"), PcTokenType.EQUALITY_BOOLEAN_OPERATOR);
	    put(Pattern.compile("^(<=)"), PcTokenType.LESS_THAN_OR_EQUAL_TO_BOOLEAN_OPERATOR);
	    put(Pattern.compile("^(>=)"), PcTokenType.GREATER_THAN_OR_EQUAL_TO_BOOLEAN_OPERATOR);
	    put(Pattern.compile("^(<>|!=)"), PcTokenType.INEQUALITY_BOOLEAN_OPERATOR);
	    put(Pattern.compile("^(=)"), PcTokenType.ASSIGNMENT_OPERATOR);
	    put(Pattern.compile("^(<)"), PcTokenType.LESS_THAN_BOOLEAN_OPERATOR);
	    put(Pattern.compile("^(>)"), PcTokenType.GREATER_THAN_BOOLEAN_OPERATOR);
	    put(Pattern.compile("^(\\()"), PcTokenType.LEFT_PARENTHESIS);
	    put(Pattern.compile("^(\\))"), PcTokenType.RIGHT_PARENTHESIS);
	    put(Pattern.compile("^(,)"), PcTokenType.ARGUMENT_DELIMITER);
	    put(Pattern.compile("^(;)"), PcTokenType.EXPRESSION_DELIMITER);
	    put(Pattern.compile("^(\\.\\.)"), PcTokenType.RANGE_DELIMITER);
	    put(Pattern.compile("^(\\\\)"), PcTokenType.NEW_LINE_DELIMITER);
	    put(Pattern.compile("^(\\[)"), PcTokenType.LEFT_SQUARE_BRACKET);
	    put(Pattern.compile("^(\\])"), PcTokenType.RIGHT_SQUARE_BRACKET);
	    put(Pattern.compile("^(!)"), PcTokenType.FACTORIAL_FUNCTION);
	    put(Pattern.compile("^[a-z]{1}[a-zA-z_\\d]{2,}"), PcTokenType.FUNCTION);
	    put(Pattern.compile("^\\$[\\$a-zA-z_\\d]*"), PcTokenType.VARIABLE);
	}
    };

    /**
     * Parses the next token out of the given expression.
     * @param expression
     * @return
     */
    public static PcToken parseNextToken(String expression) {
	PcToken nextToken = new PcToken("", PcTokenType.NULL);

	for (Pattern nextPattern : PcLexer.tokens.keySet()){
	    if (nextPattern.matcher(expression).matches()) {
		Matcher patternMatcher = nextPattern.matcher(expression);

		if (patternMatcher.find()) {
		    String actual = patternMatcher.group(0);

		    nextToken = new PcToken(actual, PcLexer.tokens.get(nextPattern));

		    break;
		}
	    }
	}

	return nextToken;
    }

}
