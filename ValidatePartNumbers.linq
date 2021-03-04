<Query Kind="Statements" />

 bool IsDigitsOnly(String str)
{
    foreach ( char c in str)
    {
        if (c < '0' || c > '9')
        {
            return false;
        }
    }
    return true;
}

bool ValidPartNumber(String pn)
{
    bool isValid = false;

    if(pn.Length == 10)
    {
        bool digitsOnly = IsDigitsOnly(pn);
        if(digitsOnly)
        {
            isValid = true;
        }
    }
    return isValid;
}
		
string[] partNumbers = {"1000033110" , "4000021893", "3000000227", "3000000000227" , "ABCDEFGHIJ", "3000000A27"};

foreach(String pn in partNumbers)
{
   bool validPartNumber = ValidPartNumber(pn);
   String message = String.Format("{0} is valid: {1}", pn, validPartNumber);
   Console.WriteLine(message);

}



// test line
String testLine = "A L1 V- A L2 V- A L3 V- A L4 V- B L1 V- B L2 V- B L3 V- B L4 V-";

char firstChar = testLine.ToCharArray()[0];
if (Char.IsDigit(firstChar))
{
    Console.WriteLine("Starts with a digit");
}
else
{
	Console.WriteLine("Does not starts with a digit");
}


