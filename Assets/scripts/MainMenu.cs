using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject lobbyPanel;
    public GameObject controlPanel;
    public GameObject creditPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame() {
        lobbyPanel.SetActive(true);
        controlPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void Controls() {
        lobbyPanel.SetActive(false);
        controlPanel.SetActive(true);
        creditPanel.SetActive(false);
    }

    public void Credits() {
        lobbyPanel.SetActive(false);
        controlPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
