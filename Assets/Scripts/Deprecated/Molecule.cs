
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

    public string GetFormula()
    {
        return formula;
    }

    public int GetFormulaLength()
    {
        return formula.Length;
    }

}
