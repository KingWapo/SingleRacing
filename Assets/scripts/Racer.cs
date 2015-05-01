using UnityEngine;
using System.Collections;

public class Racer : MonoBehaviour {

    public float speed = 30f;

    private float rotation = 300f;
    private float lean = 0f;
    private float maxLean = 12f;

    private float velocity;
    private float maxForwardVelocity = 50f;
    private float maxReverseVelocity = -4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected void UpdateMovement(float turnAxis, float acclAxis) {
        if (Mathf.Abs(turnAxis) > .1f && Mathf.Abs(acclAxis) > .1f) {
            lean = Mathf.Clamp(lean - Mathf.Sign(turnAxis), -maxLean, maxLean);
        } else {
            if (lean > 0f) {
                lean -= 1f;
            } else if (lean < 0f) {
                lean += 1f;
            }
        }

        if (Mathf.Abs(acclAxis) > .1f) {
            if (turnAxis != 0) {
                transform.Rotate(0, turnAxis * Time.deltaTime * rotation * Mathf.Sign(velocity), 0);
            }

            velocity = Mathf.Clamp(velocity + .5f * acclAxis, maxReverseVelocity, maxForwardVelocity);
        } else {
            velocity *= .9f;
            if (Mathf.Abs(velocity) <= .0001f) {
                velocity = 0f;
            }
        }

        transform.Rotate(0, 0, lean);
        transform.Translate(0, 0, Time.deltaTime * velocity);
    }

    protected float GetLean() {
        return lean;
    }
}
