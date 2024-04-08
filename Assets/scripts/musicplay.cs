using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<musicclass>().PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
