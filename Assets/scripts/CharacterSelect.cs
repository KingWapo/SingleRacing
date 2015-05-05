using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

    public int CharacterIndex;

    public GameManager manager;
    public MainMenu menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 1, 0);
	}

    void OnMouseDown()
    {
        manager.playerIndex = CharacterIndex;
        menu.StartGame();
    }
}
