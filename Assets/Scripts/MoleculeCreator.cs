using UnityEngine;


public class MoleculeCreator : MonoBehaviour {

    [SerializeField] private GameObject methanole;
    [SerializeField] private GameObject aithanole;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject propanone;
    private GameObject moleculeModel;


    private bool moleculeCreated = false;
    [SerializeField] private Transform targetPoint;

    private void Update()
    {
        if (moleculeCreated == false && GameManager.instance.currentStage == GameManager.Stage.stage3)
        {
            moleculeModel = CreateMoleculeModel();
            moleculeCreated = true;
            moleculeModel.SetActive(true);
        }
        if(GameManager.instance.currentStage == GameManager.Stage.stage0)
        {
            moleculeCreated = false;
        }
    }

    public GameObject CreateMoleculeModel()
    {
        if (GameManager.instance.chosenMolecule.GetName() == Molecule.Molecules.ethanol)
        {
            return Instantiate(aithanole, targetPoint.position, targetPoint.rotation);
        }
        else if(GameManager.instance.chosenMolecule.GetName() == Molecule.Molecules.methanole)
        {
            return Instantiate(methanole, targetPoint.position, targetPoint.rotation);
        }
        else if(GameManager.instance.chosenMolecule.GetName() == Molecule.Molecules.propanone)
        {
            return Instantiate(propanone, targetPoint.position, targetPoint.rotation);
        }
        else
        {
            return Instantiate(water, targetPoint.position, targetPoint.rotation);
        }
    }

}
