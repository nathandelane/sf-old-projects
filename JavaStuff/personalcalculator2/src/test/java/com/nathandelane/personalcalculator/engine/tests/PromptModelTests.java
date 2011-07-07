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

package com.nathandelane.personalcalculator.engine.tests;

import junit.framework.TestCase;

import org.junit.Test;

import com.nathandelane.personalcalculator.engine.PromptModel;

/**
 * Tests the PromptModel class.
 * @author nathanlane
 *
 */
public class PromptModelTests extends TestCase {

    /**
     * Tests whether what is inserted as the prompt is returned by the
     * PromptModel's toString() method.
     */
    @Test
    public void testPromptModel() {
	String prompt = "?> ";
	PromptModel newPromptModel = new PromptModel(prompt);

	assertEquals(prompt, newPromptModel.toString());
    }

}
