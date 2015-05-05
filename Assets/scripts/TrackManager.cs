using UnityEngine;
using System.Collections;

public class TrackManager : MonoBehaviour {

    public GameObject[] waypoints;
    public GameObject startPoint;

    public GameObject playerCar;
    public GameObject aiCar;

    private GameManager manager;

    private bool[] shipChosen;
    private string[] shipName = {"femur", "Drew", "Ate - Bitty", "QuadCopter", "Gnome", "Duskalo"};

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
                GameObject pc = (GameObject)Instantiate(playerCar, startPos, startPoint.transform.rotation);
                PlayerRacer pRacer = pc.GetComponent<PlayerRacer>();
                RacerInfo pInfo = pc.GetComponent<RacerInfo>();

                pRacer.racerIndex = 0;
                pInfo.racerName = ">>THIS IS YOU<<";
            } else {
                GameObject ai = (GameObject) Instantiate(aiCar, startPos, startPoint.transform.rotation);

                AIRacer aRacer = ai.GetComponent<AIRacer>();
                RacerInfo aInfo = ai.GetComponent<RacerInfo>();

                int shipIndex;

                do {
                    shipIndex = Random.Range(0, aRacer.ships.Length);
                } while (shipChosen[shipIndex]);

                aRacer.ships[shipIndex].SetActive(true);
                aInfo.racerName = shipName[shipIndex];
                shipChosen[shipIndex] = true;
                aRacer.racerIndex = shipIndex + 1;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
