using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteConstraint : MonoBehaviour
{
    public Vector3 maxlimit;
    public Vector3 minlimit;
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
        pos.z = Mathf.Clamp01(pos.z);
        // transform.position = Camera.main.ViewportToWorldPoint(pos);
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, minlimit.x, maxlimit.x), Mathf.Clamp(localPos.y, minlimit.y, maxlimit.y), 0);
        //Debug.Log("constant drop: " + constantDrop);
    }
}
