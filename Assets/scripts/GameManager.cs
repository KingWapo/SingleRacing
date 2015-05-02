using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private RacerInfo[] racers;

    public enum State {
        PreRace, Racing, PostRace
    };

    private static State state;

	// Use this for initialization
	void Start () {
        state = State.Racing;

        racers = FindObjectsOfType<RacerInfo>();
        UpdatePosition();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Racing) {
            UpdatePosition();
        }
	}

    public static void SetState(State newState) {
        state = newState;
    }

    public static State GetState() {
        return state;
    }

    private void UpdatePosition() {
        if (racers.Length > 0) {
            for (int i = 0; i < racers.Length; i++) {
                // logic for determining position
            }
        }
    }
}
