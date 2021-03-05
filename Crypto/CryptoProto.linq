<Query Kind="Statements" />


char testChar = 'Y';
int shiftIndex = 3;

// 
//char whoAmI = Mod26Alphabet.RightShift(testChar, shiftIndex);
//Console.WriteLine("Shifted Right: " + whoAmI);
//
//char whoAmI2 = Mod26Alphabet.LeftShift(whoAmI, shiftIndex);
//Console.WriteLine("Shifted Left: " + whoAmI2);

String testMessage = "thesunandthemaninthemoon";
String alphabetMessage = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
// testMessage = alphabetMessage;

Console.WriteLine("testMessage: " + testMessage + " length: " + testMessage.Length);

// MonoShift
//String encrMessage = MonoShift.Encrypt(testMessage, shiftIndex);
//Console.WriteLine("MonoShift encrMessage: " + encrMessage+ " length: " + encrMessage.Length);
//
//String decrMessage = MonoShift.Decrypt(encrMessage, shiftIndex);
//Console.WriteLine("MonoShift decrMessage: " + decrMessage+ " length: " + decrMessage.Length);
//
// Atbash
//String encrMessage2 = Atbash.Encrypt(testMessage);
//Console.WriteLine("Atbash encrMessage2: " + encrMessage2 + " length: " + encrMessage2.Length);
//
//String decrMessage2 = Atbash.Decrypt(encrMessage2);
//Console.WriteLine("Atbash decrMessage2: " + decrMessage2 + " length: " + decrMessage2.Length);

// Vigenere Square
String keyword = "KING";
String encrMessage3 = VigenereSquare.Encrypt(testMessage, keyword);
Console.WriteLine("VigenereSquare encrMessage3: " + encrMessage3 + " length: " + encrMessage3.Length);

String decrMessage3 = VigenereSquare.Decrypt(encrMessage3, keyword);
Console.WriteLine("VigenereSquare decrMessage3: " + decrMessage3 + " length: " + decrMessage3.Length);

int chunkSize = 5;
String chunkedMessage = MessageMunger.MessageChunkToUpper(decrMessage3, chunkSize);
Console.WriteLine("Chunked message in blocks of " + chunkSize + ".");
Console.WriteLine(chunkedMessage);


// Right/Left Shifting a character not in the alphabet set will 
public class Mod26Alphabet
{

	private static char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


	public static bool Contains(char c)
	{
		Int32 indexC = Index(c);
		if (indexC >= 0 && indexC <= 25)
			return true;
		else
			return false;
	}

	// return a value given index
	public static char Value(Int32 index)
	{
		if (index >= 0 && index <= 25)
		{
			return alphabet[index];
		}
		else
		{
			char blank = '-';
			return blank;
		}
	}

	// return an index given char
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
	

}

public static class VigenereSquare
{

	// Encrypt a single character
	private static char Encrypt(char plainTextLetter, char key)
	{
//		Int32 letterIndex = Mod26Alphabet.Index(letter);
//		Int32 keyIndex = Mod26Alphabet.Index(key);
//
//		Int32 encryptedIndex = Mod26Alphabet.RightShift(letterIndex, keyIndex);
//		return Mod26Alphabet.Value(encryptedIndex);

		Int32 keyIndex = Mod26Alphabet.Index(key);
		char encrypted = Mod26Alphabet.RightShift(plainTextLetter, keyIndex);
		return encrypted;
	}

	private static char Decrypt(char encryptedTextLetter, char key)
	{
		//Int32 encryptedTextLetterIndex = Mod26Alphabet.Index(encryptedTextLetter);
		Int32 keyIndex = Mod26Alphabet.Index(key);
		char decrypted = Mod26Alphabet.LeftShift(encryptedTextLetter, keyIndex);
		return decrypted;
	}

	// Encrypt a message 
	public static String Encrypt(String plainTextMessage, String keyword)
	{

		StringBuilder encrypted = new StringBuilder();
		Int32 keywordLength = keyword.Length;

		Int32 keywordIterator = 0;

		plainTextMessage = plainTextMessage.ToUpper();
		keyword = keyword.ToUpper();

		char[] messageArray = plainTextMessage.ToCharArray();
		foreach (char c in messageArray)
		{
			char encryptedChar = Encrypt(c, Convert.ToChar(keyword[keywordIterator]));
			
			// increment down keyword
			keywordIterator++;
			if (keywordIterator % keywordLength == 0)
			{
				keywordIterator = 0;
			}

			encrypted.Append(encryptedChar.ToString());
		}
		return encrypted.ToString();
	}

	public static String Decrypt(String encryptedTextMessage, String keyword)
	{

		StringBuilder decrypted = new StringBuilder();
		Int32 keywordLength = keyword.Length;

		Int32 keywordIterator = 0;

		encryptedTextMessage = encryptedTextMessage.ToUpper();
		keyword = keyword.ToUpper();

		char[] messageArray = encryptedTextMessage.ToCharArray();
		foreach (char c in messageArray)
		{
			char decryptedChar = Decrypt(c, Convert.ToChar(keyword[keywordIterator]));

			// increment down keyword
			keywordIterator++;
			if (keywordIterator % keywordLength == 0)
			{
				keywordIterator = 0;
			}

			decrypted.Append(decryptedChar.ToString());
		}
		return decrypted.ToString();
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

public static class MessageMunger
{

	// returns message divided into messgeBlockSize words
	// returns message uppercase
	// removes any spaces from original message
	public static String MessageChunkToUpper(String message, int messageBlockSize)
	{
		int chunkIterator = 0;
		String chunkedMessage = String.Empty;
		StringBuilder sb = new StringBuilder();
		char[] messageArray = message.ToCharArray();
		foreach (char c in messageArray)
		{
			chunkIterator++;
			String strC = c.ToString();
			strC = strC.ToUpper();
			if (Mod26Alphabet.Contains(Convert.ToChar(strC)))
			{
				sb.Append(strC);
				
				if (chunkIterator == messageBlockSize )
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

public static class Atbash
{
	private static char Encrypt(char plainTextLetter)
	{
		Int32 zIndex = 25;
		Int32 plainTextLetterIndex = Mod26Alphabet.Index(plainTextLetter);
		return Mod26Alphabet.Value(zIndex - plainTextLetterIndex);
	}

	public static char Decrypt(char plainTextLetter)
	{
		return Encrypt(plainTextLetter);
	}
	
	public static String Encrypt(String plainTextMessage)
	{
		StringBuilder encrypted = new StringBuilder();

		char[] messageArray = plainTextMessage.ToCharArray();
		foreach (char c in messageArray)
		{
			char encC = Encrypt(c);
			String strC = encC.ToString();
			encrypted.Append(strC);
		}
		return encrypted.ToString();
	}

	public static String Decrypt(String plainTextMessage)
	{
		StringBuilder encrypted = new StringBuilder();

		char[] messageArray = plainTextMessage.ToCharArray();
		foreach (char c in messageArray)
		{
			char encC = Decrypt(c);
			String strC = encC.ToString();
			encrypted.Append(strC);
		}
		return encrypted.ToString();
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


