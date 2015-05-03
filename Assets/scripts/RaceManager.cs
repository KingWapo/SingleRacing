using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour {

    private GameObject[] racers;

    private float timeToWeapons;
    private float maxTimeToWeapons = 5f;

    private bool weaponsEnabled = false;

    public enum State {
        PreRace, Racing, PostRace
    };

    private static State state;

	// Use this for initialization
	void Start () {
        state = State.Racing;

        racers = GameObject.FindGameObjectsWithTag("Racer");
        UpdatePosition();

        ResetTimeToWeapons();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Racing) {
            UpdatePosition();

            if (timeToWeapons > 0) {
                timeToWeapons -= Time.deltaTime;
                //Debug.Log("DEBUG - TIME TO WEAPONS: " + timeToWeapons);
            } else if (!weaponsEnabled) {
                SetWeaponsActive(true);
            }
        }
	}

    public static void SetState(State newState) {
        state = newState;
    }

    public static State GetState() {
        return state;
    }

    public void ResetTimeToWeapons() {
        timeToWeapons = maxTimeToWeapons;
        weaponsEnabled = false;
    }

    public void SetWeaponsActive(bool active) {
        for (int i = 0; i < racers.Length; i++) {
            racers[i].GetComponent<Racer>().weaponsEnabled = active;
        }

        weaponsEnabled = active;
    }

    private void UpdatePosition() {
        if (racers.Length > 0) {
            for (int i = 0; i < racers.Length; i++) {
                // logic for determining position
            }
        }
    }
}
