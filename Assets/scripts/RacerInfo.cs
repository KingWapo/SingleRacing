using UnityEngine;
using System.Collections;

public class RacerInfo : MonoBehaviour {

    // name
    public string racerName;

    // renderers
    private MeshRenderer[] renderers;
    private bool rendering = true;

    // racer position
    public int wayPointsHit = 0;
    private GameObject previousWaypoint;

    private PowerupText powerText;

    public AudioSource pickupSound;

    // gun upgrades
    private int spreadLevel;
    public static int minSpreadLevel = 1;
    public static int maxSpreadLevel = 5;

    private int fireRateLevel;
    public static int minFireRateLevel = 0;
    public static int maxFireRateLevel = 3;

    // speed upgrades
    private Racer racer;

    // racer health
    private float health;
    public static float maxHealth = 1f;

    private bool respawning = false;

    private float respawnDelay;
    public static float maxRespawnDelay = 3f;

    private bool canMove = true;

	// Use this for initialization
    void Start() {
        renderers = GetComponentsInChildren<MeshRenderer>();

        powerText = GameObject.FindGameObjectWithTag("PowerupText").GetComponent<PowerupText>();

        spreadLevel = minSpreadLevel;
        fireRateLevel = minFireRateLevel;

        racer = GetComponent<Racer>();

        health = maxHealth;
	}
	
	// Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Keypad2)) {
            IncreaseSpreadLevel();
        }

        if (Input.GetKeyUp(KeyCode.Keypad3)) {
            IncreaseFireRateLevel();
        }

        if (respawning) {
            float modVal = .3f;
            float splitVal = modVal / 2f;

            if (respawnDelay % modVal < splitVal && !rendering) {
                SetRendererStatus(true);
            } else if (respawnDelay % modVal >= splitVal && rendering) {
                SetRendererStatus(false);
            }

            respawnDelay -= Time.deltaTime;

            if (respawnDelay < 0) {
                respawning = false;
                SetRendererStatus(true);
                GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void IncreaseSpreadLevel() {
        spreadLevel++;
        spreadLevel = Mathf.Clamp(spreadLevel, minSpreadLevel, maxSpreadLevel);
    }

    public int GetSpreadLevel() {
        return spreadLevel;
    }

    public void IncreaseFireRateLevel() {
        fireRateLevel++;
        fireRateLevel = Mathf.Clamp(fireRateLevel, minFireRateLevel, maxFireRateLevel);
    }

    public int GetFireRateLevel() {
        return fireRateLevel;
    }

    public void TakeDamage(float amount) {
        if (health > 0 && !respawning) {
            health -= amount;

            if (health < 0) {
                StartCoroutine(Respawn());
            }
        }
    }

    public float GetHealth() {
        return health;
    }

    private IEnumerator Respawn() {
        SetRendererStatus(false);
        GetComponent<BoxCollider>().enabled = false;
        canMove = false;

        yield return new WaitForSeconds(3f);

        respawning = true;
        respawnDelay = maxRespawnDelay;

        health = maxHealth;
        canMove = true;
    }

    public float GetRespawnDelay() {
        return respawnDelay;
    }

    public bool IsRespawning() {
        return respawning;
    }

    public bool CanMove() {
        return canMove;
    }

    private void SetRendererStatus(bool active) {
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].enabled = active;
        }

        rendering = active;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Waypoint" && other.gameObject != previousWaypoint) {
            wayPointsHit++;
            previousWaypoint = other.gameObject;

            if (wayPointsHit == FindObjectOfType<TrackManager>().waypoints.Length * 3 + 1) {
                racer.FinishRace();
            }
        }
        if (other.tag == "Powerup")
        {
            pickupSound.Play();
            PowerupType type = (PowerupType)Random.Range(0, (int)PowerupType.Max);
            switch(type)
            {
                case PowerupType.Spreadshot:
                    IncreaseSpreadLevel();
                    if (GetComponent<PlayerRacer>()) powerText.Show("Improved Spread Shot");
                    break;
                case PowerupType.FireRate:
                    IncreaseFireRateLevel();
                    if (GetComponent<PlayerRacer>()) powerText.Show("Improved Fire Rate");
                    break;
                case PowerupType.Speed:
                    racer.IncreasePermanentBoost();
                    if (GetComponent<PlayerRacer>()) powerText.Show("Improved Speed");
                    break;
            }
        }
    }

    public int GetRacerInstanceID() {
        return GetInstanceID();
    }

    public float GetScore()
    {
        if (previousWaypoint)
        {
            float score = wayPointsHit * 100;
            score += Vector3.Distance(transform.position, previousWaypoint.transform.position);
            return score;
        }
        else
        {
            return 0;
        }
    }
}
