using UnityEngine;


public class ElementsCreator : MonoBehaviour {

    //elements
    [SerializeField] private GameObject hydrogen;
    [SerializeField] private GameObject oxygen;
    [SerializeField] private GameObject carbon;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private SoundManager SoundManager;
    Vector3 tempVectorHydrogen;
    Vector3 tempVectorOxygen;
    Vector3 tempVectorCarbon;

    private void Start()
    {
        tempVectorHydrogen = new Vector3(0, 0, 0);
        tempVectorOxygen = new Vector3(0.4f, 0, 0);
        tempVectorCarbon = new Vector3(1, 0, 0);
    }

    public void GenerateElement(int element)
    {
        if(element == 1)
        {
            Instantiate(hydrogen, targetPoint.position + tempVectorHydrogen, targetPoint.rotation);
        }
        else if (element == 2)
        {
            Instantiate(oxygen, targetPoint.position + tempVectorOxygen , targetPoint.rotation);
        }
        else if (element == 3)
        {
            Instantiate(carbon, targetPoint.position + tempVectorCarbon, targetPoint.rotation);
        }
        else
        {
            Debug.Log("uninitialized element choice");
        }
    }

}
