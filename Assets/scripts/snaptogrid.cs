using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snaptogrid : MonoBehaviour
{
    public float PPU = 96; // pixels per unit (your tile size)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        Vector2 position = transform.localPosition;

        position.x = (Mathf.Round(transform.parent.position.x * PPU) / PPU) - transform.parent.position.x;
        position.y = (Mathf.Round(transform.parent.position.y * PPU) / PPU) - transform.parent.position.y;

        transform.localPosition = position;
    }
}
