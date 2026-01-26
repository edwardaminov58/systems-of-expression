using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgobjectoffset : MonoBehaviour
{
    public float DistancefromBirdinBG;
    public GameObject bird;
    float ZoffsetStart;
    public layerCulling Layerculling;
    public Sprite NewSprite;
    Vector3 startScale;
    public bool fg = false;
    public float scaleTime;
    public Vector3 endScale;
    float z = 0;
    public float targetBirdDistancefg;
    public float targetBirdDistanceScale;
    Vector3 newTransform;
    Vector3 startTransform;
    float birdStart;
    public GameObject nextTree;
    public bool finalobject = false;
    float y = 0;
    public float TargetDistanceFromBirdinFG;
    float distancefromBirdGlobal;
    public bool Grow;
    public bool Distancerender;
    public bool SpriteChange;
    bool distanceStarted;
    float startDistance;

    // Start is called before the first frame update
    void Start()
    {
        birdStart = bird.transform.position.z;

        startScale = transform.localScale;
        startTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z + bird.transform.position.z);
        //endScale = startScale * 2f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float t = bird.transform.localPosition.y / Layerculling.farPlaneThreshold;
        //Zoffset = Mathf.Lerp(ZoffsetStart, Layerculling.occlusionPlaneEnd, t);

        // transform.position = new Vector3(transform.position.x, transform.position.y, targetDistance);


    }
    private void Update()
    {
        if (Grow)
            //StartCoroutine(ScaleGrowth());
            GrowBigger();
        if (Distancerender)
            DistanceRender();
        if (SpriteChange)
            spriteChange();

    }

    public void DistanceRender()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + DistancefromBirdinBG);
    }
    public void GrowBigger()
    {
        if (distanceStarted) {
            transform.localScale = Vector3.Lerp(startScale, endScale, z);
            //z = (bird.transform.position.z - startDistance) / (targetBirdDistanceScale + startDistance);
            z = (bird.transform.position.z) / ((transform.position.z - bird.transform.position.z));
            //Debug.Log(z + "=" + bird.transform.position.z + "-" +startDistance + "/" + targetBirdDistanceScale + "+" + startDistance);
            Debug.Log(z + "=" + bird.transform.position.z + "/" + transform.position.z + "-" + bird.transform.position.z);
            if (z >= 1)
            {

                distanceStarted = false;
                Grow = false;
            }

        }
        else
        {
            startDistance = bird.transform.position.z;
            distanceStarted = true;
        }
        //Debug.Log(z + "=" + bird.transform.position.z + "/" + targetBirdDistance + "+" + birdStart);

    }
    public void spriteChange()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = NewSprite;
    }
    public void originalCode()
    {
        distancefromBirdGlobal = transform.position.z - bird.transform.position.z;
        Debug.Log(y + "=" + bird.transform.position.z + "-" + (targetBirdDistancefg + birdStart) + "/" + targetBirdDistancefg);
        //Debug.Log("Z=" + z);
        z = bird.transform.position.z / (targetBirdDistancefg + birdStart);
        y = (bird.transform.position.z - (targetBirdDistancefg + birdStart + TargetDistanceFromBirdinFG)) / (targetBirdDistanceScale);

        if (fg == false)
        {
            DistanceRender();


            //newTransform.x = Mathf.SmoothStep(startScale.x, endScale.x, z);
            //newTransform.y = Mathf.SmoothStep(startScale.y, endScale.y, z);
            //newTransform.z = Mathf.SmoothStep(startScale.z, endScale.z, z);
            //transform.localScale = newTransform;

        }
        if (distancefromBirdGlobal <= TargetDistanceFromBirdinFG)
        {
            GrowBigger();
            GetComponentInChildren<SpriteRenderer>().sprite = NewSprite;
        }
        if (z >= .7f)
        {


        }

        if (z >= 1f)
        {
            fg = true;


        }
        if (fg == true)
        {

            //this.enabled = false;
        }
    }

    IEnumerator ScaleGrowth()
    { 
        z = (bird.transform.position.z) / ((transform.position.z - bird.transform.position.z));
        transform.localScale = Vector3.Lerp(startScale, endScale, z);
        if (z >= 1)
        {
            yield return null;
        }

     }
    // create functions and booleans for these effects individually
    // change sprite size and sprit after certain distance difference between bird and tree is reached
    // troubleshoot and fix the growh bool method so that it grows evenly no matter where
    // create 3 objects, far, middle, close. far -> grow? mid stop distance rendering, middle -> close change sprite and grow?
}
