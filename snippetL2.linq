<Query Kind="Statements" />

int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

foreach( int n in numbers )
{
	if( n % 2 == 0 )
	{
		Console.WriteLine(n);
	}
	
}

numbers.Dump();

