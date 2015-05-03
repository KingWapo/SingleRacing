using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int ownerID;

    private float speed;

    private float timeRemaining = 1f;

	// Use this for initialization
    void Start() {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed);

        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0) {
            Destroy(gameObject);
        }
	}

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
}
