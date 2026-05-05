using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space : MonoBehaviour
{
    Material skyboxMat;
    public GameObject bird;
    //Material SpaceMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        skyboxMat = RenderSettings.skybox;
        skyboxMat.mainTextureScale = new Vector2(.6f, .6f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        skyboxMat.mainTextureScale -= new Vector2(.02f, .02f) * Time.deltaTime;
        skyboxMat.mainTextureOffset = new Vector2(.5f, .8f - 50/bird.transform.position.y);
    }
}
