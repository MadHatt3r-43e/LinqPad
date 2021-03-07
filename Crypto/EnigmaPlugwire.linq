<Query Kind="Statements" />



List<Plugwire> plugwires = new List<Plugwire>();

Plugwire pw = new Plugwire('A', 'B');

Plugwire pw2 = new Plugwire('B', 'L');

plugwires.Add(pw);

// this bwlow creates in invalid plugboard
plugwires.Add(pw2);

plugwires.Dump();

Char keyEntered = 'A';

bool isWireEndpointA = plugwires.Any(pw => pw.left == keyEntered || pw.right == keyEntered);
Plugwire pw1 = (Plugwire)plugwires[0];

Console.WriteLine("Plugwire " + pw1.ToString() + " has an endpoint for keyEntered " + keyEntered.ToString() + " == " + isWireEndpointA);


public class Plugwire : IEquatable<Plugwire>
{
	public char left;
	public char right;
	
	public Plugwire(char A, char B)
	{
		this.left = A;
		this.right = B;
	}
	//A<=>B
	//T<=>J
	//K<=>Y
	//R<=>S

	public override String ToString()
	{
		return String.Format("{0}<=>{1}", left.ToString(), right.ToString());
	}

	public override int GetHashCode()
	{
		return (int)left+right;
	}

	public bool Equals(Plugwire other)
	{
		if (other == null)
			return false;

		return (this.left == other.left && this.right == other.right);
	}

	public bool Equals(Char a, Char b)
	{
		return false; //(this.value == val);
	}
	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}
	
}