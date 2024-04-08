using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playVideo());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator playVideo()
    {
        var videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;

        }

        Debug.Log("Done Preparing Video");
        videoPlayer.Play();
        //AudioSource audiodata;
       // audiodata = audio.GetComponent<AudioSource>();
       // audiodata.Play();

        videoPlayer.loopPointReached += EndReached;

    }
    void EndReached(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("credits");
    }
}
