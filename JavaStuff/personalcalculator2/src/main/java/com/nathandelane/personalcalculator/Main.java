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

package com.nathandelane.personalcalculator;

import java.util.HashMap;

import com.nathandelane.personalcalculator.engine.CalculatorContext;

/**
 *
 * @author nathanlane
 *
 */
public class Main {
    private Arguments arguments;

    @SuppressWarnings("serial")
    private Main(String[] args) {
	CalculatorContext.get().setValue(CalculatorContext.MODE_KEY, CalculatorMode.RADIANS);

	this.arguments = Arguments.getArgumentParser();
	this.arguments.addArgumentDefinitions(new HashMap<String, ArgumentDefinition>() {
		    {
			put("-h", new HelpArgumentDefinition("h"));
			put("--help", new HelpArgumentDefinition("help"));
			put("-m", new ModeArgumentDefinition("m"));
		    }
		});
    }

    public static void main(String[] args) {
	Main mainObject = new Main(args);
    }

    /**
     * Help argument definition.
     *
     * @author nathanlane
     *
     */
    private class HelpArgumentDefinition extends ArgumentDefinition {

	public HelpArgumentDefinition(String argument) {
	    super(argument);
	}

	@Override
	public void execute(String argumentValue) {
	    System.out.println(String.format("usage: bpc [options] [script[expression]] [expression]%1$s" +
	    		"general:%1$s" +
	    		"  -h, --help             print this usage file and exit%1$s" +
	    		"  -m=mode, --mode=mode   set calculator default mode to radians or degrees%1$s" +
	    		"  -v, --version          print version information and exit%1$s"
		    , System.getProperty("line.separator")));
	    System.exit(0);
	}

    }

    /**
     * Mode argument definition.
     * @author nathanlane
     *
     */
    private class ModeArgumentDefinition extends ArgumentDefinition {

	public ModeArgumentDefinition(String argument) {
	    super(argument);
	}

	@Override
	public void execute(String argumentValue) {
	    String[] modeComponents = argumentValue.split("=");

	    if (modeComponents.length == 2) {
		String mode = modeComponents[1].toUpperCase();

		CalculatorContext.get().setValue(CalculatorContext.MODE_KEY, CalculatorMode.valueOf(mode));
	    } else {
		CalculatorContext.get().addErrorToHistory(new Error(String.format("Mode value '%1$s' was not recognized. Setting to default 'radians'.", argumentValue)));
	    }
	}

    }
}
