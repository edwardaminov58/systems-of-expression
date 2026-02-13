using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class horizontalmanager : MonoBehaviour
{
    public float[] Horizontals;
    public GameObject bird;
    public UnityEvent[] HorizontalAirspaces;
    int currentPlane;
    int lastPlane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            for (int n = 0; n + 1 < Horizontals.Length; n++)
            {
                if (bird.transform.position.x > Horizontals[n] && bird.transform.position.x < Horizontals[n + 1])
                {
                lastPlane = currentPlane;
                currentPlane = n;
                if (lastPlane != currentPlane)
                    {
                    HorizontalInvoke(n);
                    //Debug.Log("changeplane");
                    //Debug.Log("currentPlane: " + currentPlane);
                    //Debug.Log("lastHeight: " + lastPlane);
                }
            }
            }
        
    }
    void HorizontalInvoke(int index)
    {
        HorizontalAirspaces[index].Invoke();
    }
}
