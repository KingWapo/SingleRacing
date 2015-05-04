using UnityEngine;
using System.Collections;

public class TrackManager : MonoBehaviour {

    public GameObject[] waypoints;
    public GameObject startPoint;

    public GameObject playerCar;
    public GameObject aiCar;

    private GameManager manager;

    private bool[] shipChosen;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<GameManager>();

        if (!manager) {
            gameObject.AddComponent<GameManager>();
        }

        int playerPos = Random.Range(0, GameManager.numRacers);

        shipChosen = new bool[GameManager.numRacers - 1];

        for (int i = 0; i < GameManager.numRacers; i++) {
            Vector3 offset = new Vector3();

            offset.z = 8 + (4 * (i / 2));
            offset.x = 3 * ((i % 2) == 0 ? -1 : 1);

            Vector3 startPos = startPoint.transform.position + offset;

            if (playerPos == i) {
                PlayerRacer pc = ((GameObject)Instantiate(playerCar, startPos, startPoint.transform.rotation)).GetComponent<PlayerRacer>();

                pc.racerIndex = 0;
            } else {
                AIRacer ai = ((GameObject) Instantiate(aiCar, startPos, startPoint.transform.rotation)).GetComponent<AIRacer>();

                int shipIndex;

                do {
                    shipIndex = Random.Range(0, ai.ships.Length);
                } while (shipChosen[shipIndex]);

                ai.ships[shipIndex].SetActive(true);
                shipChosen[shipIndex] = true;
                ai.racerIndex = shipIndex + 1;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
