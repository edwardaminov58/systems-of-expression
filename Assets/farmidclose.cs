using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farmidclose : MonoBehaviour
{
    Animator anim;
    public FarmidcloseData farmidcloseData;
    //public float distancefromBirdtoSpriteChange;
    Vector3 newTransform;
    Vector3 startScale;
     GameObject bird;
   // public Sprite NewSprite;
   // public Vector3 endScale;
    float z = 0;
    //public float distancefromBirdtoStopScaling;
    float distancefromBirdGlobal;
    public bool SpriteChanged;
    public bool Grow;
    public bool Animating;
    Transform Distance;
    //Transform parentTransform;
    //Transform childTransform;
    // Start is called before the first frame update
    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("flight");
        if (farmidcloseData.DistanceParentTransform == true)
            Distance = gameObject.transform.parent;
        else
            Distance = gameObject.transform;
        //parentTransform = GetComponent<Transform>();
        //childTransform = parentTransform.GetChild(0);
        
        startScale = transform.localScale;
    }

    // Update is called once per frame  
    void Update()
    {
        if (Grow)
            GrowBigger();
        if (farmidcloseData.NewSprite != null && Distance.position.z - bird.transform.position.z <= farmidcloseData.distancefromBirdtoSpriteChange && Distance.position.z - bird.transform.position.z > 0&& SpriteChanged == false)
            spriteChange();
        if (Animating && Distance.position.z - bird.transform.position.z <= farmidcloseData.distancefromBirdtoSpriteChange && SpriteChanged == false)
            animateChange();
    }
    public void GrowBigger()
    {

        // transform.localScale = Vector3.Lerp(startScale, endScale, z);
        newTransform.x = Mathf.SmoothStep(startScale.x, farmidcloseData.endScale.x, z);
        newTransform.y = Mathf.SmoothStep(startScale.y, farmidcloseData.endScale.y, z);
        newTransform.z = Mathf.SmoothStep(startScale.z, farmidcloseData.endScale.z, z);
        transform.localScale = newTransform;
        //z = (bird.transform.position.z - startDistance) / (targetBirdDistanceScale + startDistance);
        z = ( bird.transform.position.z) / ((Distance.position.z - farmidcloseData.distancefromBirdtoStopScaling));
            //Debug.Log(z + "=" + bird.transform.position.z + "-" + Distance.position.z + "/" + farmidcloseData.distancefromBirdtoStopScaling);
        //Debug.Log(z + "=" + bird.transform.position.z + "/" + transform.position.z + "-" + bird.transform.position.z); 
        if (z >= 1)
        {
            Grow = false;

        }

    }
    public void spriteChange()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = farmidcloseData.NewSprite;    
        SpriteChanged = true;
    }

    public void animateChange()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        anim.SetBool("spritechange", true);
        SpriteChanged = true;
    }

    //Debug.Log(z + "=" + bird.transform.position.z + "/" + targetBirdDistance + "+" + birdStart);
}
