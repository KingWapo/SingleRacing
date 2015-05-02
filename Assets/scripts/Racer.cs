using UnityEngine;
using System.Collections;

public abstract class Racer : MonoBehaviour {

    // racer movement
    public float speed = 30f;
    public float acceleration = 100f;

    private float rotation = 300f;
    private float lean = 0f;
    private float maxLean = 12f;

    private float velocity;
    private float maxForwardVelocity = 50f;
    private float maxReverseVelocity = -4f;

    // racer shooting
    public GameObject projectile;
    public GameObject leftGun;
    public GameObject rightGun;

    private int barrelIndex = 0;

    private float spreadAngle = 1f;

    private float cooldown = 0f;
    private float maxCooldown = 1f;

    private int spreadLevel;
    private int minSpreadLevel = 0;
    private int maxSpreadLevel = 5;
    private int fireRateLevel;
    private int minFireRateLevel = 0;
    private int maxFireRateLevel = 3;

	// Use this for initialization
	protected virtual void Start () {
        spreadLevel = minSpreadLevel;
        fireRateLevel = minFireRateLevel;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (GameManager.GetState() == GameManager.State.Racing) {
            DoMovement();
        }

        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Keypad2)) {
            IncreaseSpreadLevel();
            Debug.Log("DEBUG - INCREASING SPREAD: " + spreadLevel);
        }

        if (Input.GetKeyUp(KeyCode.Keypad3)) {
            IncreaseFireRateLevel();
            Debug.Log("DEBUG - INCREASING FIRE RATE");
        }
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

    protected void Shoot() {
        if (cooldown <= 0) {
            Vector3 origin;

            if (barrelIndex == 0) {
                origin = leftGun.transform.position;
            } else {
                origin = rightGun.transform.position;
            }

            switch (spreadLevel) {
                case 5:
                    GameObject proj7 = ProduceProjectile(origin);
                    proj7.transform.Rotate(transform.up, spreadAngle);
                    proj7.transform.Rotate(transform.forward, -spreadAngle);
                    GameObject proj8 = ProduceProjectile(origin);
                    proj8.transform.Rotate(transform.up, -spreadAngle);
                    proj8.transform.Rotate(transform.forward, spreadAngle);
                    goto case 4;
                case 4:
                    GameObject proj5 = ProduceProjectile(origin);
                    proj5.transform.Rotate(transform.up, spreadAngle);
                    proj5.transform.Rotate(transform.forward, spreadAngle);
                    GameObject proj6 = ProduceProjectile(origin);
                    proj6.transform.Rotate(transform.up, -spreadAngle);
                    proj6.transform.Rotate(transform.forward, -spreadAngle);
                    goto case 3;
                case 3:
                    GameObject proj3 = ProduceProjectile(origin);
                    proj3.transform.Rotate(transform.forward, spreadAngle);
                    GameObject proj4 = ProduceProjectile(origin);
                    proj4.transform.Rotate(transform.forward, -spreadAngle);
                    goto case 2;
                case 2:
                    GameObject proj1 = ProduceProjectile(origin);
                    proj1.transform.Rotate(transform.up, spreadAngle);
                    GameObject proj2 = ProduceProjectile(origin);
                    proj2.transform.Rotate(transform.up, -spreadAngle);
                    goto case 1;
                case 1:
                    ProduceProjectile(origin);
                    break;
            }

            cooldown = maxCooldown - ((maxCooldown / (maxFireRateLevel * 2)) * fireRateLevel);
            Debug.Log("cooldown: " + cooldown);
            barrelIndex = (barrelIndex + 1) % 2;
        }
    }

    protected GameObject ProduceProjectile(Vector3 origin) {
        GameObject shot = (GameObject)Instantiate(projectile);
        shot.transform.position = origin;
        shot.transform.LookAt(origin - new Vector3(5f, 0, 0));
        shot.GetComponent<Projectile>().SetSpeed(1f);
        shot.GetComponent<Projectile>().owner = gameObject;

        return shot;
    }

    public void IncreaseSpreadLevel() {
        spreadLevel++;
        spreadLevel = Mathf.Clamp(spreadLevel, minSpreadLevel, maxSpreadLevel);
    }

    public void IncreaseFireRateLevel() {
        fireRateLevel++;
        fireRateLevel = Mathf.Clamp(fireRateLevel, minFireRateLevel, maxFireRateLevel);
    }

    protected virtual void FinishRace() {
        // do stuff
    }

    protected abstract void DoMovement();
}
