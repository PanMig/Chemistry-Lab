using UnityEngine;

public class EmptyParentMolecule : MonoBehaviour
{

    [SerializeField] private ElementCollision[] placedElements;
    private bool molConstructed = false;
    private int placedElementsCount = 0;
    int i;

    private Rotation3D rotator;

    public delegate void MoleculeConstructed();
    public static event MoleculeConstructed MolConstructed;

    private void Start()
    {
        rotator = gameObject.GetComponent<Rotation3D>();
        placedElements = GetComponentsInChildren<ElementCollision>();
        molConstructed = false;
    }

    private void Update()
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
            if(molConstructed == false)
            {
                molConstructed = true;
                MolConstructed();
                rotator.ResetTransformation();
                SoundManager.instance.PlaySingle(SoundManager.instance.elementConstructed);
            }
            rotator.YawRotation(35.0f);
        }
    }

}
