#include <stdio.h>
#include <stdlib.h>
#include <string.h>

char * Multiply(char * left, char * right)
{
    char * answer = "";
    char * lStrValue = "";
    char * rStrValue = "";
    char * resultStrValue = "";
    char * carryStrValue = "";
    char * nextDigitValue = "";
    int leftLength = strlen(left);
    int rightLength = strlen(right);
    int leftCounter = 0;
    int rightCounter = 0;
    int lValue = 0;
    int rValue = 0;
    int result = 0;
    int carry = 0;
    int resultLength;

    for (rightCounter = 0; rightCounter < rightLength; rightCounter++)
    {
        for (leftCounter = 0; leftCounter < leftLength; leftCounter++)
        {
            sprintf(lStrValue, "%.*s", 1, &left[leftCounter]);
            sprintf(rStrValue, "%.*s", 1, &right[rightCounter]);

            lValue = atoi(lStrValue);
            rValue = atoi(rStrValue);
            result = (lValue * rValue) + carry;

            resultLength = sprintf(resultStrValue, "%d", result);

            if (resultLength > 1)
            {
                sprintf(carry, "%.*s", (resultLength - 1), &resultStrValue[0]);
            }
        }
    }

    return answer;
}

int main(void)
{
    int error = 0;
    char * result = Multiply((char *)"12", (char *)"13");

    printf("%s\n", result);

    return error;
}
