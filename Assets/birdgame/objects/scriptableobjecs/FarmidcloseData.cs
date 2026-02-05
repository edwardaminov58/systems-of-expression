using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FarmidcloseData", menuName = "ScriptableObjects/FarmidcloseData")]
public class FarmidcloseData : ScriptableObject
{
    public float distancefromBirdtoSpriteChange;
    public Sprite NewSprite;
    public Vector3 endScale;
    public float distancefromBirdtoStopScaling;

}
