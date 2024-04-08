using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class randomnumber : MonoBehaviour
{
    TextMeshProUGUI text;
    float number;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        InvokeRepeating("estimatedTime", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void estimatedTime()
    {
        number = Random.Range(8, 17);
        text.text = (number + "min");
    }
}
