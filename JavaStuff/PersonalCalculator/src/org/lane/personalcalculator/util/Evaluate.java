package org.lane.personalcalculator.util;

import java.security.*;

import org.lane.personalcalculator.*;
import org.lane.personalcalculator.collections.*;

public class Evaluate {
	
	private Evaluate()
	{
		
	}
	
	public static String evaluateExpression(TokenList postfixatedTokens) {
		String result = "0.0";
		
		if(postfixatedTokens.size() > 1) {
			for(int i = 0; i < postfixatedTokens.size(); i++) {
				ExpressionToken token = postfixatedTokens.get(i);
				PCState.lastToken = token.getToken();
				
				if(PCState.trace) {
					System.out.println("Trace: " + Evaluate.join(postfixatedTokens.toArray(), ",") + "; Result: " + result);
				}
				
				switch(token.getTokenType()) {
				case ExpressionToken.AddOperator:
					result = "" + Evaluate.add(Double.parseDouble(postfixatedTokens.get(i - 2).getToken()), Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
					postfixatedTokens.remove(i - 2);
					postfixatedTokens.remove(i - 2);
					
					i = -1;
					break;
				case ExpressionToken.DivisionOperator:
					result = "" + Evaluate.divide(Double.parseDouble(postfixatedTokens.get(i - 2).getToken()), Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
					postfixatedTokens.remove(i - 2);
					postfixatedTokens.remove(i - 2);
					
					i = -1;
					break;
				case ExpressionToken.MultiplicationOperator:
					result = "" + Evaluate.multiply(Double.parseDouble(postfixatedTokens.get(i - 2).getToken()), Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
					postfixatedTokens.remove(i - 2);
					postfixatedTokens.remove(i - 2);
					
					i = -1;
					break;
				case ExpressionToken.NegationOperator:
					if((i - 2) < 0) {
						result = "" + Evaluate.negate(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
						postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
						postfixatedTokens.remove(i - 1);
					} else {
						result = "" + Evaluate.subtract(Double.parseDouble(postfixatedTokens.get(i - 2).getToken()), Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
						postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
						postfixatedTokens.remove(i - 2);
						postfixatedTokens.remove(i - 2);
					}
					
					i = -1;
					break;
				case ExpressionToken.Function:
					if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("cos")) {
						result = "" + Evaluate.cos(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("sin")) {
						result = "" + Evaluate.sin(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("tan")) {
						result = "" + Evaluate.tan(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("log") || postfixatedTokens.get(i).getToken().equalsIgnoreCase("log10")) {
						result = "" + Evaluate.log(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("ln") || postfixatedTokens.get(i).getToken().equalsIgnoreCase("loge")) {
						result = "" + Evaluate.ln(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("sqrt")) {
						result = "" + Evaluate.sqrt(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("tohex")) {
					        result = Evaluate.tohex(Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("sha1")) {
						try {
							result = "" + Double.parseDouble(Evaluate.sha1(postfixatedTokens.get(i - 1).getToken()));
						} catch(Exception e) { result = "" + -1.0; }
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("md5")) {
						try {
							result = "" + Double.parseDouble(Evaluate.md5(postfixatedTokens.get(i - 1).getToken()));
						} catch(Exception e) { result = "" + -1.0; }
					} else if(postfixatedTokens.get(i).getToken().equalsIgnoreCase("md2")) {
						try {
							result = "" + Double.parseDouble(Evaluate.md2(postfixatedTokens.get(i - 1).getToken()));
						} catch(Exception e) { result = "" + -1.0; }
					} else {
						System.out.println(postfixatedTokens.get(i).getToken() + " is not an implemented function.");
					}
					postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
					postfixatedTokens.remove(i - 1);
					
					i = -1;
					break;
				case ExpressionToken.PowerOperator:
					result = "" + Evaluate.pow(Double.parseDouble(postfixatedTokens.get(i - 2).getToken()), Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
					postfixatedTokens.remove(i - 2);
					postfixatedTokens.remove(i - 2);
					
					i = -1;
					break;
				case ExpressionToken.Factorial:
					result = "" + Evaluate.factorial((int)Double.parseDouble(postfixatedTokens.get(i - 1).getToken()));
					postfixatedTokens.set(i, new ExpressionToken("" + result, ExpressionToken.Number));
					postfixatedTokens.remove(i - 1);
					break;
				}
			}
		} else {
			result = "" + Double.parseDouble(postfixatedTokens.get(0).getToken());
		}
		
		String strResult = "";
		if((double)(Double.parseDouble(result) - Math.floor(Double.parseDouble(result))) == 0.0) {
			strResult = "" + (int)Math.floor(Double.parseDouble(result));
		} else {
			strResult = "" + result;
		}
		
		if(PCState.setVariable) {
			PCState.vars.put(PCState.currentVarName, strResult);
		}

		PCState.error = 0;
		PCState.lastResult = "" + result;

		return strResult;
	}

	private static double add(double left, double right) {
		return left + right;
	}

	private static double divide(double left, double right) {
		return left / right;
	}

	private static double multiply(double left, double right) {
		return left * right;
	}
	
	private static double subtract(double left, double right) {
		return left - right;
	}
	
	private static double negate(double right) {
		return multiply(-1, right);
	}
	
	private static double cos(double right) {
		return (double)Math.cos(right);
	}

	private static double sin(double right) {
		return (double)Math.sin(right);
	}

	private static double tan(double right) {
		return (double)Math.tan(right);
	}

	private static double pow(double n, double r) {
		return (double)Math.pow(n, r);
	}
	
	private static double log(double n) {
		return (double)Math.log10(n);
	}
	
	private static double ln(double n) {
		return (double)Math.log(n);
	}
	
	private static double factorial(int n) {
		double r = (double)n;
		
		for(int i = (n - 1); i > 0; i--) {
			r *= (double)i;
		}
		
		return r;
	}
	
	private static double sqrt(double right) {
		return Math.sqrt(right);
	}
	
	private static String md2(String str) throws NoSuchAlgorithmException {
		return (MessageDigest.getInstance(ExpressionToken.MD2Method).digest(str.getBytes())).toString();
	}
	
	private static String md5(String str) throws NoSuchAlgorithmException {
		return (MessageDigest.getInstance(ExpressionToken.MD5Method).digest(str.getBytes())).toString();
	}
	
	private static String sha1(String str) throws NoSuchAlgorithmException {
		return (MessageDigest.getInstance(ExpressionToken.SHA1Method).digest(str.getBytes())).toString();
	}
	
	private static String join(ExpressionToken[] collection, String delimiter) {
		String result = "";
		
		if(collection.length == 1) {
			result = collection[0].getToken();
		} else if(collection.length > 1) {
			for(int i = 0; i < (collection.length - 1); i++) {
				result += collection[i].getToken() + delimiter;
			}
			
			result += collection[(collection.length - 1)].getToken();
		}
		
		return result;
	}
	
	private static String tohex(double right) {
	    return Double.toHexString(right);
	}
	
	private static String tobin(double right) {
	    String result = "tobin function not implemented";
	    /*
	    while(right != 0.0) {
	        
	    }
	    */
	    return result;
	}

}
