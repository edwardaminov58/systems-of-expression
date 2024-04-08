using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript1 : MonoBehaviour
{
    public TextMeshProUGUI theText;
    public string totalLines;
    public string textLine;
    bool isTyping = false;
    public float typeSpeed;

    public TextAsset textFile;
    // Start is called before the first frame update
    void Start()
    {
        if (textFile != null)
        {
            textLine = (textFile.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;

        while (isTyping && (letter < lineOfText.Length))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }
}
