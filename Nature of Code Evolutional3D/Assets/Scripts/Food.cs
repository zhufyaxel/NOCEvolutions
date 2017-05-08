using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]

public class Food : MonoBehaviour {
    public float worldRange = 50;
    public Vector3 pos;
     public float Range;   
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }

    public void init()
    {
        pos = new Vector3(Random.Range(-worldRange / 2, worldRange / 2), Random.Range(0.0f, worldRange), Random.Range(-worldRange / 2, worldRange / 2));
        transform.position = pos;
    }



}
