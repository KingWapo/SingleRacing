using UnityEngine;
using System.Collections;

public class PlayerRacer : Racer {

    public new Camera camera;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void DoMovement() {
        float turnAxis = Input.GetAxis("360_LeftThumbstick");
        float acclAxis = Input.GetAxis("360_Triggers");

        UpdateMovement(turnAxis, acclAxis);

        camera.transform.localRotation = Quaternion.Euler(new Vector3(15f, 0f, -GetLean()));
    }
}
