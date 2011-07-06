package com.nathandelane.personalcalculator.engine;

/**
 * Interface for all lexers.
 * @author nathanlane
 *
 * @param <T>
 */
public abstract class AbstractLexer<T> {

    /**
     * Parses the next token out of the given expression.
     * @param expression
     * @return
     */
    public abstract T parseNextToken(String expression);

}
