using UnityEngine;
using System.Collections;

public class RacerInfo : MonoBehaviour {

    // renderers
    private MeshRenderer[] renderers;
    private bool rendering = true;

    // racer position
    public int wayPointsHit = 0;

    // gun upgrades
    private int spreadLevel;
    public static int minSpreadLevel = 1;
    public static int maxSpreadLevel = 5;

    private int fireRateLevel;
    public static int minFireRateLevel = 0;
    public static int maxFireRateLevel = 3;

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

        spreadLevel = minSpreadLevel;
        fireRateLevel = minFireRateLevel;

        health = maxHealth;
	}
	
	// Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Keypad2)) {
            IncreaseSpreadLevel();
            Debug.Log("DEBUG - INCREASING SPREAD: " + spreadLevel);
        }

        if (Input.GetKeyUp(KeyCode.Keypad3)) {
            IncreaseFireRateLevel();
            Debug.Log("DEBUG - INCREASING FIRE RATE");
        }

        if (GetComponent<PlayerRacer>()) {
            Debug.Log("DEBUG - INSTANCE ID: " + GetRacerInstanceID());
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
        if (other.tag == "Waypoint") {
            wayPointsHit++;
        }
    }

    public int GetRacerInstanceID() {
        return GetInstanceID();
    }
}
