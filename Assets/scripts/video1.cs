using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video1 : MonoBehaviour
{
    
    void Start()
    {
        var videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.Prepare();

    }

    // Update is called once per frame
    void Update()
    {
       var videoPlayer = gameObject.GetComponent<VideoPlayer>();
       if (videoPlayer.isPrepared) { 
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
            startVideo();
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                startVideo();
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
               stopVideo();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                stopVideo();
            }
           
        }
    }


    void startVideo()
    {
        var videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;
    }
    void stopVideo()
    {
        var videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.Pause();
    }
    void EndReached(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("credits");
        //videoPlayer.Stop();
    }
}