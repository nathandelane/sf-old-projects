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

package com.nathandelane.personalcalculator.engine.parser.tests;

import java.util.Stack;

import junit.framework.TestCase;

import org.junit.Test;

import com.nathandelane.personalcalculator.engine.parser.PcLexer;
import com.nathandelane.personalcalculator.engine.parser.PcToken;
import com.nathandelane.personalcalculator.engine.parser.PcTokenType;

/**
 * Tests for the Lexer.
 * @author nathanlane
 *
 */
public class PcLexerTests extends TestCase {

    private PcLexer lexer;

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Numbers.
     */
    @Test
    public void testKnownValidTokensNumbers() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.NUMBER;

	String number = "43";
	PcToken tokenFromNumber = this.lexer.parseNextToken(number);

	assertEquals(number, tokenFromNumber.getValue());
	assertTrue(tokenFromNumber.getType() == expectedTokenType);

	String decimalNumber = "42.109";
	PcToken tokenFromDecimalNumberToken = this.lexer.parseNextToken(decimalNumber);

	assertEquals(decimalNumber, tokenFromDecimalNumberToken.getValue());
	assertTrue( tokenFromDecimalNumberToken.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Operators.
     */
    @Test
    public void testKnownValidTokensOperators() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.SUBTRACTION_OPERATOR;

	String value = "-";
	PcToken tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);

	expectedTokenType = PcTokenType.DIVISION_OPERATOR;

	value = "/";
	tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);

	expectedTokenType = PcTokenType.DIV_MOD_OPERATOR;

	value = "//";
	tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);

	expectedTokenType = PcTokenType.OR_LOGICAL_OPERATOR;

	value = "||";
	tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);

	expectedTokenType = PcTokenType.AND_LOGICAL_OPERATOR;

	value = "&&";
	tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);

	expectedTokenType = PcTokenType.EQUALITY_BOOLEAN_OPERATOR;

	value = "==";
	tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known invalid tokens against the lexer to ensure that the
     * lexer returns a null-token.
     */
    @Test
    public void testKnownInvalidTokens() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.NULL;

	String value = "@123";
	PcToken tokenFromInvalidValue = this.lexer.parseNextToken(value);

	assertEquals("", tokenFromInvalidValue.getValue());
	assertTrue(tokenFromInvalidValue.getType() == expectedTokenType);
    }

    /**
     * Test multiple tokens parsed from a single expression.
     */
    @Test
    public void testMultipleTokens() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	Stack<PcToken> tokenStack = new Stack<PcToken>();
	Stack<PcToken> expectedTokenStack = new Stack<PcToken>() {
	    {
		push(new PcToken("1", PcTokenType.NUMBER));
		push(new PcToken("+", PcTokenType.ADDITION_OPERATOR));
		push(new PcToken("21", PcTokenType.NUMBER));
	    }
	};
	String expression = "1+21";

	while (!expression.equals("")) {
	    PcToken nextToken = this.lexer.parseNextToken(expression);

	    if (!nextToken.equals(PcToken.NULL_TOKEN)) {
		tokenStack.push(nextToken);

		expression = expression.substring(nextToken.getValue().length());
	    }
	}

	while (tokenStack.size() > 0 && expectedTokenStack.size() > 0) {
	    PcToken nextActualToken = tokenStack.pop();
	    PcToken nextExpectedToken = expectedTokenStack.pop();

	    assertTrue(nextActualToken.equals(nextExpectedToken));
	}
    }

}
