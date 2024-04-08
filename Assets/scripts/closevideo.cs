using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class closevideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("collided");
        var videoPlayer = gameObject.GetComponent<VideoPlayer>();
        other.GetComponent<firstpersonedward>().enabled = false;
        videoPlayer.Play();
        videoPlayer.loopPointReached += close;
        
    }
    void close(VideoPlayer videoplayer)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<firstpersonedward>().enabled = true;
        gameObject.SetActive(false);
    }
}
