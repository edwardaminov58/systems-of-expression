using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteConstraint : MonoBehaviour
{
    public Vector2 maxlimit;
    public Vector2 minlimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClampPosition();
    }
    void ClampPosition()
    {
        Vector3 localPos = transform.localPosition;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        // transform.position = Camera.main.ViewportToWorldPoint(pos);
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, minlimit.x, maxlimit.x), Mathf.Clamp(localPos.y, minlimit.y, maxlimit.y), localPos.z);
        //Debug.Log("constant drop: " + constantDrop);
    }
}
