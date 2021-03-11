<Query Kind="Statements" />


UInt16 startOfAlphabet = 65;
UInt16 lastOfAlphabet = 90;

Char a = 'A';
UInt16 aInt = Convert.ToUInt16(a);


Char z = 'Z';
UInt16 zInt = Convert.ToUInt16(z);


Char zz = (Char)(Convert.ToUInt16(a) + 25);

Console.WriteLine(a);
Console.WriteLine(aInt);

Console.WriteLine(z);
Console.WriteLine(zInt);


Console.WriteLine(zz);

Char p = 'A';

UInt16 intA = GetOrdinalRingPosition(p);
Console.WriteLine(intA);

UInt16 GetOrdinalRingPosition(Char position)
{
	UInt16 intPos = Convert.ToUInt16(position);
	return (UInt16)(intPos - startOfAlphabet);
}

Random random = new Random();
int length = 2500;
const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXY";
String stuff =  new String(Enumerable.Repeat(chars, 2500)
  .Select(s => s[random.Next(s.Length)]).ToArray());
  
  
int brkPt5=5;