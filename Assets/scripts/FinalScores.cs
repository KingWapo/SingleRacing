using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalScores : MonoBehaviour {

    public Button continueButton;
    public Text names;
    public Text scores;

	// Use this for initialization
	void Start () {
        GameManager manager = FindObjectOfType<GameManager>();

        continueButton.onClick.AddListener(() => { manager.NextLevel(); });

        names.text = "";
        scores.text = "";

        string[] nameList = manager.GetNames();
        int[] scoreList = manager.GetScores();

        System.Array.Sort(scoreList, nameList);

        for (int i = nameList.Length - 1; i >= 0; i--) {
            names.text += nameList[i] + ":\n";
            scores.text += scoreList[i] + "\n";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
