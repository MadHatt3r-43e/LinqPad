<Query Kind="Statements" />

int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//var result = from n in numbers
var result = from n in numbers.AsQueryable()
			where n%2 == 0
			orderby n descending
			select n;
			
result.Dump();