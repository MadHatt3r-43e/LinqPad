<Query Kind="Statements" />


UInt16 startOfAlphabet = 65;
UInt16 lastOfAlphabet = 90;

Char a = 'A';
UInt16 aInt = Convert.ToUInt16(a);

Char b = 'B';

Char w = 'W';
UInt16 wInt = Convert.ToUInt16(w);

Char y = 'Y';
UInt16 yInt = Convert.ToUInt16(y);

Char z = 'Z';
UInt16 zInt = Convert.ToUInt16(z);

Console.WriteLine(a + " = " + aInt);
Console.WriteLine(w + " = " + wInt);
Console.WriteLine(y + " = " + yInt);
Console.WriteLine(z + " = " + zInt);


Char aPlus25 = (Char)(Convert.ToUInt16(a) + 25);
Console.WriteLine(aPlus25);

Char wPlus25 = (Char)(Convert.ToUInt16(w) + 25);
Console.WriteLine(wPlus25);

UInt16 intSum = (UInt16)(CharInt(wPlus25) - 90);
Char wtf = Convert.ToChar(intSum);
Console.WriteLine(wtf);

Char offset = AlphabetModAdd(a, b);
Console.WriteLine(offset);


UInt16 CharInt(Char c)
{
	return (UInt16)(Convert.ToUInt16(c));
}

Char AlphabetModAdd(Char a, Char b)
{
	UInt16 intSum = (UInt16)(CharInt(a) + CharInt(b));
	// UInt16 alphabetAdjusted = intSum > lastOfAlphabet ? (UInt16)(intSum - startOfAlphabet) : intSum;
	UInt16 alphabetAdjusted = (UInt16)(intSum % startOfAlphabet);
	return Convert.ToChar(alphabetAdjusted);
}


// handy dataGen
//Random random = new Random();
//int length = 2500;
//const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXY";
//String stuff =  new String(Enumerable.Repeat(chars, 2500)
//  .Select(s => s[random.Next(s.Length)]).ToArray());
  
  
int brkPt5=5;