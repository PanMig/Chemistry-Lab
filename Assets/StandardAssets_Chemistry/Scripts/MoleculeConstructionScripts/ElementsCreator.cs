using UnityEngine;
using goedle_sdk;

public class ElementsCreator : MonoBehaviour {

    //elements
    [SerializeField] private GameObject element;
    public Vector3 pos;
    public Transform position;
    public float leftBorder;
    public float rightBorder;
    public AudioClip clip;

    public void Start()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -dist)).x;
    }

    public void SpawnElement()
    {
        Mathf.Clamp(position.position.x, leftBorder, rightBorder);
        Instantiate(element,position.position, Quaternion.identity);
        GoedleAnalytics.instance.track("spawn.element", element.name);
        SoundManager.instance.PlaySingle(clip);
    }

}
