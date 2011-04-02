#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/**
 * Calculation struct.
 * This type contains two fields for an enponentiation calculation: base which is the base number to be multiplied
 * by itself and the exponent which is the number of times that the Exponent will be multiplied by itself.
 */
struct Calculation
{
	unsigned long Base;
	unsigned long Exponent;
    char * Result;
    unsigned int ResultLength;
};

int Multiply(struct Calculation * calculation)
{
    signed int result = 0;
    int nextDigitResultLength = 0;
    unsigned long digitIndex = 0;
    unsigned long nextDigit = 0;
    unsigned long nextDigitResult = 0;
    unsigned long carry = 0;
    char * nextDigitValue = "";
    char * nextDigitResultValue = "";
    char multiplicationResult[2000];
    char * carryValue = "";
    char * nextAnswerDigit = "";
    char nextDigitChar = '\0';

    if (calculation != NULL && calculation->ResultLength == 0)
    {
        calculation->ResultLength = sprintf(calculation->Result, "%lu", calculation->Base);
    }

    while (digitIndex < calculation->ResultLength)
    {
        nextDigitChar = (char)calculation->Result[digitIndex];

        sprintf(nextDigitValue, "%c", nextDigitChar);

        nextDigit = atol(nextDigitValue);
        nextDigitResult = (nextDigit * calculation->Base) + carry;

        printf("(%lu * %lu) + %lu = %lu\n", nextDigit, calculation->Base, carry, nextDigitResult);

        nextDigitResultLength = sprintf(nextDigitResultValue, "%ld", nextDigitResult);

        printf("NextDigitResultValue: %s\n", nextDigitResultValue);

        if (nextDigitResultLength > 1)
        {
            printf("Setting carry\n");

            sprintf(nextAnswerDigit, "%.*s", 1, &nextDigitResultValue[(nextDigitResultLength - 1)]);

            printf("NextAnswerDigit: %s\n", nextAnswerDigit);

            sprintf(carryValue, "%.*s", (nextDigitResultLength - 1), &nextDigitResultValue[0]);

            carry = atoi(carryValue);
        }

        strcat(multiplicationResult, nextDigitResultValue);

        printf("Multiplication Result: %s, Carry: %lu, NextDigitResultValue: %s\n", multiplicationResult, carry, nextDigitResultValue);

        digitIndex++;
    }

    return result;
}

int main(int argc, char * argv[])
{
	int result = 0;
    unsigned long counter;
    struct Calculation calculation;

    if (argc < 3)
    {
        printf("usage: permutator <base> <exponent>\n");

        result = 1;
    }
    else
    {
        calculation.Base = atol(argv[1]);
        calculation.Exponent = atol(argv[2]);
        calculation.Result = "0";

        printf("Base: %lu, Exponent: %lu, Result: %s, ResultLength: %i\n", calculation.Base, calculation.Exponent, calculation.Result, calculation.ResultLength);

        if (calculation.Base > 0 && calculation.Exponent > 0)
        {
            for (counter = 0; counter < calculation.Exponent; ++counter)
            {
                if (Multiply(&calculation) == 0)
                {
                    printf("Counter=%lu, Result=%s, Base=%lu, ResultLength=%i\n", counter, calculation.Result, calculation.Base, calculation.ResultLength);
                }
            }
        }
        else
        {
            printf("base and exponent must be positive integers.\n");
        }
    }

	return result;
}
