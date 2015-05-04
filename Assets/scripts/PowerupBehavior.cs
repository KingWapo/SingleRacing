using UnityEngine;
using System.Collections;

public enum PowerupType { Spreadshot, FireRate, Speed, Max }

public class PowerupBehavior : MonoBehaviour {

    public Collider col;

    private int lifetime;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 1, 0);

        if (lifetime <= 0)
        {
            col.enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            lifetime--;
        }
	}


    void OnTriggerEnter(Collider other)
    {
        print("Triggered");
        if (other.tag == "Racer")
        {
            lifetime = 100;
            col.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
