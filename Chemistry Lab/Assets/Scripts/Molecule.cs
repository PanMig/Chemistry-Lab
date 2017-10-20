using UnityEngine;

public class Molecule
{

    public enum Molecules { methanole, ethanol, water,propanone}
    public enum HomogenousTeam {alchohol,various,ketone}

    private Molecules name;
    private Color color;
    private string formula;
    private bool isConstructed;
    private HomogenousTeam homogenousTeam;

    public Molecule(Molecules name, Color color, string formula,bool isConstructed,HomogenousTeam homogenousTeam)
    {
        this.name = name;
        this.color = color;
        this.formula = formula;
        this.isConstructed = isConstructed;
        this.homogenousTeam = homogenousTeam;
    }

    public bool checkFormula(string formula)
    {
        if (string.Equals(this.formula, formula) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Molecules GetName()
    {
        return name;
    }

    public string getFormula()
    {
        return formula;
    }

    public void SetConstruction(bool construction)
    {
        isConstructed = construction;
    }

    public bool GetConstruction()
    {
        return isConstructed;
    }

    public HomogenousTeam GetHomogenousTeam()
    {
        return homogenousTeam;
    }

}
