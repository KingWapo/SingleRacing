using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum State {
        PreRace, Racing, PostRace
    };

    private static State state;

	// Use this for initialization
	void Start () {
        state = State.PreRace;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space)) {
            SetState(State.Racing);
        }
	}

    public static void SetState(State newState) {
        state = newState;
    }

    public static State GetState() {
        return state;
    }
}
