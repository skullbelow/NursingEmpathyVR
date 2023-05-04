using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Tobii.XR;
using HP.Omnicept;
using HP.Omnicept.Messaging;
using HP.Omnicept.Messaging.Messages;
using System.Threading.Tasks;
using System.IO;
using HP.Omnicept.Unity; //this connects to the gliaBehaviour script

public class DataWrapper : MonoBehaviour
{
    Pupil pupilObj = new Pupil();
    GazeTracking gazeObj = new GazeTracking();
    HeartBPM heartObj = new HeartBPM();
    TextWriter tw;
    int frameNum = 1;
    private string sensorDataPath = @"..\SensorData.csv";

    // Start is called before the first frame update
    void Start()
    {
        //Creating sensor data file, writing User ID, and writing header fields for sensor data
        tw = new StreamWriter(sensorDataPath,false); // false means write-mode (will overwite)

        // Writing User ID (Ideally this would be moved to the intro video scene)
        tw.WriteLine("User ID");
        tw.WriteLine($"{new System.Random().Next()}");
        tw.WriteLine(); // Added for spacing
        tw.WriteLine(); // Added for spacing
        tw.WriteLine(); // Added for spacing

        // Writing header fields for sensor data then closing file
        tw.WriteLine("Time-Stamp, Left Pupil Dilation, Right Pupil Dilation, Avg. Pupil Dilation, Left Gaze Tracking, Right Gaze Tracking, Combined Gaze, Heart Rate");
        tw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        //if(frameNum % 30 == 0){ // writes to CSV every 30 frames (can be changed)
        tw = new StreamWriter(sensorDataPath,true); // true means append-mode (will NOT overwite)
        tw.WriteLine(System.DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffffff") + "," + pupilObj.GetData() + "," + gazeObj.GetData() + "," + heartObj.GetData());
        tw.Close();   
        //}

        frameNum++;
    }

    /*
    TODO:
    For adding onto this file in other scenes (i.e. saving survey data, intro video, etc.)
    TextWriter tw; // In class, above methods

    // In update (or wherever else makes sense)
    tw = new StreamWriter(@"C:\Users\genev\OneDrive\Desktop\Nursing VR Stuff\SensorData.csv",true); // true means append-mode (will NOT overwite)
    tw.WriteLine(); // Added for spacing
    tw.WriteLine(); // Added for spacing
    tw.WriteLine(); // Added for spacing
    tw.WriteLine("Whatever data you want to add");
    tw.Close();
    */

}
