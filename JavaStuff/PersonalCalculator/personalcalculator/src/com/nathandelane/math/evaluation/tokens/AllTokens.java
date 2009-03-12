package com.nathandelane.math.evaluation.tokens;

import java.util.ArrayList;
import java.util.Collections;

public class AllTokens {

	private static ArrayList<IToken> __tokens = null;
	
	public static ArrayList<IToken> getSet() {
		if(__tokens == null) {
			__tokens = new ArrayList<IToken>();
			
			Collections.addAll(__tokens,
				new SetCommand(),
				new Assignment(),
				new Number(0),
				new Pi(),
				new E(),
				new LastResult(),
				new OpenPerenthesis(),
				new ClosePerenthesis(),
				new Multiplication(),
				new Division(),
				new Subtraction(),
				new Addition(),
				new Sine(),
				new Cosine(),
				new Tangent()
			);
		}
		
		return __tokens;
	}
	
}
