package com.nathandelane.math.evaluation;

import java.util.ArrayList;
import java.util.Stack;

import com.nathandelane.math.evaluation.tokens.*;
import com.nathandelane.math.evaluation.tokens.Number;

public class Expression extends ArrayList<IToken> {

	private static final long serialVersionUID = -7092815995436435239L;
	
	private boolean _isPostfixFormatted;

	public static Expression parse(String expression) throws InvalidTokenException, NotImplementedException, IllegalStateException {
		Expression expressionObject = new Expression();
		IToken lastToken = new NullToken();
		boolean nextNumberIsNegative = false;
		
		expression = expression.replaceAll(" ", "");
		
		while(expression.length() > 0) {
			IToken nextToken = getNextToken(expression);
			
			if(isNegation(lastToken, nextToken)) {
				nextNumberIsNegative = true;
			} else if((nextToken instanceof Number) && nextNumberIsNegative) {
				String tokenValue = String.format("-%1$s", nextToken.getValue());
				expression = String.format("-%1$s", expression);
				nextNumberIsNegative = false;
				nextToken = new Number(tokenValue);
				
				int indexToRemove = expressionObject.size() - 1;
				expressionObject.remove(indexToRemove);
			}
			
			expressionObject.add(nextToken);
			
			lastToken = nextToken;
			
			int tokenLength = nextToken.getValue().length();
			expression = expression.substring(tokenLength);
		}
		
		expressionObject._isPostfixFormatted = false;
		
		return expressionObject;
	}
	
	public static Expression postfixate(Expression expression) {
		Expression postfixExpression = new Expression();
		Stack<IToken> operatorStack = new Stack<IToken>();
		int expressionIndex = 0;
		
		while(expressionIndex < expression.size()) {
			IToken nextToken = expression.get(expressionIndex);
			
			if(nextToken.getType() == TokenType.NUMBER || nextToken.getType() == TokenType.SPECIAL_NUMBER) {
				postfixExpression.add(nextToken);
			} else if(nextToken.getType() == TokenType.OPERATOR || nextToken.getType() == TokenType.FUNCTION) {
				TokenPrecedence nextTokenPrecedence = nextToken.getPrecedence();
				
				if(operatorStack.size() > 0) {
					TokenPrecedence operatorPrecedence = operatorStack.peek().getPrecedence();
					
					if(nextTokenPrecedence.compareTo(operatorPrecedence) == PostfixationAction.STACK) {
						operatorStack.push(nextToken);
					} else if(nextTokenPrecedence.compareTo(operatorPrecedence) == PostfixationAction.POP) {
						while(nextTokenPrecedence.compareTo(operatorPrecedence) == PostfixationAction.POP) {
							postfixExpression.add(operatorStack.pop());
							
							operatorPrecedence = operatorStack.peek().getPrecedence();
						}
					}
				} else {
					operatorStack.push(nextToken);
				}
			} else if(nextToken.getType() == TokenType.STRUCTURE) {
				if(nextToken instanceof OpenPerenthesis) {
					operatorStack.push(nextToken);
				} else {
					while(!(operatorStack.peek() instanceof OpenPerenthesis)) {
						postfixExpression.add(operatorStack.pop());
					}
					
					operatorStack.pop(); // Pop the OpenPerenthesis off from the operatorStack.
				}
			}
			
			expressionIndex++;
		}
		
		while(operatorStack.size() > 0) {
			postfixExpression.add(operatorStack.pop());
		}
		
		postfixExpression._isPostfixFormatted = true;
		
		return postfixExpression;
	}

	private static boolean isNegation(IToken lastToken, IToken nextToken) {
		boolean result = false;
		
		if(nextToken.getType() == TokenType.OPERATOR) {
			if((lastToken.getType() == TokenType.OPERATOR)
					|| (lastToken.getType() == TokenType.NULL) 
					|| ((lastToken.getType() == TokenType.STRUCTURE) && (lastToken instanceof OpenPerenthesis)))
			{
				result = true;
			}
		}
		
		return result;
	}
	
