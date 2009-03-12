package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class SetCommand extends Command {

	public SetCommand() {
		super(TokenType.COMMAND, TokenPrecedence.NULL_PRECEDENCE, "set");
		
		_pattern = Pattern.compile("^(set){1}");
	}
	
	@Override
	public void run() {
		// TODO Auto-generated method stub

	}

}
