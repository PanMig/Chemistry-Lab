using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PDBParser
{

    private string text;  //the string that the pd file will be assigned.
    private List<Atom> Atoms = new List<Atom>();  // a list that holds all the atoms from the pdb file.
    private List<int[]> Bonds = new List<int[]>(); // a list with all the bond pairs from the pdb file.

    // class for the atom object.
    public class Atom
    {
        public float x, y, z; //coordinates in the world.
        public int[] colorPackage; // color RGB values.

        //constructor
        public Atom(float x, float y, float z, int[] clrPk) { this.x = x; this.y = y; this.z = z; this.colorPackage = clrPk; }
    }

    //Reads the whole file and saves it to a string.
    public void ReadFile(string filename)
    {
        try
        {
            text = File.ReadAllText(filename);
            try
            {
                ParseText();
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.Log("string length error");
                return;
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("File not found");
        }
    }

    //Parses the PDB formated string that contains the whole text.
    private void ParseText()
    {
        //fileds declaration
        string clr;
        float x, y, z;


        //split the text to lines
        string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        

        foreach (string line in lines)
        {

            if (line.Substring(0, 4) == "ATOM" || line.Substring(0, 6) == "HETATM")
            {
                // parse the x,y,z cordinates of the atom or hematm
                x = float.Parse(line.Substring(30, 7));
                y = float.Parse(line.Substring(38, 7));
                z = float.Parse(line.Substring(46, 7));
               
                // gets the name of the atom or hetatm to lower case so to much it with the color package. (for example "c" for C , which matches to carbon atom).
                clr = Trim(line.Substring(12, 2)).ToLower();

                Atom atom = new Atom(x, y, z, colorTable[clr]);
                //add atributes to list
                Atoms.Add(atom);

            }
            else if (line.Substring(0, 6) == "CONECT")
            {
                int satom = int.Parse(line.Substring(6, 5));
                if (line.Length >= 16) ParseBond(11, 5, satom, line);
                if (line.Length >= 21) ParseBond(16, 5, satom, line);
                if (line.Length >= 26) ParseBond(21, 5, satom, line);
                if (line.Length >= 31) ParseBond(26, 5, satom, line);
            }
        }
    }


    #region parsing function

    private string Trim(string text)
    {
        if (text.Length > 1) return text.Trim();
        return text;
        //return text.Replace("/^\r\n\r\n*/", "").Replace("/\r\n\r\n*$/", "");

    }

    private string Hash(int s, int e)
    {
        return "s" + Math.Min(s, e) + "e" + Math.Max(s, e);
    }

    private void ParseBond(int start, int length, int satom, string line)
    {
        int eatom = int.Parse(line.Substring(start, length));
        int[] bond = new int[] { satom, eatom, 1 };
        //Debug.Log(bond[0].ToString() + "|" + bond[1].ToString());
        Bonds.Add(bond);
    }
    # endregion   

    public List<Atom> GetAtoms()
    {
        if (Atoms.Count > 0) return Atoms;
        Debug.LogError("Atoms list is empty");
        return null;
    }

    public List<int[]> GetBonds()
    {
        if (Bonds.Count > 0) return Bonds;
        Debug.LogError("Bonds list is empty");
        return null;
    }

    //a dictionary that holds a key for the atoms name, and a value with the RGB properties.
    Dictionary<string, int[]> colorTable = new Dictionary<string, int[]>()
    {
        { "h", new int[] { 255, 255, 255 }},{ "c", new int[] { 144, 144, 144 }},{ "o", new int[] { 255, 13, 13 }},{"al", new int[] {191,166,166}},
        {"cl", new int[] {31,240,31}}, {"n",  new int[] {48, 80, 248}}, {"na", new int[] {171, 92, 242}}, {"he", new int[] {217, 255, 255 }},
        {"li", new int[] {204,128,255}}, {"be", new int[] {194,255,0 }}, {"b",  new int[] {255,181,181}}, {"f", new int[] {144, 224, 80 }},
        {"ne", new int[] {179, 227, 245}},{"mg", new int[] {138,255,0}},{"si",new int[] {240, 200, 160}},
        {"p",new int[]{255,128,0}}, {"s", new int[]{255,255,48 }},
    };


}
