using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeSpawner : MonoBehaviour {

    [SerializeField] private GameObject molecule;
    public Transform position;

	// Use this for initialization
	void Start () {
        molecule = GameObject.Find("");
        Instantiate(molecule,gameObject.transform.position,Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		//molecule.transform.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
}
