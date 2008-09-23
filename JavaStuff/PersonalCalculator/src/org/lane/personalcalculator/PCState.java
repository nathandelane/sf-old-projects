package org.lane.personalcalculator;

import java.util.*;

public class PCState {
	
	public static String lastResult = "0";
	public static String lastToken = "";
	public static String currentVarName = "";
	public static int error = 0;
	public static boolean trace = false;
	public static boolean setVariable = false;
	public static Hashtable<String, String> vars = new Hashtable<String, String>();
	
	public static final String availableCommands = "Available Commands: +, -, /, *, cos, sin, tan, (), trace, $ (which means last result), ? (which means help), ln, log, loge, log10, sqrt, ~, ^ (which is a power operator)";

}