	private static IToken getNextToken(String expression) throws NotImplementedException, IllegalStateException {
		IToken nextToken = new NullToken();
		ArrayList<IToken> allTokens = AllTokens.getSet();
		int allTokensIndex = 0;
		
		while(nextToken instanceof NullToken && allTokensIndex < allTokens.size()) {
			IToken nextTokenBase = allTokens.get(allTokensIndex);			

			if(nextTokenBase.matches(expression)) {
				if(nextTokenBase instanceof Number) {
					String value = nextTokenBase.firstMatch();
					nextToken = new Number(value);
				} else {
					nextToken = nextTokenBase;
				}
			}
			
			allTokensIndex++;
		}
		
		return nextToken;
	}
	
	public String evaluate() throws TokenTranspositionException, DivisionByZeroException {
		String result = "";
		
		if(getIsPostfixFormatted()) {
			while(size() > 1) {
				int expressionIndex = 0;
				IToken nextToken = new NullToken();
				
				while(nextToken.getType() != TokenType.OPERATOR && nextToken.getType() != TokenType.FUNCTION) {
					nextToken = get(expressionIndex);
					expressionIndex++;
				}
				
				if(nextToken.getType() == TokenType.OPERATOR) {
					int leftIndex = expressionIndex - 3;
					Number operationResult = new Number();
					
					if(isANumber(get(leftIndex)) && isANumber(get(leftIndex + 1))) {
						Number left = (Number)get(leftIndex);
						Number right = (Number)get(leftIndex + 1);

						if(left.isDouble()) {
							double doubleLeft = Double.parseDouble(left.getValue());
							double doubleRight = Double.parseDouble(right.getValue());
							operationResult = evaluateDoubleExpression(nextToken, doubleLeft, doubleRight);
						} else if(left.isInt()) {
							int intLeft = Integer.parseInt(left.getValue());
							int intRight = Integer.parseInt(right.getValue());
							operationResult = evaluateIntExpression(nextToken, intLeft, intRight);
						} else if(left.isLong()) {
							long longLeft = Long.parseLong(left.getValue());
							long longRight = Long.parseLong(right.getValue());
							operationResult = evaluateLongExpression(nextToken, longLeft, longRight);
						}
					} else if(isANumber(get(leftIndex)) && isASpecialNumber(get(leftIndex + 1))) {
						Number left = (Number)get(leftIndex);
						
						double doubleLeft = Double.parseDouble(left.getValue());
						double doubleRight = ((SpecialNumber)get(leftIndex + 1)).evaluate();
						operationResult = evaluateDoubleExpression(nextToken, doubleLeft, doubleRight);
					} else if(isASpecialNumber(get(leftIndex)) && isANumber(get(leftIndex + 1))) {
						Number right = (Number)get(leftIndex + 1);
						
						double doubleLeft = ((SpecialNumber)get(leftIndex)).evaluate();
						double doubleRight = Double.parseDouble(right.getValue());
						operationResult = evaluateDoubleExpression(nextToken, doubleLeft, doubleRight);
					} else if(isASpecialNumber(get(leftIndex)) && isASpecialNumber(get(leftIndex + 1))) {
						double doubleLeft = ((SpecialNumber)get(leftIndex + 1)).evaluate();
						double doubleRight = ((SpecialNumber)get(leftIndex)).evaluate();
						operationResult = evaluateDoubleExpression(nextToken, doubleLeft, doubleRight);
					} else {
						throw new TokenTranspositionException();
					}
					
					this.remove(leftIndex); // remove left
					this.remove(leftIndex); // remove right
					this.set(leftIndex, operationResult); // replace operator with result
				} else if(nextToken.getType() == TokenType.FUNCTION) {
					int rightIndex = expressionIndex - 2;
					Number operationResult = new Number();
					
					if(isANumber(get(rightIndex))) {
						Number right = (Number)get(rightIndex);
						
						if(right.isDouble()) {
							double doubleRight = Double.parseDouble(right.getValue());
							operationResult = evaluateDoubleExpression(nextToken, doubleRight);
						} else if(right.isInt()) {
							int intRight = Integer.parseInt(right.getValue());
							operationResult = evaluateIntExpression(nextToken, intRight);
						} else if(right.isLong()) {
							long longRight = Long.parseLong(right.getValue());
							operationResult = evaluateLongExpression(nextToken, longRight);
						}
					} else if(isASpecialNumber(get(rightIndex))) {
						double doubleRight = ((SpecialNumber)get(rightIndex)).evaluate();
						operationResult = evaluateDoubleExpression(nextToken, doubleRight);
					} else {
						throw new TokenTranspositionException();
					}
					
					this.remove(rightIndex); // remove right
					this.set(rightIndex, operationResult); // replace operator with result
				}
			}
			
			if(size() == 1) {
				if(get(0) instanceof Number) {
					result = ((Number)get(0)).getValue();
				} else if(get(0) instanceof SpecialNumber) {
					result = String.format("%1$s", ((SpecialNumber)get(0)).evaluate());
				}
			} else {
				throw new TokenTranspositionException();
			}
		} else {
			throw new TokenTranspositionException();
		}
		
		if(result.endsWith(".0")) {
			int indexOfDecimal = result.indexOf(".0");
			result = result.substring(0, indexOfDecimal);
		}
		
		EvaluationState.__lastResult = result;
		
		return result;
	}
	
