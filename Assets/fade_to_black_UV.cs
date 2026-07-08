using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade_to_black_UV : MonoBehaviour
{
    public GameObject bird;
    Material mat;
    public altitudeManager AltitudeManager;
    float darkeningStart;
    float t;


    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
        darkeningStart = mat.GetFloat("_Darkening");
    }


    private void OnEnable()
    {
       
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
        if (bird.transform.position.y > AltitudeManager.altitudes[AltitudeManager.currentHeightLayer + 1] - 50)
        {
            
            mat.SetFloat("_Darkening", Mathf.Lerp(1.5f, 5f, t));
            t += Time.deltaTime/ .5f ;
        }
    }
}
