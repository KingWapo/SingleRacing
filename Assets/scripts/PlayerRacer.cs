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

        if (Input.GetKeyUp(KeyCode.Keypad1)) {
            Debug.Log("DEBUG - FINISHING RACE");
            FinishRace();
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("360_ButtonA")) {
            Shoot();
        }
	}

    protected override void DoMovement() {
        float turnAxis = Input.GetAxis("Horizontal");
        float acclAxis = Input.GetAxis("Vertical");

        turnAxis = Mathf.Abs(turnAxis) > .1f ? turnAxis : Input.GetAxis("360_LeftThumbstick");
        acclAxis = Mathf.Abs(acclAxis) > .1f ? acclAxis : Input.GetAxis("360_Triggers");

        UpdateMovement(turnAxis, acclAxis);

        camera.transform.localRotation = Quaternion.Euler(new Vector3(15f, 0f, -GetLean()));
    }

    protected override void FinishRace() {
        base.FinishRace();

        gameObject.AddComponent<AIRacer>();
        enabled = false;
    }
}
