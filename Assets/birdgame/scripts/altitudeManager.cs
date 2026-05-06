using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class altitudeManager : MonoBehaviour
{
    public GameObject bird;
    //ublic GameObject Base;
   //public GameObject Layer1;
    public Material material1;
    public Material material;
    Vector2 startvector;
    public Coroutine changeHeight;
    public static event Action onHeightChange;
    float startHeightLayer;
    //bool alt1;
    //bool alt2;
    public UnityEvent Alt0;
    public UnityEvent Alt1;
    public UnityEvent Alt2;
    public UnityEvent Alt3;
    public UnityEvent Alt4;
    public UnityEvent Alt5;
    public UnityEvent Alt6;
    public float[] altitudes;
    public int currentHeightLayer;
    int nextHeightLayer;
    int lastHeightLayer;



    // Start is called before the first frame update
    void Start()
    {

        //startvector = material.GetVector("_offset2");
        //currentHeightLayer = altitudes[n];
        //currentHeightLayer = bird.transform.position.y;
        //Debug.Log("currentHeightLayer: " + currentHeightLayer);
        //Alt1.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        for (int n = 0; n +1<altitudes.Length; n++)
        {
            if (bird.transform.position.y > altitudes[n] && bird.transform.position.y < altitudes[n + 1])
            {
                //Debug.Log(currentHeightLayer);
                lastHeightLayer = currentHeightLayer;
                currentHeightLayer = n;
                //if (currentHeightLayer! = )
                //Debug.Log(currentHeightLayer);
                //Debug.Log(lastHeightLayer);
                if (lastHeightLayer != currentHeightLayer)
                {
                    AltitudeChange(currentHeightLayer);
                    //Debug.Log("changelayer");
                    //Debug.Log("currentHeightLayer: " + currentHeightLayer); 
                    //Debug.Log("lastHeightLayer: " + lastHeightLayer);

                }
            }


            //nextHeightLayer = altitudes[n + 1];
                //onHeightChange?.Invoke();

            //if (bird.transform.position.y > 0 && bird.transform.position.y < 20)
            //{
            //    Base.SetActive(true);
            //    Layer1.SetActive(false);

            //}
            //else if (bird.transform.position.y > 20)
            //{
            //    Base.SetActive(false);
            //    Layer1.SetActive(true);
            //}

        }

    }


    void AltitudeChange(int currentHeightLayer)
    {
        switch (currentHeightLayer)
        {
            case 0: 
                Alt0.Invoke();
                Debug.Log("case0");
                break;
            case 1:
                Debug.Log("case1");
                Alt1.Invoke();
                break;
            case 2:
                Debug.Log("case2");
                Alt2.Invoke();
                break;
            case 3:
                Alt3.Invoke();
                Debug.Log("case0");
                break;
            case 4:
                Debug.Log("case1");
                Alt4.Invoke();
                break;
            case 5:
                Debug.Log("case2");
                Alt5.Invoke();
                break;           
            case 6:
                Debug.Log("case2");
                Alt6.Invoke();
                break;
        }
    }
    //public void Alt1()
    //{
    //    Base.SetActive(true);
    //    Layer1.SetActive(false);
    //    //RenderSettings.skybox = material;
    //    //material.SetVector("_tiling", new Vector2(2, 1));
    //    //alt1 = true;
    //    //alt2 = false;
    //    //log();
    //}
    //public void Alt2()
    //{
    //    Base.SetActive(false);
    //    Layer1.SetActive(true);
    //    //RenderSettings.skybox = material;
    //    //material.SetVector("_tiling", new Vector2(2, 1));

    //    //    alt2 = true;
    //    //    alt1 = false;
    //    //    log();
    //}
}



    //public void log()
    //{
    //    Debug.Log("trigger");
    //}
    //}
