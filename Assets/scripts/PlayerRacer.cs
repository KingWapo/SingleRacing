using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRacer : Racer {

    public new Camera camera;

    public GameObject healthBar;

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

        if (Input.GetKeyUp(KeyCode.Keypad4)) {
            Debug.Log("DEBUG - TAKE DAMAGE");
            GetComponent<RacerInfo>().TakeDamage(.1f);
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("360_ButtonA")) {
            Shoot();
        }

        UpdateHealthBar();
	}

    protected override void DoMovement() {
        float turnAxis = Input.GetAxis("Horizontal");
        float acclAxis = Input.GetAxis("Vertical");

        turnAxis = Mathf.Abs(turnAxis) > .1f ? turnAxis * .5f : Input.GetAxis("360_LeftThumbstick");
        acclAxis = Mathf.Abs(acclAxis) > .1f ? acclAxis : Input.GetAxis("360_Triggers");

        UpdateMovement(turnAxis, acclAxis);

        camera.transform.localRotation = Quaternion.Euler(new Vector3(15f, 0f, -GetLean()));
    }

    protected override float RacerVelocity() {
        return velocity;
    }

    protected override void FinishRace() {
        base.FinishRace();

        gameObject.AddComponent<AIRacer>();
        enabled = false;
    }

    protected void UpdateHealthBar() {
        healthBar.transform.localScale = new Vector3(6f * racerInfo.GetHealth(), .2f, 1f);

        float halfHealth = RacerInfo.maxHealth / 2f;
        float redMod = 0;
        float greenMod = 0;

        if (racerInfo.GetHealth() < .5f) {
            redMod = 1f;
            greenMod = Mathf.Clamp(racerInfo.GetHealth() / halfHealth, 0, 1f);
        } else {
            redMod = 1f - Mathf.Clamp((racerInfo.GetHealth() - halfHealth) / halfHealth, 0, 1f);
            greenMod = 1f;
        }

        healthBar.GetComponent<Image>().color = new Color(redMod, greenMod, 0);
    }
}
