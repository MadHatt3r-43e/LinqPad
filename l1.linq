<Query Kind="Statements" />

int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

numbers.Dump();

//var result = from n in numbers
var result = from n in numbers.AsQueryable()
			where n%2 == 0
			orderby n descending
			select n;
			
result.Dump();


var query = 
	from c in "hello, world"
	orderby c
	select char.ToUpper(c);
	
Console.WriteLine(query.Distinct());

List<int> list = new List<int>();
list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });

List<int> evenNumbers = list.FindAll(i => (i % 2) == 0);
Console.WriteLine("Here are your even numbers:");
foreach (int evenNumber in evenNumbers)
{
   Console.Write("{0}\t", evenNumber);
}

// count occurrences in an array
int[] ints = { 4, 8, 8, 3, 9, 0, 7, 8, 2 };

// Count the even numbers in the array, using a seed value of 0.
int numEven = ints.Aggregate(0, (total, next) =>
                                    next % 2 == 0 ? total + 1 : total);

Console.WriteLine("The number of even integers is: {0}", numEven);

// Setting the query language to "C# Statement(s)" permits multiple statements:

var words =
	from word in "The quick brown fox jumps over the lazy dog".Split()
	orderby word.ToUpper()
	select word;
	
var duplicates =
	from word in words
	group word.ToUpper() by word.ToUpper() into g
	where g.Count() > 1
	select new { g.Key, Count = g.Count() };	
	
// The Dump extension method writes out queries:

var counts =
	from word in words
	group word.ToUpper() by word.ToUpper() into g
	select new { g.Key, Count = g.Count() };	
counts.Dump();

words.Dump();
duplicates.Dump();






