<Query Kind="Statements" />


// Plug Play
// plugs contains a Plug for every key that maps to another key
//List<Plug> plugs = new List<Plug>();
//
//// plugwire A-B
//Plug pAB = new Plug('A', 'B');
//Plug pBA = new Plug('B', 'A');
//
//Plug pTJ = new Plug('T', 'J');
//Plug pJT = new Plug('J', 'T');

//plugs.Add(pAB);
//plugs.Add(pBA);
//plugs.Add(pTJ);
//plugs.Add(pJT);
//
//Char searchChar = 'A';
//bool plugAExists = plugs.Any(p => p.value == searchChar);
//Console.WriteLine("Plugs has plug " + searchChar + " == " + plugAExists);
//
//searchChar = 'B';
//bool plugAExistsb = plugs.Any(p => p.value == searchChar);
//Console.WriteLine("Plugs has plug " + searchChar + " == " + plugAExistsb);
//
//searchChar = 'T';
//bool plugAExistsc = plugs.Any(p => p.value == searchChar);
//Console.WriteLine("Plugs has plug " + searchChar + " == " + plugAExistsb);
//
//searchChar = 'W';
//bool plugAExistsw = plugs.Any(p => p.value == searchChar);
//Console.WriteLine("Plugs has plug " + searchChar + " == " + plugAExistsw);

//plugs.Dump();
// -------------------------------



// Plugboard

//ReturnInput ri = ShowPlugboardOutput;
//// delegate to handle 
//void ShowPlugboardOutput(Char c)
//{
//	Console.WriteLine(" returned " + c.ToString() + " from the plugboard.");
//}
//
//Plugboard pgBoard = new Plugboard(null, null);
//	pgBoard.AddPlugwire('A','B');
//	pgBoard.AddPlugwire('J','T');
//	pgBoard.AddPlugwire('R','S');
//	pgBoard.AddPlugwire('Y','D');
//	pgBoard.AddPlugwire('Y','D');
//
//Console.WriteLine(pgBoard.ToString());
//
//Char keyEntered = 'J';
//Char keyOutput = pgBoard.ProcessSignal(keyEntered);
//Console.WriteLine(keyEntered.ToString() + " returned " + keyOutput.ToString() + " from the plugboard.");
//keyEntered = 'A';
//keyOutput = pgBoard.ProcessSignal(keyEntered);
//Console.WriteLine(keyEntered.ToString() + " returned " + keyOutput.ToString() + " from the plugboard.");
//keyEntered = 'B';
//keyOutput = pgBoard.ProcessSignal(keyEntered);
//Console.WriteLine(keyEntered.ToString() + " returned " + keyOutput.ToString() + " from the plugboard.");
//keyEntered = 'T';
//keyOutput = pgBoard.ProcessSignal(keyEntered);
//Console.WriteLine(keyEntered.ToString() + " returned " + keyOutput.ToString() + " from the plugboard.");
//keyEntered = 'E';
//
//Console.WriteLine();
//Console.WriteLine("Enigma machine output below.........");





// EnigmaMachine delegates
OutputMessage om = ShowOutput;
void ShowOutput(String message)
{
	Console.WriteLine(message);
}
ShowProcess sp = ShowObjectProcessing;
void ShowObjectProcessing(Char input, Char output, String processingObject)
{
	Console.WriteLine(processingObject + ": " + input.ToString() + " => " + output.ToString());
}

// Messages
String testMessage = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
testMessage = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ";

testMessage = "C";
//testMessage = "AUERJCTNMQQTXFYPAHIVCTYRSIAABKBAIWFLCHHKTKPXWSXAYGSCMDCPKDAURWZWQWUFBGGTYTAJZGSSCBCGOLYLQOVIVR";

//testMessage = "THEBROWNFOX";




const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXY";
Random random = new Random();
String stuff = new String(Enumerable.Repeat(chars, 2500)
  .Select(s => s[random.Next(s.Length)]).ToArray());
// testMessage = stuff;  

Console.WriteLine("Test Execution");Console.WriteLine();

// Setup machine
EnigmaMachine em = new EnigmaMachine(om, sp );

// line 325 == rotor settings
em.SetRotorPosition(1, 'A');
em.SetRotorPosition(2, 'A');
em.SetRotorPosition(3, 'A');


