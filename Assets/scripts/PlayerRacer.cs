using UnityEngine;
using System.Collections;

public class PlayerRacer : Racer {

    Camera camera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        DoMovement();
	}

    protected void DoMovement() {
        float turnAxis = Input.GetAxis("360_LeftThumbstick");
        float acclAxis = Input.GetAxis("360_Triggers");

        UpdateMovement(turnAxis, acclAxis);

        camera.transform.localRotation = Quaternion.Euler(new Vector3(15f, 0f, -GetLean()));
    }
}
