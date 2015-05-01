using UnityEngine;
using System.Collections;

public class RacerInfo : MonoBehaviour {

    // racer position
    public int wayPointsHit = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Waypoint") {
            wayPointsHit++;
        }
    }
}
