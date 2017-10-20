using UnityEngine;


public class ElementsCreator : MonoBehaviour {

    //elements
    [SerializeField] private GameObject hydrogen;
    [SerializeField] private GameObject oxygen;
    [SerializeField] private GameObject carbon;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private SoundManager SoundManager;

    public void GenerateElement(int element)
    {
        if(element == 1)
        {
            Instantiate(hydrogen, targetPoint.position, targetPoint.rotation);
        }
        else if (element == 2)
        {
            Instantiate(oxygen, targetPoint.position, targetPoint.rotation);
        }
        else if (element == 3)
        {
            Instantiate(carbon, targetPoint.position, targetPoint.rotation);
        }
        else
        {
            Debug.Log("uninitialized element choice");
        }
        SoundManager.PlaySoundOnce(SoundManager.audioClips[2]);
    }

}
