<Query Kind="Statements" />

            Worksheet wsheet = Excel_Library.ExcelAccess.GetActiveWorksheet(uiMessageHandler);

            StdReturnObject objCol1 = Excel_Library.ExcelAccess.GetColumnN(wsheet, 9, 3, 0, uiMessageHandler);

            List<String> filenames = (List<String>)objCol1.returnedObject;

            foreach (String filename in filenames)
            {

                // doctype

                // if Word doc

                // switch appropriate application type per doc type

                //Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                //Microsoft.Office.Interop.Word.Document wordDocument = appWord.Documents.Open(@"C:\users\estrickland\desktop\1000012612_SPEC_SHEET_REV00.doc");
                //wordDocument.ExportAsFixedFormat(@"C:\users\estrickland\desktop\1000012612_SPEC_SHEET_REV00.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);



            }

