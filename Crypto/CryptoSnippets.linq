<Query Kind="Statements" />

char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


char checkoutLetter = 'K';
char checkoutLetter2 = 'T';

int index = Array.IndexOf(alphabet, checkoutLetter);
int index2 = Array.IndexOf(alphabet, checkoutLetter2);

Console.WriteLine("Index of " + checkoutLetter.ToString() + " is " + index);
Console.WriteLine("Index of " + checkoutLetter2.ToString() + " is " + index2);

Console.WriteLine("(" + index + " + " + index2 + ") % 26 = " + ((index + index2) % 26).ToString() );

3 - 10 + 26