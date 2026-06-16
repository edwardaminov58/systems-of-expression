using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "groundMoveSettings", menuName = "ScriptableObjects/groundMoveSettings")]
public class groundmoveSettings : ScriptableObject
{
    public Vector2 tilingStart;
    public Vector2 tilingEnd;
    public Vector2 offsetStart;
    public Vector2 offsetEnd;
    public Vector2 centerStart;
    public Vector2 centerEnd;
    public Vector2 StrengthStart;
    public Vector2 StrengthEnd;
    public Vector2 tiling2Start;
    public Vector2 tiling2End;
    public Vector2 offset2Start;
    public Vector2 offset2End;
    public Vector2 offsetSphereStart;
    public Vector2 offsetSphereEnd;

}
