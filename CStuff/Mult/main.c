#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main(int argc, char * argv[])
{
    int result = 0;
    int multiplicandLength = 0;
    int multiplierLength = 0;
    int multiplicandIndex = 0;
    int multiplierIndex = 0;
    int resultLength = 0;
    char resultComponents[200][20000];
    char nextResult[20000];
    char * multiplicand = "";
    char * multiplier = "";
    char * leftValue = "";
    char * rightValue = "";
    char * resultString = "";
    char * carryString = "";
    char * nextResultDigit = "";
    unsigned int left = 0;
    unsigned int right = 0;
    unsigned int lrResult = 0;
    unsigned int carry = 0;
    unsigned int resultComponentsIndex = 0;
    unsigned int resultIndex = 0;

    if (argc != 3)
    {
        printf("Usage: mult <multiplicand> <multiplier>\n");

        result = 1;
    }
    else
    {
        multiplicandLength = strlen(argv[1]);
        multiplierLength = strlen(argv[2]);
        multiplicand = argv[1];
        multiplier = argv[2];

        for (multiplicandIndex = 0; multiplicandIndex < multiplicandLength; multiplicandIndex++)
        {
            strncpy(nextResult, "\0", 1);

            for (multiplierIndex = 0; multiplierIndex < multiplierLength; multiplierIndex++)
            {
                sprintf(leftValue, "%.*s", 1, &multiplicand[multiplicandIndex]);
                sprintf(rightValue, "%.*s", 1, &multiplier[multiplierIndex]);

                left = atoi(leftValue);
                right = atoi(rightValue);
                lrResult = (left * right) + carry;

                resultLength = sprintf(resultString, "%d", lrResult);

                if (resultLength > 1)
                {
                    sprintf(carryString, "%.*s", (resultLength - 1), &resultString[0]);
                }

                sprintf(nextResultDigit, "%.*s", 1, &resultString[(resultLength - 1)]);

                strcat(nextResult, nextResultDigit);
            }

            strncpy(resultComponents[resultComponentsIndex], nextResult, 20000);

            resultComponentsIndex++;
        }

        for (resultIndex = 0; resultIndex < resultComponentsIndex; resultIndex++)
        {
            printf("%s\n", resultComponents[resultIndex]);
        }

        printf("%s\n", resultString);
    }

    return result;
}
