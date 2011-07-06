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
     * ensure that the lexer is behaving as expected. This one tests Numbers -
     * Integers.
     */
    @Test
    public void testKnownValidTokensNumbersInteger() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.NUMBER;

	String number = "43";
	PcToken tokenFromNumber = this.lexer.parseNextToken(number);

	assertEquals(number, tokenFromNumber.getValue());
	assertTrue(tokenFromNumber.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Numbers -
     * Decimals.
     */
    @Test
    public void testKnownValidTokensNumbersDecimal() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.NUMBER;

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
    public void testKnownValidTokensOperatorsSubtraction() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.SUBTRACTION_OPERATOR;

	String value = "-";
	PcToken tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Operators.
     */
    @Test
    public void testKnownValidTokensOperatorsDivision() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.DIVISION_OPERATOR;

	String value = "/";
	PcToken tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Operators.
     */
    @Test
    public void testKnownValidTokensOperatorsDivMod() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.DIV_MOD_OPERATOR;

	String value = "//";
	PcToken tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Operators.
     */
    @Test
    public void testKnownValidTokensOperatorsOrLogical() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.OR_LOGICAL_OPERATOR;

	String value = "||";
	PcToken tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Operators.
     */
    @Test
    public void testKnownValidTokensOperatorsAndLogical() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.AND_LOGICAL_OPERATOR;

	String value = "&&";
	PcToken tokenFromValue = this.lexer.parseNextToken(value);

	assertEquals(value, tokenFromValue.getValue());
	assertTrue(tokenFromValue.getType() == expectedTokenType);
    }

    /**
     * Tests a series of known valid tokens and token types against the lexer to
     * ensure that the lexer is behaving as expected. This one tests Operators.
     */
    @Test
    public void testKnownValidTokensOperatorsEqualityBoolean() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	PcTokenType expectedTokenType = PcTokenType.EQUALITY_BOOLEAN_OPERATOR;

	String value = "==";
	PcToken tokenFromValue = this.lexer.parseNextToken(value);

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
    public void testMultipleTokensTwoArgumentAddition() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	Stack<PcToken> expectedTokenStack = new Stack<PcToken>() {
	    {
		push(new PcToken("1", PcTokenType.NUMBER));
		push(new PcToken("+", PcTokenType.ADDITION_OPERATOR));
		push(new PcToken("21", PcTokenType.NUMBER));
	    }
	};
	String expression = "1+21";

	Stack<PcToken> actualTokenStack = parseExpression(expression);

	assertTrue(stacksAreEqual(expectedTokenStack, actualTokenStack));
    }

    /**
     * Test multiple tokens parsed from a single expression.
     */
    @Test
    public void testMultipleTokensTwoNegativeArgumentAddition() {
	if (this.lexer == null) {
	    this.lexer = PcLexer.getInstance();
	}

	Stack<PcToken> expectedTokenStack = new Stack<PcToken>() {
	    {
		push(new PcToken("-", PcTokenType.SUBTRACTION_OPERATOR));
		push(new PcToken("1", PcTokenType.NUMBER));
		push(new PcToken("+", PcTokenType.ADDITION_OPERATOR));
		push(new PcToken("-", PcTokenType.SUBTRACTION_OPERATOR));
		push(new PcToken("21", PcTokenType.NUMBER));
	    }
	};
	String expression = "-1+-21";

	Stack<PcToken> actualTokenStack = parseExpression(expression);

	assertTrue(stacksAreEqual(expectedTokenStack, actualTokenStack));
    }

    /**
     * Parses an expression into a Stack of tokens.
     * @param expression
     * @return
     */
    private Stack<PcToken> parseExpression(String expression) {
	Stack<PcToken> tokenStack = new Stack<PcToken>();

	while (!expression.equals("")) {
	    PcToken nextToken = this.lexer.parseNextToken(expression);

	    if (!nextToken.equals(PcToken.NULL_TOKEN)) {
		tokenStack.push(nextToken);

		expression = expression.substring(nextToken.getValue().length());
	    }
	}

	return tokenStack;
    }

    /**
     * Compares two Stacks of tokens.
     * @param expected
     * @param actual
     * @return
     */
    private boolean stacksAreEqual(Stack<PcToken> expected, Stack<PcToken> actual) {
	boolean result = true;

	while (actual.size() > 0 && expected.size() > 0) {
	    PcToken nextExpectedToken = expected.pop();
	    PcToken nextActualToken = actual.pop();

	    if (!nextExpectedToken.equals(nextActualToken)) {
		result = false;
	    }
	}

	return result;
    }

}
