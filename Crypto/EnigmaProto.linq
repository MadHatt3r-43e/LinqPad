<Query Kind="Statements" />

// Eric Strickland
// 3.6.2021
// Engigma prototypes



// Machine

// Encryption components
// Collection of rotors
// Reflector
// Plugboard

// UI
// keyboard
// lightboard



// Plugboard
// 26 plugs

// A=>T === T=>A  // one plugboard wire

List<Plug> plugs = new List<Plug>();

Plug pAB = new Plug('A','B');
Plug pBA = new Plug('B','A');

Plug pTJ = new Plug('T', 'J');
Plug pJT = new Plug('J', 'T');

plugs.Add(pAB);
plugs.Add(pBA);
plugs.Add(pTJ);
plugs.Add(pJT);

bool plugAExists = plugs.Contains('A');



public class Plug : IEquatable<Plug>
{
	private Char value;
	private Char pointsTo;

	public Plug(Char v, Char p)
	{
		value = v;
		pointsTo = p;
	}

	public override String ToString()
	{
		return String.Format("{0}=>{1}", value, pointsTo);
	}
	public bool Equals(Plug other)
	{
		if (other == null)
			return false;

		return (this.value == other.value);
	}

	public bool Equals(Char val)
	{
		return (this.value == val);
	}
	//public override bool Equals(object obj)
	//{
	//	return base.Equals(obj);
	//}



	//A=>B
	//B=>A

	//T=>J
	//J=>T

	//K=>Y
	//Y=>K

	//R=>S
	//S=>R

	// Value
	// ConnectsTo
}


public class Rotor
{

	private int CurrentIndex { get; set; }
	private RotorMapping map;
	private String Name { get; set; }

	private int RingOrientation { get; set; }

	private int NotchLocation { get; set; }

	public delegate void Advanced(String rotorName);
	public delegate void SignalReceived(int value, char c);

	// delegate pawlHandler
	public Rotor(String name, RotorMapping map, int ringOrientation, int notchLocation)
	{
		this.Name = name;
		this.map = map;
		this.RingOrientation = ringOrientation;
		this.NotchLocation = notchLocation;
	}

	public int Value(int position, RotorInputDirection direction)
	{
		int mv = map.Value(position, direction);
		if (direction == RotorInputDirection.Right)
			return mv + RingOrientation;
		else
			return mv - RingOrientation;
	}

	#region IndexFlipping

	public int SetCurrentIndex(int position)
	{
		CurrentIndex = position;
		return CurrentIndex;
	}

	// Set Ring Orientation to rotor
	public int SetRingOrientation(int position)
	{
		RingOrientation = position;
		return RingOrientation;
	}

	#endregion
	public int Advance()
	{
		if (CurrentIndex == NotchLocation)
		{
			// call delegate to Advance appropriate Rotor.
		}
		CurrentIndex = ((CurrentIndex + 1) % 26);

		return CurrentIndex;
	}


	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("RotorName=");
		sb.Append(Name);
		sb.Append(",CurrentIndex=");
		sb.Append(CurrentIndex);
		sb.Append(",RingOrientation=");
		sb.Append(RingOrientation);
		sb.Append(",NotchLocation=");
		sb.Append(NotchLocation);
		sb.Append(Environment.NewLine);
		sb.Append("Map:");
		sb.Append(map.ToString());
		return sb.ToString();
	}
}

public class RotorMapping
{
	
}




public class Plugboard
{

	List<Plug> plugs;
	public Plugboard()
	{
		plugs = new List<Plug>();
		ProcessInput pi = ProcessSignal;
	}
	
	private void AddPlugWire(Char a, Char b)
	{
		// if(! plugs.Contains(a
		// Add Plug(A, B)
		// if(! plugs.Contains(b
		// Add Plug(A, B)
	}
	

	public void AddPlug(Char A, Char B)
	{
		Plug newPlugA = new Plug(A, B);
		Plug newPlugB = new Plug(B, A);
		plugs.Add(newPlugA);
		plugs.Add(newPlugB);
	}
	
	public delegate Char ProcessInput(Char i);
	
	
	private Char ProcessSignal (Char s)
	{
		
		return s;
	}
	public Char SignalReceived(Char s)
	{
		// if plugwire map exists for s
		// run s through map
		// else
		// return s
		
		return s;
	}
	// collection of Plugwire(s)
	
	// ||
	// if Exists(Plug(S)
	// return PlugEncrypted(S)
	// else
	// return s
	
	// if Exists(Plugwire(s))
	// return PlugEncrypted(s)
	// else
	// return s
	
	// collection of Plug(s)
}

public class EnigmaMachine
{
	
	public delegate Char ProcessInput(Char i);
	
	private Plugboard plugboard;
	
	public EnigmaMachine()
	{
		plugboard = new Plugboard();
	}
	
	public void PressAKey(Char plainKey)
	{
		Char plugValue = plugboard.SignalReceived(plainKey);
	}
	
    public Char Encrypt ( Char k)
	{
		Char toLampBoard = k;// keyboard => plugboard => R1 => R2 => R3 => RN? => Reflector => R3 => R2 => R1 => Lampboard
		return toLampBoard;
	}
	
}

// keyboard => plugboard => R1 => R2 => R3 => RN? => Reflector => R3 => R2 => R1 => Lampboard

