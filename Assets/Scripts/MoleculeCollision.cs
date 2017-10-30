using UnityEngine;

public class MoleculeCollision : MonoBehaviour {

    private Renderer rend;
    public bool placed;
    public bool loopCheck = false; // used only for the loop check

    private void Start()
    {
        rend = GetComponent<Renderer>();
        placed = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!placed && collider.gameObject.tag == this.tag)
        {
            Destroy(collider.gameObject);
            Color transpColor = rend.material.color;
            transpColor.a = 1;
            rend.material.color = transpColor;
            placed = true;
        }
    }

}
