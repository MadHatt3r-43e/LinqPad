<Query Kind="Statements" />

// Proto Process Start

String filename1 = @"C:\users\estrickland\Desktop\docWarehouse_prcl.pptx";
String filename2 = @"C:\users\estrickland\Desktop\Installation Guide.pdf";

ProcessStartInfo psi = new ProcessStartInfo();
psi.FileName = filename1;
Process.Start(psi);
