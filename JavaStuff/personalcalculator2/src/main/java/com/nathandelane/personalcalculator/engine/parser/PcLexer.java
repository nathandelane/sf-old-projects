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

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.nathandelane.personalcalculator.engine.AbstractLexer;

/**
 * Lexes or parses an expression into tokens, one at a time. The responsibility of the ITokenCollector is to
 * remove tokens from the expression so that the lexer doesn't duplicate tokens on inadvertently.
 * @author nathanlane
 *
 */
public final class PcLexer extends AbstractLexer<PcToken> {

    /**
     * Lexer token definitions.
     */
    @SuppressWarnings("serial")
    private static final List<LexicalEntry> TOKENS = new ArrayList<LexicalEntry>() {
	{
	    add(new LexicalEntry(Pattern.compile("^[\\d]+([.]{1}[\\d]+){0,1}"), PcTokenType.NUMBER));
	    add(new LexicalEntry(Pattern.compile("^([-])"), PcTokenType.SUBTRACTION_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(\\+)"), PcTokenType.ADDITION_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(\\*\\*)"), PcTokenType.POWER_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(\\*)"), PcTokenType.MULTIPLICATION_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(//)"), PcTokenType.DIV_MOD_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(/)"), PcTokenType.DIVISION_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(%)"), PcTokenType.MODULUS_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(\\^)"), PcTokenType.XOR_BITWISE_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(&&)"), PcTokenType.AND_LOGICAL_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(\\|\\|)"), PcTokenType.OR_LOGICAL_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(&)"), PcTokenType.AND_BITWISE_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(\\|)"), PcTokenType.OR_BITWISE_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(==)"), PcTokenType.EQUALITY_BOOLEAN_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(<=)"), PcTokenType.LESS_THAN_OR_EQUAL_TO_BOOLEAN_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(>=)"), PcTokenType.GREATER_THAN_OR_EQUAL_TO_BOOLEAN_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(<>|!=)"), PcTokenType.INEQUALITY_BOOLEAN_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(=)"), PcTokenType.ASSIGNMENT_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(<)"), PcTokenType.LESS_THAN_BOOLEAN_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(>)"), PcTokenType.GREATER_THAN_BOOLEAN_OPERATOR));
	    add(new LexicalEntry(Pattern.compile("^(\\()"), PcTokenType.LEFT_PARENTHESIS));
	    add(new LexicalEntry(Pattern.compile("^(\\))"), PcTokenType.RIGHT_PARENTHESIS));
	    add(new LexicalEntry(Pattern.compile("^(,)"), PcTokenType.ARGUMENT_DELIMITER));
	    add(new LexicalEntry(Pattern.compile("^(;)"), PcTokenType.EXPRESSION_DELIMITER));
	    add(new LexicalEntry(Pattern.compile("^(\\.\\.)"), PcTokenType.RANGE_DELIMITER));
	    add(new LexicalEntry(Pattern.compile("^(\\\\)"), PcTokenType.NEW_LINE_DELIMITER));
	    add(new LexicalEntry(Pattern.compile("^(\\[)"), PcTokenType.LEFT_SQUARE_BRACKET));
	    add(new LexicalEntry(Pattern.compile("^(\\])"), PcTokenType.RIGHT_SQUARE_BRACKET));
	    add(new LexicalEntry(Pattern.compile("^(!)"), PcTokenType.FACTORIAL_FUNCTION));
	    add(new LexicalEntry(Pattern.compile("^[a-z]{1}[a-zA-z_\\d]{2,}"), PcTokenType.FUNCTION));
	    add(new LexicalEntry(Pattern.compile("^\\$[\\$a-zA-z_\\d]*"), PcTokenType.VARIABLE));
	}
    };

    private static PcLexer instance;

    private PcLexer() {
	// Left intentionally blank.
    }

    /**
     * Parses the next token out of the given expression.
     * @param expression
     * @return
     */
    public PcToken parseNextToken(String expression) {
	PcToken nextToken = PcToken.NULL_TOKEN;

	for (LexicalEntry nextEntry : PcLexer.TOKENS){
	    Pattern nextPattern = nextEntry.getPattern();

	    if (nextPattern.matcher(expression).find()) {
		Matcher patternMatcher = nextPattern.matcher(expression);
		patternMatcher.find();

		String actual = patternMatcher.group(0);

		nextToken = new PcToken(actual, nextEntry.getType());

		break;
	    }
	}

	return nextToken;
    }

    /**
     * Gets a reference to the instance of PcLexer.
     * @return
     */
    public static PcLexer getInstance() {
	if (PcLexer.instance == null) {
	    PcLexer.instance = new PcLexer();
	}

	return PcLexer.instance;
    }

}
