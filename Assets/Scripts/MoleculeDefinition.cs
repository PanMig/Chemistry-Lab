using System.Collections;
using System.Collections.Generic;

public static class MoleCuleDefinition
{

    static Molecule H20 = new Molecule("water", "H2O");
    static Molecule CH4 = new Molecule("methane", "CH4");
    static Molecule HCl = new Molecule("Hydrogen Chloride", "HCl");
    static Molecule NaCl = new Molecule("Sodium Chloride", "NaCl");
    static Molecule CH4O = new Molecule("Methanole", "CH4O");
    static Molecule C2H6O = new Molecule("Ethanol", "C2H6O");
    static Molecule C3H6O = new Molecule("Acetone", "C3H6O");
    static Molecule CH5N = new Molecule("Isocyanic acid", "CH5N");
    static Molecule CH2N2 = new Molecule("Cyanamide", "CH2N2");
    static Molecule C2H2O = new Molecule("Ethenone", "C2H2O");
    public static Stack<Molecule> _strategy_stack = new Stack<Molecule>();

    public static string[] standard_strategy = ["H20", "CH4", "HCl", "NaCl", "CH4O", "C2H6O", "C3H6O", "CH5N", "CH2N2", "C2H2O"];

    public static Molecule getMolecule(string forumla)
    {
        switch (forumla)
        {
            case "H20": return H20;
            case "CH4": return CH4;
            case "HCl": return HCl;
            case "NaCl": return NaCl;
            case "CH4O": return CH4O;
            case "C2H6O": return C2H6O;
            case "C3H6O": return C3H6O;
            case "CH5N": return CH5N;
            case "CH2N2": return CH2N2;
            case "C2H2O": return C2H2O;
            default: return H20;

        }
    }

    public static void buildStrategyStack(string[] strategy)
    {
    }


    public static Molecule getActiveMolecule()
    {
        return _strategy_stack.Peek();
    }

    public static  Molecule nextMolecule(){
     
        return _strategy_stack.Peek();

    }



}

