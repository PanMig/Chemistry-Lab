using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomsRandomSpawner : MonoBehaviour {

    private GameObject molecule;
    private GameObject[] Atoms;
    private Vector2 random2DCords;

    // Use this for initialization
    void Start ()
    {
        molecule = GameObject.FindGameObjectWithTag("Molecule");
        Atoms = GameObject.FindGameObjectsWithTag("Atom");
        SpawnAtomsToRandomPosition();
	}

    public void SpawnAtomsToRandomPosition()
    {
        for (int i = 0; i < Atoms.Length; i++)
        {
            random2DCords = new Vector2 (Random.Range(-5.0f,5.0f), Random.Range(-5.0f, 5.0f));
            Atoms[i].transform.position = new Vector3 (random2DCords.x, random2DCords.y, 0);
        }
    }
	
}
