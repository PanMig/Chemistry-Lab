using UnityEngine;
using System.Collections;

public class CamRotat : MonoBehaviour 
{
    public float Speed = 7.5f;
    public Transform TargetPos;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.RotateAround(TargetPos.position, Vector3.up, Speed * Time.deltaTime);
	}
}