String returnedMessage = em.EncryptMessage(testMessage);
Console.WriteLine(testMessage);
Console.WriteLine(returnedMessage);

public abstract class EnigmaObject
{
	public abstract Char ProcessSignal(Char input);
}

// delegate that issues output of function on delegate
public delegate void ReturnInput(Char c);
// shows input and output
public delegate void ShowProcess(Char input, Char output, String processingObject);

public delegate void OutputMessage(String message);

public delegate void SignalReflected(Char c);
public delegate void RotorAdvancementChannel(Int16 slotNumber, String name, Char advancedOnCharacterPosition);
	
public class EnigmaMachine : EnigmaObject
{
	private  List<EnigmaObject> objects;
	
	// delegates
	private ShowProcess sp;
	private OutputMessage om;
	private SignalReflected sf;
	
	// signal output from the reflector
	private Char latchedReflectedSignal;
	
	public EnigmaMachine(OutputMessage om = null, ShowProcess sp = null, String config = null)
	{
		if (om != null)
		{
			this.om = om;
		}
		if (sp != null)
		{
			this.sp = sp;
		}
		
		objects = new List<EnigmaObject>();
		
		Plugboard pgBoard = new Plugboard(om, sp);
		objects.Add(pgBoard);

		RotorAssembly ra = new RotorAssembly(om, sp);
		objects.Add(ra);
		
		sf = ReceiveReflectedSignal;
		String reflectorName = "C";
		Reflector reflector = new Reflector(reflectorName, sf,  om, sp);
		objects.Add(reflector);
		
		// reflector needs to know how to propogate signal back through the RotorAssembly in reverse
		
		InitMachine();
	}
	
	public void ReceiveReflectedSignal(Char c)
	{
		
		latchedReflectedSignal = c;
		om("Latched onto machine from reflector: " + c.ToString());
	}
	// User Settings after machine built
	public void InitMachine()
	{
		//AddPlugwire('J', 'T');
		//AddPlugwire('R', 'S');
		//AddPlugwire('Y', 'D');
		//AddPlugwire('Y', 'D');

	}
	
	public String EncryptMessage(String inputMessage, int chunkSize = 5)
	{
		String encryptedMessage = String.Empty;
		StringBuilder encryptedSB = new StringBuilder();
		foreach (Char c in inputMessage.ToCharArray())
		{
			Char encrypted = ProcessSignal(c);
			encryptedSB.Append(encrypted.ToString());
		}
		encryptedMessage = encryptedSB.ToString();

		return encryptedMessage; // MessageChunk(encryptedMessage, chunkSize);
	}
	
	override public Char ProcessSignal(Char c)
	{
		if(om != null) om(Environment.NewLine + "Processing character: " + c.ToString());
		Char output = c;
		
		// PlugBoard => Rotors => Reflector
		foreach(EnigmaObject o in objects)
		{
			output = o.ProcessSignal(output);
		}

		// => Back propogate through rotors
		 RotorAssembly ra = (RotorAssembly)objects.Find(t => t is RotorAssembly);
		 Char rotorOutput = ra.BackProcessSignal(latchedReflectedSignal);
		
		// => Lastly, back through the Plugboard
		Plugboard pb = (Plugboard)objects.Find(t => t is Plugboard);
		output = pb.ProcessSignal(rotorOutput);
		
		if(om != null) om("Enigma Machine returned " + output.ToString() + " for input "  + c.ToString());
		return output;
		// delegate output once done to Lampboard
	}

	public bool AddPlugwire(Char a, Char b)
	{
		Plugboard pg = (Plugboard)objects.Find(t => t is Plugboard);
		bool added = pg.AddPlugwire(a, b);
		return added;
	}
	
	// 
	public bool SetRotorPosition(int rotorNumber, Char c)
	{
		RotorAssembly ra = (RotorAssembly)objects.Find(t => t is RotorAssembly);
		bool success = ra.SetRotorPosition(rotorNumber, c);
		return success;
	}

}



public class RotorAssembly : EnigmaObject
{
	
	private OutputMessage om;
	private ShowProcess sp;
	RotorAdvancementChannel rac;
	
