using UnityEngine;

public class EmptyParentMolecule : MonoBehaviour
{

    [SerializeField] private MoleculeCollision[] placedElements;
    private int placedElementsCount = 0;
    int i;
    private Rotation3D rotator;


    private void Start()
    {
        rotator = gameObject.GetComponent<Rotation3D>();
        placedElements = GetComponentsInChildren<MoleculeCollision>();
    }

    private void Update()
    {
        if (GameManager.instance.chosenMolecule != null && GameManager.instance.currentStage == GameManager.Stage.stage3)
        {
            if (placedElementsCount != placedElements.Length)
            {
                for (i = 0; i < placedElements.Length; i++)
                {
                    if (placedElements[i].placed == true && placedElements[i].loopCheck == false)
                    {
                        placedElementsCount++;
                        placedElements[i].loopCheck = true;
                    }
                }
            }
            else if (placedElementsCount == placedElements.Length)
            {
                rotator.YawRotation();
                GameManager.instance.DestroyUnusedAtoms();
                GameManager.instance.chosenMolecule.SetConstruction(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GameManager.instance.ChangeToStage(4);
                    Destroy(gameObject);
                }
            }
        }
        //rotation
        rotator.RotateWithMouse();
    }
}
