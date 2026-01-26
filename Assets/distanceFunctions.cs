using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceFunctions : MonoBehaviour
{
    public Vector3 size = new Vector3(404, 8.57854843f, 13.4292526f);
    public Vector3 size2 = new Vector3(2492.91089f, 52.9345436f, 82.8661652f);

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSprite()
    {
        //GetComponent<SpriteRenderer>().sprite = currentSprite;
    }   
    public void changePosition()
    {

    }    
    public void ChangeScale()
    {
        gameObject.transform.localScale = size2;
    }    
    public void changeAnimation()
    {

    }
    public void Gofast(float speed)
    {
        
        //gameObject.transform.position =
        StartCoroutine(MoveForward(speed));

    }
    IEnumerator MoveForward (float speed)
    {
        Vector3 heading = transform.position - Camera.main.transform.position;
        float distanceFromCamera = Vector3.Dot(heading, Camera.main.transform.forward);
        for (float t = 0; t < 1f; t += Time.deltaTime / distanceFromCamera)
        {
            gameObject.transform.position -= new Vector3(0,0,1) * speed;
            yield return null;
        }
    }
}
