using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using goedle_sdk;

public class MoleculeSpawner : MonoBehaviour
{

    public List<GameObject> Molecules = new List<GameObject>();
    public Transform position;
    public GameObject molecule;

    public void OnEnable()
    {
        ContentAdaptationManager.NextMolecule += DestroyMolecule;
        ContentAdaptationManager.NextMolecule += DestroyUnusedElements;
        ContentAdaptationManager.NextMolecule += SpawnMolecule;
        EmptyParentMolecule.MolConstructed += DestroyUnusedElements;
    }

    private void Start()
    {
        SpawnMolecule();
    }

    public void OnDisable()
    {
        ContentAdaptationManager.NextMolecule -= SpawnMolecule;
        EmptyParentMolecule.MolConstructed -= DestroyUnusedElements;
        ContentAdaptationManager.NextMolecule -= DestroyUnusedElements;
        ContentAdaptationManager.NextMolecule -= DestroyMolecule;
    }

    public void SpawnMolecule()
    {
        foreach (GameObject mol in Molecules)
        {
            if (mol.name == GameManager.chosenMolecule.Name)
            {
                molecule = Instantiate(mol, position.position,Quaternion.identity);
            }
        }
    }

    public void DestroyUnusedElements()
    {
        var elements = FindObjectsOfType<MouseTranslate>();
        if (elements.Length != 0)
        {
            GoedleAnalytics.instance.track("clear.elements", GameManager.chosenMolecule.Name, elements.Length.ToString());

            for (int i = 0; i < elements.Length; i++)
            {
                Destroy(elements[i].gameObject);
            }
        }
    }

    public void DestroyMolecule()
    {
        if (molecule != null)
        {
            Destroy(molecule);
        }
    }
}
