using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject owner;

    private float speed;

    private float distanceTravelled;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed);
        distanceTravelled += speed;

        if (distanceTravelled > 10f) {
            Destroy(gameObject);
        }
	}

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
}
