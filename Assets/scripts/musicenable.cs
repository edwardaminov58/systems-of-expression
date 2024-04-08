using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicenable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Object;
    public GameObject thunder;
  


    void SetOn()
    {
        Object.SetActive(true);
    }

    void SetThunder()
    {
        thunder.SetActive(true);
    }

}