	private List<Rotor> rotors;
	public RotorAssembly(OutputMessage om = null, ShowProcess sp = null , String config = null)
	{
		if (om != null)
		{
			this.om = om;
		}
		if (sp != null)
		{
			this.sp = sp;
		}
		rotors = new List<Rotor>();
		
		rac = ReceiveRotorAdvancement;
		// drive with config file
		AssembleRotors();
		
	}

	override public Char ProcessSignal(Char signal)
	{
		AdvanceRotors();
		Char output = signal;
		foreach(Rotor r in rotors)
		{
			output = r.ProcessSignal(output);
		}
		return output;
	}
	
	// run through the rotors in reverse
	public Char BackProcessSignal(Char signal)
	{
		Char output = signal;
		for(int i = rotors.Count - 1; i > -1; i--)
		{
			Char tmpOut = output;
			// select rotor
			Rotor r = rotors[i];
			
			// go from Value to Key 
			output  = r.map.FirstOrDefault(x => x.Value == output).Key;
			if (sp != null)  sp(tmpOut, output, String.Format("Rotor {0}: Reverse", r.name));	
		}
		if (sp != null)  sp(signal, output, String.Format("Rotor Assembly: Reverse"));
		return output;
	}

	private void AdvanceRotors()
	{
		// Advance the rotors by manually advancing the first rotor
		// The cascade of rotor advancements will happen on delegates
		// simply bump the first rotor
		Rotor r = rotors[0];
		r.Advance();
		if (om != null) om("Advance Rotors...");
	}
	
	private void AssembleRotors()
	{
		
		// Do I want the rotor to know where it is in the machine?
		if(om != null) om("Assemble Rotors");
		Rotor r1 = new Rotor("I"   , 1, rac, om, sp);
		Rotor r2 = new Rotor("III" , 2, rac, om, sp);
		Rotor r3 = new Rotor("IV"  , 3, rac, om, sp);
		bool success;


		Char rotorPlacement = 'A';
		Char ringSetting = 'A';
		success = AddRotor(r1, rotorPlacement, ringSetting);
		success = AddRotor(r2, rotorPlacement, ringSetting);
		success = AddRotor(r3, rotorPlacement, ringSetting);
	}
	
	private bool AddRotor(Rotor r, Char rotorPlacement, Char ringSetting)
	{
		if(r == null)
		{
			if(om != null) om("Rotor is null");
			return false;
		}
		bool rotorAdded = true;
		try
		{
			r.position = rotorPlacement;
			r.RingSetting(ringSetting);
			rotors.Add(r);
			if(om != null) om("Added rotor " + r.name.ToString());
		}
		catch ( Exception ex)
		{
			rotorAdded = false;
			if(om != null) om("Assemble Rotors");
		}
		return rotorAdded;
	}
	
	// delegate function to handle rotor advancement signals
	void ReceiveRotorAdvancement (Int16 slotNumberAdvanced, String name, Char advancedOnCharacterPosition)
	{
		// which rotor triggered it's notch 
		// call advance on rotor to it's left
		
		// find next rotor to advance
		Rotor r = rotors.Find(r => r.assemblySlot == slotNumberAdvanced + 1);
		if( r != null )
		{
			r.Advance();
		}
	}
	
	public bool SetRotorPosition(int rotorNum, Char c)
	{
		bool success = false;
		try
		{
			Rotor r = rotors.Find(r => r.assemblySlot == rotorNum);
			r.position = c;
			success = true;
		}
		catch(Exception ex)
		{
			if(om != null) om("Error setting rotor");
		}
		return success;
	}
	
}

// A == Char 'A'
// A == Uint16 65
// Offsets internally will use UInt16 65 - 90

public class Rotor : EnigmaObject
{
	// constants
	private readonly static UInt16 lastOfAlphabet = 90;
	private readonly static UInt16 startOfAlphabet = 65;
	
	// delegates
	private OutputMessage om;
	private ShowProcess sp;
	private RotorAdvancementChannel rac;
	
	// wiring map:  input, output
	public readonly Dictionary<Char, Char> map;
	
	// position inwhich to signal next rotor to advance
	private readonly Char notch;
	
	// name of rotor:  Roman numeral representation
	public readonly String name;
	
