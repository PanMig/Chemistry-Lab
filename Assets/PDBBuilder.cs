using System.Collections.Generic;
using UnityEngine;

public class PDBBuilder : MonoBehaviour {

    public string filename;

    List<PDBParser.Atom> atoms = new List<PDBParser.Atom>();
    List<int[]> bonds = new List<int[]>();
    PDBParser parser = new PDBParser();

    //default rotation
    Quaternion rot;

    //atom gameobject prefab
    public GameObject atomPrefab;
    public GameObject bondPrefab;

    private MaterialPropertyBlock _propBlock;

    //parent gameobject
    private GameObject molecule;


    // Use this for initialization
    void Awake () {
        _propBlock = new MaterialPropertyBlock();
        rot = new Quaternion(0, 0, 0, 0);
        CreateMoleculeParent();
        BuildMoleculeFromPDB(filename);
    }

    public void BuildMoleculeFromPDB(string filename)
    {
        parser.ReadFile(filename);
        atoms = parser.GetAtoms();
        bonds = parser.GetBonds();

        // build atoms
        for (int i = 0; i <atoms.Count; i++)
        {
            Vector3 pos = new Vector3(atoms[i].x, atoms[i].y, atoms[i].z);
            Color molColor = new Color32((byte)atoms[i].colorPackage[0], (byte)atoms[i].colorPackage[1], (byte)atoms[i].colorPackage[2],255);

            GameObject atomGameObject = Instantiate(atomPrefab, pos, rot,molecule.transform);

            //apply color
            atomGameObject.GetComponent<Renderer>().GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_Color", molColor);
            atomGameObject.GetComponent<Renderer>().SetPropertyBlock(_propBlock);
        }

        //build bonds
        for (int i = 0; i < bonds.Count; i++)
        {
            int atom_1 = bonds[i][0];
            int atom_2 = bonds[i][1];

            if (atom_1 > 0 && atom_2 > 0)
            {
                Vector3 startPoint = new Vector3(atoms[atom_1 - 1].x, atoms[atom_1 - 1].y, atoms[atom_1 - 1].z);
                Vector3 endPoint = new Vector3(atoms[atom_2 - 1].x, atoms[atom_2 - 1].y, atoms[atom_2 - 1].z);
                CreateBondBetweenAtoms(startPoint, endPoint, 0.4f);
            }
        }
    }



    void CreateBondBetweenAtoms(Vector3 start, Vector3 end,float width)
    {
        var offset = end - start;
        var scale = new Vector3(width, offset.magnitude / 2.0f, width);
        var position = start + (offset / 2.0f);

        GameObject bondGameObject = Instantiate(bondPrefab, position, Quaternion.identity,molecule.transform);
 
        bondGameObject.transform.up = offset;
        bondGameObject.transform.localScale = scale;
    }

    void CreateMoleculeParent()
    {
         molecule = new GameObject
        {
            name = "molecule"
        };
        molecule.tag = "Molecule";
    }

}
