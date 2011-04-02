#include <stdio.h>
#include <string.h>

int main(void)
{
    char stuff[2000];
    char * stuff2 = "";

    strcat(stuff, "20");

    printf("Stuff: %s\n", stuff);

    stuff2 -= strlen(" 23");
    strcat(stuff2, " 23");

    printf("Stuff2: %s\n", stuff2);

    return 0;
}
