using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class IntroToRandom : MonoBehaviour
{
    // Start is called before the first frame update
    VideoPlayer video;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
        //new RandomizeScene().LoadRandomScene(); // comment out line above and uncomment this one to skip intro video for testing 
    }
 
     void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        RandomizeScene randomizeScene = new RandomizeScene();
        randomizeScene.LoadRandomScene();
    }
}
