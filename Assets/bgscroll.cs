using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgscroll : MonoBehaviour
{
    Material mat;
    Image img;
    public GameObject bird;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        mat = img.material;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 offset = mat.mainTextureOffset;
        //offset.x += x * Time.deltaTime/15;
        offset.x = bird.transform.localPosition.x/ 1500;
        offset.y = (bird.transform.localPosition.y / 1700) ;
        mat.mainTextureOffset = offset;
    }
}
