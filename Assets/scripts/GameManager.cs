using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private List<string> queuedMaps;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        queuedMaps = new List<string>();
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
}
