using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerupText : MonoBehaviour {

    private int lifetime = 0;

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (lifetime <= 0)
        {
            text.text = "";
        }
        else
        {
            lifetime--;
        }
	}

    public void Show(string newText)
    {
        lifetime = 300;
        text.text = newText;
    }
}
