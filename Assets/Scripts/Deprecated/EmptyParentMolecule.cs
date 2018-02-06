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
        if (GameManager.chosenMolecule != null)
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
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Destroy(gameObject);
                }
            }
        }
        //rotation
        rotator.RotateWithMouse();
    }
}
