<Query Kind="Statements" />

foreach (var prop in new Microsoft.WindowsAPICodePack.Shell.PropertySystem.ShellPropertyCollection(@"C:\Users\estrickland\Desktop\MotivePowerSCD\1500000036_SCD_Rev00.pdf"))
{
    Console.WriteLine(prop.CanonicalName + "=" + prop.ValueAsObject);
}