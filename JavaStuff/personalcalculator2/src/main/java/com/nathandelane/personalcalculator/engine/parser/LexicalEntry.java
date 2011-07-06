package com.nathandelane.personalcalculator.engine.parser;

import java.util.regex.Pattern;

/**
 * Defines an entry for the lexer.
 * @author nathanlane
 *
 */
public class LexicalEntry {

    private Pattern pattern;
    private PcTokenType type;

    public LexicalEntry(Pattern pattern, PcTokenType type) {
	this.pattern = pattern;
	this.type = type;
    }

    /**
     * Gets the pattern for this entry.
     * @return
     */
    public Pattern getPattern() {
	return this.pattern;
    }

    /**
     * Gets the type for this entry.
     * @return
     */
    public PcTokenType getType() {
	return this.type;
    }

}
