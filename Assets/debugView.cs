using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class debugView : MonoBehaviour
{
    string UIText;
    bool DisplayDebug = false;
    public TextMeshProUGUI text;
    public GameObject flight;
    public GameObject Camera;
    flight Flight;
    public GameObject altitudeManager;
    altitudeManager AltitudeManager;
    
    // Start is called before the first frame update
    void Start()
    {
        Flight = flight.GetComponent<flight>();
        AltitudeManager = altitudeManager.GetComponent<altitudeManager>();
    }

    // Update is called once per frame
    void Update()
    {

        float height = flight.transform.position.y;
        Vector3 velocity = Flight.rb.velocity;
        int altitudeLayer = AltitudeManager.currentHeightLayer;
        float burst = Flight.burst;
        flightData flightProfile = Flight.FlightData;
        float constantForward = Flight.constantForward;
        Vector2 maxLimit = flightProfile.maxlimit;
        Vector2 minLimit = flightProfile.minlimit;
        
        UIText = $"height:{height:F2} \n velocity:{velocity} \n altitude layer: {altitudeLayer} \n flap power: {burst} \n  flight profile: {flightProfile} \n constant forward: {constantForward} \n MaxLimit: {maxLimit} \n MinLimit: {minLimit}";

        if (Input.GetButtonDown("Enable Debug Button 1") && DisplayDebug == false)
        {
            DisplayDebug = true;


        }
        else if (Input.GetButtonDown("Enable Debug Button 1") && DisplayDebug == true)
        {
            DisplayDebug = false;

        }


        if (DisplayDebug == true)
        {
            text.enabled = true;

        }
        else
        {
            text.enabled = false;

        }

        text.text = UIText;
    }
}
