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
        Debug.Log("OnDisable");
        EnterButton.ButtonClicked -= SpawnMolecule;
        EmptyParentMolecule.MolConstructed -= DestroyUnusedElements;
        ExitButton.ButtonClicked -= DestroyUnusedElements;
        ExitButton.ButtonClicked -= DestroyMolecule;
    }

    public void SpawnMolecule()
    {
        Debug.Log("SpawnMolecule");

        foreach (GameObject mol in Molecules)
        {
            Debug.Log("SpawnMolecule in loop");

            if(mol.name == GameManager.chosenMolecule.Name)
            {
                Debug.Log("SpawnMolecule in if");

                molecule = Instantiate(mol, position.position, Quaternion.identity);
            }
        }
    }

    public void DestroyUnusedElements()
    {
        var elements = FindObjectsOfType<MouseTranslate>();
        Debug.Log("DestroyUnusedElements");

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
        Debug.Log("DestroyMolecule");

        if(molecule != null)
        {
            Destroy (molecule);
        }
    }
}
