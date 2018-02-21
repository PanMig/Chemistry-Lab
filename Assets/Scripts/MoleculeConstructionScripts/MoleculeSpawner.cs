using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeSpawner : MonoBehaviour {

    public List<GameObject> Molecules = new List<GameObject>();
    public Transform position;
    public GameObject molecule;

    public void Start()
    {
        EnterButton.ButtonClicked += SpawnMolecule;
        ExitButton.ButtonClicked += DestroyMolecule;
        ExitButton.ButtonClicked += DestroyUnusedElements;
        EmptyParentMolecule.MolConstructed += DestroyUnusedElements;
    }

    public void OnDisable()
    {
        EnterButton.ButtonClicked -= SpawnMolecule;
        EmptyParentMolecule.MolConstructed -= DestroyUnusedElements;
        ExitButton.ButtonClicked -= DestroyUnusedElements;
        ExitButton.ButtonClicked -= DestroyMolecule;
    }

    public void SpawnMolecule()
    {
        foreach (GameObject mol in Molecules)
        {
            if(mol.name == GameManager.chosenMolecule.Name)
            {
                molecule = Instantiate(mol, position.position, Quaternion.identity);
            }
        }
    }

    public void DestroyUnusedElements()
    {
        var elements = FindObjectsOfType<MouseTranslate>();

        if (elements.Length != 0)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                Destroy(elements[i].gameObject);
            }
        }
    }

    public void DestroyMolecule()
    {
        if(molecule != null)
        {
            Destroy (molecule);
        }
    }
}
