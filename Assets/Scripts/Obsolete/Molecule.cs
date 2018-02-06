using System;
using System.Collections.Generic;
using UnityEngine;

public class Molecule
{

    private string name;
    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    private string formula;

    private bool moleculeNamed;
    public bool MoleculeNamed
    {
        get
        {
            return moleculeNamed;
        }

        set
        {
            moleculeNamed = value;
        }
    }

    #region CONSTRUCTORS

    public Molecule(string name, string formula)
    {
        this.formula = formula;
        this.name = name;
    }



    #endregion

    public List<string> GetFormula()
    {
        //return formula;
        List<string> list = new List<string>();
        for (int i = 0; i < formula.Length; i++) // in string always use lesser than the lenght otherwise out of range exception is given.
        {
            if (char.IsUpper(formula[i]) || char.IsNumber(formula[i]))
            {
                if (i + 1 < formula.Length)
                {
                    if (char.IsLower(formula[i + 1]))
                    {
                        list.Add(string.Concat(formula[i], formula[i + 1]));
                        Debug.Log(string.Concat(formula[i], formula[i + 1]));
                    }
                    else
                    {
                        Debug.Log(formula[i]);
                        list.Add(formula[i].ToString());
                    }
                }
                else
                {
                    Debug.Log(formula[i]);
                    list.Add(formula[i].ToString());
                }
            }
        }
        Debug.Log(list.Count);
        return list;
    }


    public int GetFormulaLength()
    {
        int length = 0;
        //return formula.Length;
        foreach (char c in formula)
        {
            if (Char.IsUpper(c))
            {
                length++;
            }
            else if (Char.IsNumber(c))
            {
                length++;
            }
        }
        return length;
    }

}
