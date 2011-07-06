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
 * Interface that all Tokenizer or Token Collector classes should implement.
 * @author nathanlane
 *
 * @param <T>
 */
public interface ITokenCollector<T> extends Iterable<T> {

   /**
    * Clears all of the tokens from the internal collection.
    */
   void clear();

   /**
    * Returns whether this Token Collector has any tokens in its internal collection.
    * @return whether this Token Collector has any tokens in its internal collection.
    */
   boolean hasTokens();

   /**
    * Transforms a String expression into an internal collection of tokens of type T.
    * @param expression A String expression that is tokenizable by this Token Collector.
    */
   void tokenize(String expression);

}
