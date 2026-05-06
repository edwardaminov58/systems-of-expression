using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space : MonoBehaviour
{
    public Material skyboxMat;
    public GameObject bird;
    //Material SpaceMaterial;
    // Start is called before the first frame update
    void OnEnable()
    {
        RenderSettings.skybox = skyboxMat;
        skyboxMat.mainTextureScale = new Vector2(.6f, .6f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        skyboxMat.mainTextureScale -= new Vector2(.02f, .02f) * Time.deltaTime;
        skyboxMat.mainTextureOffset = new Vector2(.42f, .8f - 125/bird.transform.position.y);
    }
}
