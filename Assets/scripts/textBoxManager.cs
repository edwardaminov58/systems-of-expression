using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class textBoxManager : MonoBehaviour {

	public GameObject textBox;

	public TextMeshProUGUI theText;
    public TextMeshProUGUI oldText;

	public TextAsset textFile;
	public string textLines;
    public string textOld;

	public int currentLine;
	public int endAtLine;

	public float typeSpeed;
	private bool isTyping = false;

	// Use this for initialization
	void Start () {

		if (textFile != null) {
			//textLines = (textFile.text.Split ('\n'));
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length -1;
		}

	}

	private IEnumerator TextScroll(string lineOfText) {
		int letter = 0;
		theText.text = "";
		isTyping = true;

		while (isTyping && (letter < lineOfText.Length)) {
			theText.text += lineOfText [letter];
			letter += 1;
			yield return new WaitForSeconds (typeSpeed);
		}
		yield return new WaitForSeconds (2);
        oldText.text = lineOfText;
		theText.text+= lineOfText;
		isTyping = false;
	}

	void Update() {
		if (!isActiveAndEnabled) {
			return;
		}
		if (!isTyping) {
			currentLine += 1;
			// upon reaching end of text, reset to beginning
			//if(currentLine > endAtLine) {
			//	currentLine = 0;
			//}
			StartCoroutine (TextScroll (textLines));

		}
	}
}
