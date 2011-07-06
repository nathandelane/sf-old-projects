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

package com.nathandelane.personalcalculator.engine;

/**
 * Context handlers take care of handling execution in a particular context. There are two types of
 * context supported by default for personal calculator: interactive and script. A script context may
 * be either simply command-line arguments in the form of an expression, or the location of a script.
 * A switch will be used to determine which is used.
 * @author nathanlane
 *
 */
public interface IContextHandler {

    void execute();

}
