using UnityEngine;
using System.Collections;

public class TrackManager : MonoBehaviour {

    public GameObject[] waypoints;
    public GameObject startPoint;

    public GameObject playerCar;
    public GameObject aiCar;

    private int numRacers = 6;

	// Use this for initialization
	void Start () {
        int playerPos = Random.Range(0, numRacers);

        for (int i = 0; i < numRacers; i++) {
            Vector3 offset = new Vector3();

            offset.x = 8 + (4 * (i / 2));
            offset.z = 3 * ((i % 2) == 0 ? -1 : 1);

            Vector3 startPos = startPoint.transform.position + offset;

            if (playerPos == i) {
                Instantiate(playerCar, startPos, Quaternion.identity);
            } else {
                Instantiate(aiCar, startPos, Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
