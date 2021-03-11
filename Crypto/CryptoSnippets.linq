<Query Kind="Statements" />

char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };



char checkoutLetter = ')';
char checkoutLetter2 = 'Z';

int index = Array.IndexOf(alphabet, checkoutLetter);
int index2 = Array.IndexOf(alphabet, checkoutLetter2);

Console.WriteLine("Index of " + checkoutLetter.ToString() + " is " + index);
Console.WriteLine("Index of " + checkoutLetter2.ToString() + " is " + index2);

Console.WriteLine("(" + index + " + " + index2 + ") % 26 = " + ((index + index2) % 26).ToString() );

Char a = 'A';
UInt16 aInt = Convert.ToUInt16(a);


Char whatever = ')';
UInt16 intWhatever = Convert.ToUInt16(whatever);


Char b = (Char)(Convert.ToUInt16(a) + 25);

Console.WriteLine(a);
Console.WriteLine(aInt);
Console.WriteLine(b);


Console.WriteLine(whatever);
Console.WriteLine(intWhatever);

UInt16 intZ = 90;
UInt16 intA = 65;
UInt16 ordinalZ = 25;
UInt16 ordinalC = 2;

Console.WriteLine( ( intA + ordinalZ ) % 90);
Console.WriteLine(intA + ordinalC);

// 67 => 67
// 90 => 90
// 91 => 65




