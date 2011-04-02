#include <stdio.h>

int main(void)
{
    char * test1 = "This is test 1";
    char * test2;

    test2 = "This is test 2";

    printf("Test 1: %s\n", test1);
    printf("Test 2: %s\n", test2);
    printf("Test 1.1: %s\n", &test1[5]);
    printf("Test 1.2: %.*s\n", 6, &test1[8]);

    return 0;
}
