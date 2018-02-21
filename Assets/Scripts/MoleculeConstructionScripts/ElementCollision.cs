using UnityEngine;

public class ElementCollision : MonoBehaviour {

    private Renderer rend;
    public bool placed;
    public bool loopCheck = false; // used only for the loop check
    private Color elementColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        placed = false;
        elementColor = rend.material.color;

        //make grey semi trasparent color
        Color transpColor = Color.white;
        transpColor.a = 0.2f;
        rend.material.color = transpColor;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!placed && collider.gameObject.tag == this.tag)
        {
            Destroy(collider.gameObject);
            Color color = elementColor;
            color.a = 1;
            rend.material.color = color;
            placed = true;
            SoundManager.instance.PlaySingle(SoundManager.instance.elementCollision);
        }
    }

}