	public boolean getIsPostfixFormatted() {
		return _isPostfixFormatted;
	}
	
	private boolean isANumber(IToken token) {
		return (token instanceof Number);
	}
	
	private boolean isASpecialNumber(IToken token) {
		return (token instanceof Pi || token instanceof E || token instanceof SpecialNumber);
	}
	
	private Number evaluateDoubleExpression(IToken operator, double left, double right) throws DivisionByZeroException {
		Number result = new Number();
		
		if(operator instanceof Addition) {
			result = new Number(left + right);
		} else if(operator instanceof Subtraction) {
			result = new Number(left - right);
		} else if(operator instanceof Multiplication) {
			result = new Number(left * right);
		} else if(operator instanceof Division) {
			if(right != 0.0) {
				result = new Number(left / right);
			} else {
				throw new DivisionByZeroException();
			}
		}
		
		return result;
	}
	
	private Number evaluateLongExpression(IToken operator, long left, long right) throws DivisionByZeroException {
		Number result = new Number();
		
		if(operator instanceof Addition) {
			result = new Number(left + right);
		} else if(operator instanceof Subtraction) {
			result = new Number(left - right);
		} else if(operator instanceof Multiplication) {
			result = new Number(left * right);
		} else if(operator instanceof Division) {
			if(right != 0) {
				result = new Number((double)left / (double)right);
			} else {
				throw new DivisionByZeroException();
			}
		}
		
		return result;
	}
	
	private Number evaluateIntExpression(IToken operator, int left, int right) throws DivisionByZeroException {
		Number result = new Number();
		
		if(operator instanceof Addition) {
			result = new Number(left + right);
		} else if(operator instanceof Subtraction) {
			result = new Number(left - right);
		} else if(operator instanceof Multiplication) {
			result = new Number(left * right);
		} else if(operator instanceof Division) {
			if(right != 0) {
				result = new Number((double)left / (double)right);
			} else {
				throw new DivisionByZeroException();
			}
		}
		
		return result;
	}
	
	private Number evaluateDoubleExpression(IToken operator, double right) {
		Number result = new Number();
		
		if(operator instanceof Sine) {
			result = new Number(Math.sin(right));
		} else if(operator instanceof Cosine) {
			result = new Number(Math.cos(right));
		} else if(operator instanceof Tangent) {
			result = new Number(Math.tan(right));
		}
		
		return result;
	}
	
	private Number evaluateLongExpression(IToken operator, long right) {
		Number result = new Number();
		
		if(operator instanceof Sine) {
			result = new Number(Math.sin(right));
		} else if(operator instanceof Cosine) {
			result = new Number(Math.cos(right));
		} else if(operator instanceof Tangent) {
			result = new Number(Math.tan(right));
		}
		
		return result;
	}
	
	private Number evaluateIntExpression(IToken operator, int right) {
		Number result = new Number();
		
		if(operator instanceof Sine) {
			result = new Number(Math.sin(right));
		} else if(operator instanceof Cosine) {
			result = new Number(Math.cos(right));
		} else if(operator instanceof Tangent) {
			result = new Number(Math.tan(right));
		}
		
		return result;
	}
	
}
