<Query Kind="Statements" />


char testChar = 'Y';
int shiftIndex = 3;

// 
//char whoAmI = Mod26Alphabet.RightShift(testChar, shiftIndex);
//Console.WriteLine("Shifted Right: " + whoAmI);
//
//char whoAmI2 = Mod26Alphabet.LeftShift(whoAmI, shiftIndex);
//Console.WriteLine("Shifted Left: " + whoAmI2);

String testMessage = "MY TESTMESSAGE";
Console.WriteLine("testMessage: " + testMessage);
String encrMessage = MonoShift.Encrypt(testMessage, shiftIndex);
Console.WriteLine("encrMessage: " + encrMessage);

String decrMessage = MonoShift.Decrypt(encrMessage, shiftIndex);
Console.WriteLine("decrMessage: " + decrMessage);

public class Mod26Alphabet
{

	private static char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


	public static bool Contains(char c)
	{
		Int32 indexC = Index(c);
		if (indexC >= 0 || indexC <= 25)
			return true;
		else
			return false;
	}

	public static char Value(Int32 index)
	{
		return alphabet[index];
	}

	public static Int32 Index(char letter)
	{
		return Array.IndexOf(alphabet, letter);
	}


	
	private static Int32 RightShift(Int32 index, Int32 offset)
	{
		return (index + offset) % 26;
	}

	// right shifted character
	public static char RightShift(char letter, Int32 offset)
	{
		if (Contains(letter))
		{
			Int32 letterIndex = Index(letter);
			Int32 offsetIndex = RightShift(letterIndex, offset);
			return Value(offsetIndex);
		}
		else
			return letter;
	}


	private static Int32 LeftShift(Int32 index, Int32 offset)
	{
		return (index - offset + 26 ) % 26;
	}

	public static char LeftShift(char letter, Int32 offset)
	{
		if (Contains(letter))
		{
			Int32 letterIndex = Index(letter);
			Int32 offsetIndex = LeftShift(letterIndex, offset);
			return alphabet[offsetIndex];
		}
		else
			return letter;
	}
	
	// returns message divided into messgeBlockSize words
	// returns message uppercase
	// removes any spaces from original message
	public static String MessageChunk(String message, int messageBlockSize)
	{
		int chunkIterator = 0;
		String chunkedMessage = String.Empty;
		StringBuilder sb = new StringBuilder();
		char[] messageArray = message.ToCharArray();
		foreach (char c in messageArray)
		{
			String strC = c.ToString();
			strC = strC.ToUpper();
			if (Mod26Alphabet.Contains(Convert.ToChar(strC)))
			{
				sb.Append(strC);
				chunkIterator++;
				if (chunkIterator == messageBlockSize - 1)
				{
					sb.Append(" ");
					chunkIterator = 0;
				}
			}
		}
		chunkedMessage = sb.ToString();
		return chunkedMessage;
	}
}


public static class MonoShift
{

	private static char Encrypt(char plainLetter, Int32 shiftIndex)
	{
		return Mod26Alphabet.RightShift(plainLetter, shiftIndex);
	}

	public static String Encrypt(String plainMessage, Int32 shiftIndex)
	{
		StringBuilder encrypted = new StringBuilder();

		char[] messageArray = plainMessage.ToCharArray();
		foreach (char c in messageArray)
		{
			char encC = Encrypt(c, shiftIndex);
			encrypted.Append(encC.ToString());
		}
		return encrypted.ToString();
	}

	private static char Decrypt(char encryptedLetter, Int32 shiftIndex)
	{
		return Mod26Alphabet.LeftShift(encryptedLetter, shiftIndex);
	}
	
	public static String Decrypt(String encryptedMessage, Int32 shiftIndex)
	{
		StringBuilder decrypted = new StringBuilder();

		char[] messageArray = encryptedMessage.ToCharArray();
		foreach (char c in messageArray)
		{
			char encC = Decrypt(c, shiftIndex);
			String strC = encC.ToString();
			decrypted.Append(strC);
		}
		return decrypted.ToString();
	}

}


//public static class VigenereSquare
//{
//
//	// Encrypt a single character
//	private static char Encrypt(char letter, char key)
//	{
////		Int32 letterIndex = Mod26Alphabet.Index(letter);
////		Int32 keyIndex = Mod26Alphabet.Index(key);
////
////		Int32 encryptedIndex = Mod26Alphabet.RightShift(letterIndex, keyIndex);
////		return Mod26Alphabet.Value(encryptedIndex);
//	}
//
//	// Encrypt a message 
//	public static String Encrypt(String message, String keyword)
//	{
//
//		StringBuilder sb = new StringBuilder();
//		Int32 keywordLength = keyword.Length;
//
//		Int32 keywordIterator = 0;
//
//		message = message.ToUpper();
//		keyword = keyword.ToUpper();
//
//		char[] messageArray = message.ToCharArray();
//		foreach (char c in messageArray)
//		{
//			char encC = Encrypt(c, Convert.ToChar(keyword[keywordIterator]));
//			keywordIterator++;
//			if (keywordIterator % keywordLength == 0)
//			{
//				keywordIterator = 0;
//			}
//
//			//keywordIterator = ((keywordIterator % keywordLength-1) == 0) ? 0 : keywordIterator += 1;
//			String strC = encC.ToString();
//			sb.Append(strC);
//		}
//		return sb.ToString();
//	}
//
//}


