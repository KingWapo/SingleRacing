using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaceManager : MonoBehaviour {

    private GameObject[] racers;

    public Text countdown;
    public GameObject listing;
    public Button continueButton;

    public static List<string> raceListings;

    private static float timeToStart;
    public static float maxTimeToStart = 3f;

    private float timeToWeapons;
    private float maxTimeToWeapons = 5f;

    private bool weaponsEnabled = false;

    public enum State {
        PreRace, Racing, PostRace
    };

    private static State state;

	// Use this for initialization
	void Start () {
        state = State.PreRace;

        timeToStart = maxTimeToStart;

        racers = GameObject.FindGameObjectsWithTag("Racer");
        UpdatePosition();

        raceListings = new List<string>();

        ResetTimeToWeapons();

        continueButton.onClick.AddListener(() => { FindObjectOfType<GameManager>().NextLevel(); });
	}
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case State.PreRace:
                PreRace();
                break;
            case State.Racing:
                Racing();
                break;
            case State.PostRace:
                PostRace();
                break;
        }
	}

    private void PreRace() {
        timeToStart -= Time.deltaTime;

        countdown.text = ((int)timeToStart + 1) + "";

        if (timeToStart < 0) {
            state = State.Racing;
            countdown.gameObject.SetActive(false);
        }
    }

    private void Racing() {
        UpdatePosition();

        if (timeToWeapons > 0) {
            timeToWeapons -= Time.deltaTime;
            //Debug.Log("DEBUG - TIME TO WEAPONS: " + timeToWeapons);
        } else if (!weaponsEnabled) {
            SetWeaponsActive(true);
        }
    }

    private void PostRace() {
        if (!listing.gameObject.activeSelf) {
            listing.gameObject.SetActive(true);
        }

        Text playerListings = listing.GetComponentInChildren<Text>();

        playerListings.text = "";

        for (int i = 0; i < raceListings.Count; i++) {
            playerListings.text += raceListings[i];
            playerListings.text += "\n";
        }

        if (raceListings.Count == GameManager.numRacers) {
            playerListings.text += "\nALL PLAYERS FINISHED";
        } else {
            playerListings.text += "\nWAIT FOR OTHERS\nOR CONTINUE";
        }
    }

    public static void SetState(State newState) {
        state = newState;
    }

    public static State GetState() {
        return state;
    }

    public static float GetTimeToStart() {
        return timeToStart;
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
        string placement = "";
        if (racers.Length > 0) {
            for (int i = 0; i < racers.Length - 1; i++) {
                if (racers[i].GetComponent<RacerInfo>().GetScore() < 
                    racers[i + 1].GetComponent<RacerInfo>().GetScore())
                {
                    GameObject temp = racers[i];
                    racers[i] = racers[i + 1];
                    racers[i + 1] = temp;
                }
                placement += (i + 1) + ": " + racers[i].gameObject.name + " Score: " + racers[i].GetComponent<RacerInfo>().GetScore() + "\n";
            }
            placement += (racers.Length) + ": " + racers[racers.Length - 1].gameObject.name;
            //Debug.Log(placement);
        }
    }
}
