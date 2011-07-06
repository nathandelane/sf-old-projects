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

import java.util.Iterator;
import java.util.Stack;

import com.nathandelane.personalcalculator.engine.CalculatorContext;
import com.nathandelane.personalcalculator.engine.ITokenCollector;

/**
 * Personal Calculator implementation if ITokenCollector. This is the token
 * scanner.
 * @author nathanlane
 *
 */
public class PcTokenCollector implements ITokenCollector<PcToken> {

    private PcLexer lexer;
    private Stack<PcToken> tokenStack;

    public PcTokenCollector() {
	this.tokenStack = new Stack<PcToken>();
	this.lexer = PcLexer.getInstance();
    }

    /**
     * Gets the iterator for the token collection in the collector.
     */
    public Iterator<PcToken> iterator() {
	return this.tokenStack.iterator();
    }

    /**
     * Clears the internal token collection.
     */
    public void clear() {
	this.tokenStack.clear();
    }

    /**
     * Determines whether the collector has any tokens.
     */
    public boolean hasTokens() {
	return this.tokenStack.size() > 0;
    }

    /**
     * Tokenizes a given expression.
     */
    public void tokenize(String expression) {
	int expressionCursor = 0;

	while (expressionCursor < (expression.length() - 1)) {
	    PcToken nextToken = this.lexer.parseNextToken(expression.substring(expressionCursor));

	    if (nextToken != null) {
		expressionCursor += nextToken.getValue().length();

		this.tokenStack.push(nextToken);
	    } else {
		CalculatorContext.get().addErrorToHistory(new Error(String.format("Parsed null token from %1$s.", expression.substring(expressionCursor))));
	    }
	}
    }

}