	// Ring setting.  This is where the rings letter A is placed.  
	// ie.  Ring == A sits over rotor position G.
	// Ring setting is an offset.
	private Char ringSetting;
	// UInt16 - Zero based ordinal position of of Ring.  A == 0, Z == 25
	private UInt16 ringSettingInt;
	
	// Position of rotor of rotor in machine as shown by letter facing from Ring.  
	// As the machine operates, the right-most rotor position changes with each keypress.
	public Char position;
	// buffers the last value output from the rotor.  Simply helpful in sending messages on delegate.
	private Char lastOutput;
	
	
	// 1 based slot number.  Populate R to L.  Rightmost == 1. 
	public Int16 assemblySlot;
	
	public Rotor(String Name
	, Int16 Slot
	, RotorAdvancementChannel rac
	, OutputMessage om = null
	, ShowProcess sp = null
	, String config = null)
	{
		name = Name;
		assemblySlot = Slot;
		notch = MapConfig.Notch(Name);
		map = MapConfig.Rotor(Name);
		
		this.rac = rac;
		
		if (om != null)
		{
			this.om = om;
		}
		if (sp != null)
		{
			this.sp = sp;
		}
	}

	// All rotors first advance, then the signal is processed
	public void Advance()
	{
		// detect if this is the position to send signal to advance rotor to left
		if (ShouldAdvanceNextRotor())
		{
			rac(this.assemblySlot, this.name, this.position);
		}

		position = IncrementPosition(position);
		if (om != null)
		{
			String advanceInfo = "Rotor " + name + " position advanced to " + position.ToString();
			om(advanceInfo);
		}
	}

	// have we reached the notch?
	private bool ShouldAdvanceNextRotor()
	{
		bool advanceNextRotor = false;
		
		// the notch is on the ring.  position 
		if (position == notch)
		{
			advanceNextRotor = true;
		}
		return advanceNextRotor;
	}
	
	// coming off of ETW or other rotor
	override public Char ProcessSignal(Char signal)
	{
		// signal indicates where on the rotor the signal enters.  Rotating the rotor == advancing the position
		UInt16 indexSignal = OrdinalChar(signal);
		
		// position of the rotor as indicated by the Ring
		UInt16 indexPosition = OrdinalChar(position);
		
		// The key is the signal
		UInt16 indexMapKey = (UInt16)( ( (indexSignal + indexPosition + ringSettingInt) %26) + startOfAlphabet);
		
		Char mapKey = Convert.ToChar(indexMapKey);
		if (!map.ContainsKey(mapKey))
		{
			if (om != null) om(String.Format("KEY NOT FOUND: Rotor {0} , Position: {1}, Signal {2}", name, position.ToString(), signal.ToString()));
			var rand = new Random();
			UInt16 randIntChar = (UInt16)rand.Next(65, 91);
			return Convert.ToChar(randIntChar);
		}

		Char output = map[mapKey];

		if (sp != null) sp(signal, output, String.Format("Rotor {0}", this.name));
		lastOutput = output;
		if (om != null)
		{
			String info = String.Format("Rotor {0}: Signal: {1} Position: {2} Last Output: {3} Ringsetting: {4}",
				name, signal.ToString(), position.ToString(), output.ToString(), ringSetting.ToString());
			om(info);

		}
		return output;
	}
	
	// ringsetting is the character said to be 'the dot'  ie.  it's a character, it's also ordinal position away from 'A'
	public void RingSetting(Char rs)
	{
		ringSetting = rs;
		// zero based ordinal position of ringsetting
		ringSettingInt = OrdinalChar(rs);
	}
	
	private UInt16 CharInt(Char c)
	{
		return (UInt16)(Convert.ToUInt16(c));
	}
	
	// zero indexed ordinal position of Char ie. A == 0  Z == 25
	private UInt16 OrdinalChar(Char a)
	{
		return (UInt16)( CharInt(a) - startOfAlphabet);
	}
	// used to increment position
	// Increments one
	private Char IncrementPosition ( Char position)
	{
		Char newPosition = ++position > (UInt16)lastOfAlphabet ? 'A' : position;
		return newPosition;
	}
	
	
}


public class Reflector : EnigmaObject
{
	private String Name { get; set;}
	SignalReflected sf;
	OutputMessage om;
	ShowProcess sp;
	private Dictionary<Char, Char> map;
	
