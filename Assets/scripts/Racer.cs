using UnityEngine;
using System.Collections;

public abstract class Racer : MonoBehaviour {

    // racer movement
    public float acceleration = 100f;

    private float rotation = 300f;
    private float lean = 0f;
    public static float maxLean = 12f;

    protected float velocity;
    public static float maxForwardVelocity = 50f;
    public static float maxReverseVelocity = -4f;

    protected float speedBoost = 0f;
    public static float maxBoost = 30f;
    public static float boostDuration = 1f;

    public int racerIndex;

    // racer shooting
    public GameObject projectile;
    public GameObject leftGun;
    public GameObject rightGun;

    private int barrelIndex = 0;

    private float spreadAngle = 1f;

    private float cooldown = 0f;
    private float maxCooldown = 1f;

    public bool weaponsEnabled = false;

    // racer info
    protected RacerInfo racerInfo;

	// Use this for initialization
    protected virtual void Start() {
        racerInfo = GetComponent<RacerInfo>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (RaceManager.GetState() == RaceManager.State.Racing && racerInfo.CanMove()) {
            DoMovement();
        }

        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }

        if (speedBoost > 0) {
            velocity = maxForwardVelocity + speedBoost;
            speedBoost -= (maxBoost / boostDuration) * Time.deltaTime;
        } else if (speedBoost < 0) {
            speedBoost = 0;
        }
	}

    protected void UpdateMovement(float turnAxis, float acclAxis) {
        if (Mathf.Abs(turnAxis) > .1f && Mathf.Abs(velocity + speedBoost) > .1f) {
            lean = Mathf.Clamp(lean - Mathf.Sign(turnAxis), -maxLean, maxLean);
        } else {
            if (lean > 0f) {
                lean -= 1f;
            } else if (lean < 0f) {
                lean += 1f;
            }
        }

        if (Mathf.Abs(velocity + speedBoost) > .1f) {
            if (turnAxis != 0) {
                transform.Rotate(0, turnAxis * Time.deltaTime * rotation * Mathf.Sign(velocity), 0);
            }
        }

        if (Mathf.Abs(acclAxis) > .1f) {
            velocity += .5f * acclAxis;

            velocity = Mathf.Clamp(velocity, maxReverseVelocity, maxForwardVelocity + speedBoost);
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
        if (weaponsEnabled && cooldown <= 0 && racerInfo.CanMove()) {
            Vector3 origin;

            if (barrelIndex == 0) {
                origin = leftGun.transform.position;
            } else {
                origin = rightGun.transform.position;
            }

            switch (racerInfo.GetSpreadLevel()) {
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

            cooldown = maxCooldown - ((maxCooldown / (RacerInfo.maxFireRateLevel * 2)) * racerInfo.GetFireRateLevel());
            barrelIndex = (barrelIndex + 1) % 2;
        }
    }

    protected GameObject ProduceProjectile(Vector3 origin) {
        GameObject shot = (GameObject)Instantiate(projectile);
        shot.transform.position = origin;
        shot.transform.LookAt(origin + transform.forward * 5);
        shot.GetComponent<Projectile>().SetSpeed(1f + RacerVelocity() / 20f);
        shot.GetComponent<Projectile>().ownerID = racerInfo.GetRacerInstanceID();

        return shot;
    }

    public void StartSpeedBoost() {
        speedBoost = maxBoost;
    }

    protected virtual void FinishRace() {
        // do stuff
    }

    protected abstract void DoMovement();
    protected abstract float RacerVelocity();

    public void Fire()
    {
        Shoot();
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Projectile") {
            if (racerInfo.GetRacerInstanceID() != other.gameObject.GetComponent<Projectile>().ownerID) {
                racerInfo.TakeDamage(.1f);
                Destroy(other.gameObject);
            }
        }
    }
}
