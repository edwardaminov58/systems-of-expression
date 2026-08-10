using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudUV : MonoBehaviour
{
    public float altStart;
    public altitudeManager AltitudeManager;
    public float threshold;
    public GameObject bird;
    Material mat;
    public groundmoveSettings groundmoveSettings;
    public float sideSpeed;
    public float sphereOffsetBaseX;
    // Start is called before the first frame update
    void OnEnable()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeUV();
    }
    void ChangeUV()
    {
        //altStart = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];
        //threshold = (AltitudeManager.altitudes[AltitudeManager.currentHeightLayer + 1] - altStart);
        float t = (bird.transform.position.y - altStart) / threshold;

        //mat.SetVector("_tiling", new Vector2(1, Mathf.Lerp(tilingStart, tilingEnd, t)));
        mat.SetVector("_tiling", Vector2.Lerp(groundmoveSettings.tilingStart, groundmoveSettings.tilingEnd, t));
        //Debug.Log("changeUV t = " + t);
        //Debug.Log("altStart = " + altStart);
        //Debug.Log("threshhold = " + threshold);
        mat.SetVector("_offset", Vector2.Lerp(groundmoveSettings.offsetStart, groundmoveSettings.offsetEnd, t));

        // mat.SetVector("_center", new Vector2(0.5f, Mathf.Lerp(centerYstart, centerYend, t)));
        mat.SetVector("_center", Vector2.Lerp(groundmoveSettings.centerStart, groundmoveSettings.centerEnd, t));
        mat.SetVector("_strength", Vector2.Lerp(groundmoveSettings.StrengthStart, groundmoveSettings.StrengthEnd, t));
        mat.SetVector("_tiling2", Vector2.Lerp(groundmoveSettings.tiling2Start, groundmoveSettings.tiling2End, t));
        mat.SetVector("_offset2", Vector2.Lerp(groundmoveSettings.offset2Start, groundmoveSettings.offset2End, t));
        mat.SetVector("_sphereoffset", new Vector2(sphereOffsetBaseX - bird.transform.position.x / sideSpeed, 0));

    }

}