	public Reflector(String reflectorName
	, SignalReflected sf
	, OutputMessage om = null
	, ShowProcess sp = null)
	{
		map = MapConfig.Reflector(reflectorName);
		this.sf = sf;
		
		if (om != null)
		{
			this.om = om;
		}
		if(sp != null)
		{
			this.sp = sp;
		}
	}
	override public Char ProcessSignal(Char signal)
	{
		Char output = map[signal];
		
		// issue output of reflector on delegate
		sf(output);
		if(sp != null)
		{
			sp(signal, output, "Reflector");
		}
		return output;
	}
}


// the plugboard contains a list of plugs
// for each wire connecting two plugs  A <=> B, a plug will exist in the plugboard for A => B and B => A
public class Plugboard : EnigmaObject
{
	// delegates
	ShowProcess sp;
	OutputMessage om;
	
	List<Plug> plugs;
	
	public Plugboard(OutputMessage om = null, ShowProcess sp = null)
	{
		plugs = new List<Plug>();
		if( sp != null )
		{
			this.sp = sp;
		}
		if (om != null)
		{
			this.om = om;
		}
	}
	
	override public Char ProcessSignal(Char signal)
	{
		// if a plug does not exist, the signal will be passed through
		Char output = signal;
		bool plugExists = plugs.Any(p => p.value == signal);
		if (plugExists)
		{
			// Find plug
			Plug p = plugs.Find(p => p.value == signal);
			output = p.pointsTo;
		}

		if (sp != null)
		{
			sp(signal, output, "Plugboard");
		}
		return output;
	}

	// Adding one logical plugwire between A && B will add two plugs:  A=>B, B=>A
	public bool AddPlugwire(Char A, Char B)
	{
		bool plugAExists = plugs.Any(p => p.value == A);
		bool plugBExists = plugs.Any(p => p.value == B);
		if (plugAExists || plugBExists)
		{
			String errorMessage = "Cannot add this configuration to plugboard: " + A.ToString() + " => " + B.ToString();
			if (om != null) om(errorMessage);
			return false;
		}

		try
		{
			Plug newPlugA = new Plug(A, B);
			Plug newPlugB = new Plug(B, A);
			plugs.Add(newPlugA);
			plugs.Add(newPlugB);
			if (om != null)
			{
				String addedPlugwireMessage = String.Format("Added plug wire {0} => {1}", A.ToString(), B.ToString());
				om(addedPlugwireMessage);
			}
			return true;
		}
		catch( Exception ex)
		{
			if (om != null)
			{
				om("Failed to add plug");
			}
			return false;
		}
	}
	
	public override String ToString()
	{
		String plugboardString = "Plugboard =>" + Environment.NewLine;
		foreach(Plug p in plugs)
		{
			plugboardString += p.ToString() + Environment.NewLine;
		}
		return plugboardString;
	}
	
}


// a plug is akin to a vertex in a directed graph
// A plug contains a mapping from one position on the plugboard to another.
// A plug is directed, not biDirectional
public class Plug : IEquatable<Plug>
{
	public  Char value;
	public  Char pointsTo;

	public Plug(Char v, Char p)
	{
		value = v;
		pointsTo = p;
	}

	public override String ToString()
	{
		return String.Format("{0}=>{1}", value, pointsTo);
	}
	
	public override int GetHashCode()
	{
		return (int)value;
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
	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

}

// setup
public static class MapConfig
{
	public static Dictionary<Char, Char> Reflector(String name)
	{
		Dictionary<Char, Char> map = new Dictionary<char, char>();
		if (name == "C")
		{
			map.Add('A', 'F');
			map.Add('B', 'V');
			map.Add('C', 'P');
			map.Add('D', 'J');
			map.Add('E', 'I');
			map.Add('F', 'A');
			map.Add('G', 'O');
			map.Add('H', 'Y');
			map.Add('I', 'E');
			map.Add('J', 'D');
			map.Add('K', 'R');
			map.Add('L', 'Z');
			map.Add('M', 'X');
			map.Add('N', 'W');
			map.Add('O', 'G');
			map.Add('P', 'C');
			map.Add('Q', 'T');
			map.Add('R', 'K');
			map.Add('S', 'U');
			map.Add('T', 'Q');
			map.Add('U', 'S');
			map.Add('V', 'B');
			map.Add('W', 'N');
			map.Add('X', 'M');
			map.Add('Y', 'H');
			map.Add('Z', 'L');
		}
		return map;
	}

