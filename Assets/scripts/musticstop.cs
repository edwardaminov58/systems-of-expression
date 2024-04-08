using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musticstop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<musicclass>().StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
