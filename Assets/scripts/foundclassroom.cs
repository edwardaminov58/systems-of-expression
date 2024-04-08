using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class foundclassroom : MonoBehaviour
{
    Transform transform;
    // Start is called before the first frame update
    void Start()
    {
       transform = gameObject.GetComponent<Transform>();
        Debug.Log("print");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -40) {
            print("x"); 
            if (transform.position.z <= -40) {
                print("z");
                sceneLoad();
        }
        }
    }
    void sceneLoad()
    {
        SceneManager.LoadScene("scene3");
    }
}
