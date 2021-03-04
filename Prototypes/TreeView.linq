<Query Kind="Statements">
  <Reference>C:\PurcellTools\PurcellTools\Purcell.Tools.SAP_Library\bin\Debug\Purcell.Tools.SAP_Library.dll</Reference>
  <Reference>C:\PurcellTools\PurcellTools\Std_Library\bin\Debug\Purcell.Tools.Std_Library.dll</Reference>
  <Reference>C:\PurcellTools\PurcellTools\Std_Library\bin\Debug\System.IO.Compression.FileSystem.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
</Query>

// https://www.linqpad.net/CustomVisualizers.aspx

System.Windows.Forms.TreeNode AddNode(System.Windows.Forms.TreeNode tn, String newItem)
{
	tn.Nodes.Add(newItem);
	return tn.LastNode;

}

Purcell.Tools.SAP_Library.SAPBOMData bomData = null;
var sBOM = File.ReadAllText(@"c:\users\estrickland\desktop\tmp\BOMDATA\2-5995.dat");
        byte[] b = Convert.FromBase64String(sBOM);
        using (var stream = new MemoryStream(b))
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            bomData =  (Purcell.Tools.SAP_Library.SAPBOMData)formatter.Deserialize(stream);
        }

bomData.Dump();

var tv1 = new System.Windows.Forms.TreeView();

// http://csharphelper.com/blog/2014/09/load-a-treeview-from-a-tab-delimited-file-in-c/

tv1.BeginUpdate();

	bool showExpandedList = false;
	String formatString = "{0} | {1} | Level={2}";
	tv1.BeginUpdate();

	System.Collections.Generic.Dictionary<int, System.Windows.Forms.TreeNode> parents = new System.Collections.Generic.Dictionary<int, System.Windows.Forms.TreeNode>();
	for(int i = 0; i< bomData.bom.Count; i++)
	{ 
		
		int currentLevel = bomData.bom[i].intLevel;
		// Add the TreeNode to the TreeView and to parents
		if(currentLevel == 0)
		{
		   parents.Add(0, tv1.Nodes.Add(String.Format(formatString, bomData.bom[i].partNumber, bomData.bom[i].description, bomData.bom[i].intLevel)));
		}
		else
		{
			if(parents.ContainsKey(currentLevel) == false)
			{
				parents.Add(currentLevel, parents[currentLevel - 1].Nodes.Add(String.Format(formatString, bomData.bom[i].partNumber, bomData.bom[i].description, bomData.bom[i].intLevel)));
			}
			else
			{
				parents[currentLevel] = parents[currentLevel - 1].Nodes.Add(String.Format(formatString, bomData.bom[i].partNumber, bomData.bom[i].description, bomData.bom[i].intLevel));
			}
			if(showExpandedList)
			{
				parents[currentLevel].EnsureVisible();
			}
		}
			
	}
	tv1.EndUpdate();
PanelManager.DisplayControl(tv1, "TreeView");