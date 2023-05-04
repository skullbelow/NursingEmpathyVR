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
using HP.Omnicept.Unity; //connects to gliaBehvaiour script
using System.Numerics;

public class GazeTracking : MonoBehaviour
{
    private GliaBehaviour _gliaBehaviour = null;
    private GliaLastValueCache lastValueCache;
    private EyeGaze leftgazePosition;
    private EyeGaze rightgazePosition;
    private EyeGaze combineGaze;
    //private float gazeDuration = 0;
    //private bool isGazing = false;
    string filePath = @"C:\Users\ARVR Lab\Documents\gaze.txt"; // For testing in lab only
    private GliaBehaviour gliaBehaviour
    {
        get
        {
            if (_gliaBehaviour == null)
            {
                _gliaBehaviour = FindObjectOfType<GliaBehaviour>();
            }

            return _gliaBehaviour;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        var gazeData = gliaBehaviour.GetLastEyeTracking();
        if (gazeData != null)
        {
            //get the gaze position
            leftgazePosition = gazeData.LeftEye.Gaze; //gaze info for left eye
            rightgazePosition = gazeData.RightEye.Gaze; //gaze info for left eye
            combineGaze = gazeData.CombinedGaze; //gaze info for both

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Left Eye Gaze: " + leftgazePosition);
                writer.WriteLine("Right Eye Gaze: " + rightgazePosition);
                writer.WriteLine("Combined Gaze: " + combineGaze);
            }

        }
    }


    public string GetData(){
        var gazeData = gliaBehaviour.GetLastEyeTracking();
        if (gazeData != null)
        {
            //get the gaze position
            leftgazePosition = gazeData.LeftEye.Gaze; //gaze info for left eye
            rightgazePosition = gazeData.RightEye.Gaze; //gaze info for left eye
            combineGaze = gazeData.CombinedGaze; //gaze info for both

            string positionData = leftgazePosition.ToString() + "," + rightgazePosition.ToString() + "," + combineGaze.ToString();
            return positionData;
        }
        else{
            return "GT1 null,GT2 null,GT3 null";
        }
    }

}