	public static Char Notch(String name)
	{
		Char notch = 'A';

		switch (name)
		{
			case "I":
				notch = 'Q';
				break;
			case "II":
				notch = 'E';
				break;
			case "III":
				notch = 'V';
				break;
			case "IV":
				notch = 'J';
				break;
		}
		return notch;
	}
	public static Dictionary<Char, Char> Rotor(String name)
	{
		Dictionary<Char, Char> map = new Dictionary<char, char>();
		if (name == "I")
		{
			map.Add('A', 'E');
			map.Add('B', 'K');
			map.Add('C', 'M');
			map.Add('D', 'F');
			map.Add('E', 'L');
			map.Add('F', 'G');
			map.Add('G', 'D');
			map.Add('H', 'Q');
			map.Add('I', 'V');
			map.Add('J', 'Z');
			map.Add('K', 'N');
			map.Add('L', 'T');
			map.Add('M', 'O');
			map.Add('N', 'W');
			map.Add('O', 'Y');
			map.Add('P', 'H');
			map.Add('Q', 'X');
			map.Add('R', 'U');
			map.Add('S', 'S');
			map.Add('T', 'P');
			map.Add('U', 'A');
			map.Add('V', 'I');
			map.Add('W', 'B');
			map.Add('X', 'R');
			map.Add('Y', 'C');
			map.Add('Z', 'J');
		}
		if (name == "II")
		{
			map.Add('A', 'A');
			map.Add('B', 'J');
			map.Add('C', 'D');
			map.Add('D', 'K');
			map.Add('E', 'S');
			map.Add('F', 'I');
			map.Add('G', 'R');
			map.Add('H', 'U');
			map.Add('I', 'X');
			map.Add('J', 'B');
			map.Add('K', 'L');
			map.Add('L', 'H');
			map.Add('M', 'W');
			map.Add('N', 'T');
			map.Add('O', 'M');
			map.Add('P', 'C');
			map.Add('Q', 'Q');
			map.Add('R', 'G');
			map.Add('S', 'Z');
			map.Add('T', 'N');
			map.Add('U', 'P');
			map.Add('V', 'Y');
			map.Add('W', 'F');
			map.Add('X', 'V');
			map.Add('Y', 'O');
			map.Add('Z', 'E');
		}
		if (name == "III")
		{
			map.Add('A', 'B');
			map.Add('B', 'D');
			map.Add('C', 'F');
			map.Add('D', 'H');
			map.Add('E', 'J');
			map.Add('F', 'L');
			map.Add('G', 'C');
			map.Add('H', 'P');
			map.Add('I', 'R');
			map.Add('J', 'T');
			map.Add('K', 'X');
			map.Add('L', 'V');
			map.Add('M', 'Z');
			map.Add('N', 'N');
			map.Add('O', 'Y');
			map.Add('P', 'E');
			map.Add('Q', 'I');
			map.Add('R', 'W');
			map.Add('S', 'G');
			map.Add('T', 'A');
			map.Add('U', 'K');
			map.Add('V', 'M');
			map.Add('W', 'U');
			map.Add('X', 'S');
			map.Add('Y', 'Q');
			map.Add('Z', 'O');
		}
		if (name == "IV")
		{
			map.Add('A', 'E');
			map.Add('B', 'S');
			map.Add('C', 'O');
			map.Add('D', 'V');
			map.Add('E', 'P');
			map.Add('F', 'Z');
			map.Add('G', 'J');
			map.Add('H', 'A');
			map.Add('I', 'Y');
			map.Add('J', 'Q');
			map.Add('K', 'U');
			map.Add('L', 'I');
			map.Add('M', 'R');
			map.Add('N', 'H');
			map.Add('O', 'X');
			map.Add('P', 'L');
			map.Add('Q', 'N');
			map.Add('R', 'F');
			map.Add('S', 'T');
			map.Add('T', 'G');
			map.Add('U', 'K');
			map.Add('V', 'D');
			map.Add('W', 'C');
			map.Add('X', 'M');
			map.Add('Y', 'W');
			map.Add('Z', 'B');
		}
		return map;
	}

}




