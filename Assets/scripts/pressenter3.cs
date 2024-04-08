using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pressenter3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            scene();
        }

    }
    void scene()
    {
        SceneManager.LoadScene("scene6");
    }
}
