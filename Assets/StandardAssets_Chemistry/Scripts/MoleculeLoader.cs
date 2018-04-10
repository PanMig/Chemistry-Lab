using UnityEngine;

public class MoleculeLoader : MonoBehaviour
{

    public GameObject moleculeManager; //GameManager prefab to instantiate.

    void Awake()
    {
        if (MoleculeManager.instance == null)
            //Instantiate gameManager prefab
            Instantiate(moleculeManager);
    }
}