using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    /// <summary>
    /// This enum is utlized by Equation to set the token type of a particular token. This helps determine precedence in order of operations.
    /// </summary>
    internal enum TokenType
    {
        Variable,           // A variable constitutes any alphanumeric symbol that does not map to a function. It has the lowest precedence, because it must be evaluated before other operations may continue.
        SpecialNumber,      // Special numbers are numbers like e or pi. These numbers are devined as Evaluator functions, but really they are numbers.
        Number,             // Number is any real number of either integer or floating point type.
        LeftPerenthesis,    // A left perenthesis marks the beginning of a portion of an equation that is to be evaluated before all other operations external to the perenthesized block.
        RightPerenthesis,   // A right perenthesis marks the end of a perenthesized block of an equation.
        BitwiseOperation,   // A bitwise operation operates at the binary level. Included are &, |, and ^ (xor).
        Negation,           // Negation is the negation of a result or number. It looks similar to a subtraction without a minuend.
        Subtraction,        // Subtraction is the difference between two numbers, the minuend minus the subtrahend.
        Addition,           // Addition is the sum of two addends.
        Division,           // Division is the quotient of two numbers, the dividend divided by the divisor.
        Multiplication,     // Multiplication is the product of two numbers, the multiplier and the multiplicand, or factors.
        Function,           // A function my be trigonometrical, analytical, algebraic, or a calculus function. It may also be user defined. It is not limited to mathematically provable functions.
        Factorial,          // Factorial is a special function in Algebra the multiplies descending children of a value together.
        Power,              // Power is a function that multiplies a number by itself n times.
        Null                // This is only used in the case of creating a null token.
    }
}
