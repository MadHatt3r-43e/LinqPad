<Query Kind="Statements" />

Random rand = new Random();
var randomSeq = Enumerable.Range(0, 100).OrderBy(i => rand.Next(0,20)).Take(50);
randomSeq.Dump();