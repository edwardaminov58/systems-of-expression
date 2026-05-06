using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : MonoBehaviour
{
    public Material SkyboxMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        RenderSettings.skybox = SkyboxMat;
        RenderSettings.skybox.mainTextureScale = new Vector2(0.93f, 2.63f);
        RenderSettings.skybox.mainTextureOffset = new Vector2(.3f, .12f);
        RenderSettings.ambientSkyColor = Color.white;
        RenderSettings.ambientEquatorColor = new Color32(226, 226, 226, 1);
        RenderSettings.ambientGroundColor = Color.black;
    }
}
