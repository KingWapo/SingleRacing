using UnityEngine;
using System.Collections;

public class SpeedPad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Racer") {
            other.GetComponent<Racer>().StartSpeedBoost();
            /*
            AIRacer aiRacer = other.GetComponent<AIRacer>();
            PlayerRacer pcRacer = other.GetComponent<PlayerRacer>();

            if (aiRacer) {
                aiRacer.StartSpeedBoost();
            } else if (pcRacer) {
                pcRacer.StartSpeedBoost();
            }*/
        }
    }
}
