using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class distancemanager : MonoBehaviour
{
    public GameObject bird;
    //public UnityEvent Distance1;
    //public UnityEvent Distance2;
    public UnityEvent[] DistanceEvents;
    public float[] distances;
    //public GameObject altitudeManagerObject;
    public altitudeManager AltitudeManager;
    public int Layer;

    // Start is called before the first frame update
    void Start()
    {
        //AltitudeManager = altitudeManagerObject.GetComponent<altitudeManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        for (int n = 0; n < distances.Length; n++)
        {
            if (bird.transform.position.z >= distances[n])
            {
                DistanceInvoke(n);
            }
        }
    }

    void DistanceInvoke(int distanceIndex)
    {
        //if (AltitudeManager.currentHeightLayer == Layer)
        DistanceEvents[distanceIndex].Invoke();
    }

    public void SetInt(int Value)
    {
        Layer = Value;
    }
}
