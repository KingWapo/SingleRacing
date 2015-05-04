using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public AIRacer aiShip;
    public static int numRacers;

    private List<string> queuedMaps;

    private int[] racerScores;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);

        numRacers = 4;// aiShip.ships.Length + 1;

        queuedMaps = new List<string>();

        racerScores = new int[numRacers];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void QueueMap(string mapName) {
        queuedMaps.Add(mapName);
    }

    public void NextLevel() {
        if (queuedMaps.Count > 0) {
            Application.LoadLevel(queuedMaps[0]);
            queuedMaps.RemoveAt(0);
        } else {
            Application.LoadLevel("Menu");
            Destroy(gameObject);
        }
    }

    public int[] GetScores() {
        return racerScores;
    }

    public void AddScore(int index, int score) {
        racerScores[index] += score;
    }
}
