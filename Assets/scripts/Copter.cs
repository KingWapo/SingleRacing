using UnityEngine;
using System.Collections;

public class Copter : MonoBehaviour {

    public GameObject[] rotors;

    private float theta;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < rotors.Length; i++) {
            rotors[i].transform.rotation = Quaternion.Euler(0, theta + 15f, 0);
        }

        theta = (theta + 20f) % 360f;
	}
}
