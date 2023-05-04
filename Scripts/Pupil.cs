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

public class Pupil : MonoBehaviour
{
    private GliaBehaviour _gliaBehaviour = null;
    private float leftPupilDilation = 0;
    private float rightPupilDilation = 0;
    private float baselineLeftPupilDilation = 0;
    private float baselineRightPupilDilation = 0;
    private float baselineAveragePupilDilation = 0;
    private bool hasTakenBaseline = false;
    string filePath = @"C:\Users\ARVR Lab\Documents\pupil.txt"; // For testing in lab only
    private GliaBehaviour gliaBehaviour //this defines gliaBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        if (hasTakenBaseline != true)
        {
            var pupillometry = gliaBehaviour.GetLastEyeTracking();
            //set the baseline values and mark that the baseline has been taken
            baselineLeftPupilDilation = pupillometry.LeftEye.PupilDilation;
            baselineRightPupilDilation = pupillometry.RightEye.PupilDilation;
            baselineAveragePupilDilation = (baselineLeftPupilDilation + baselineRightPupilDilation) / 2;
            hasTakenBaseline = true;

            //write the baseline values to a file
            //using (var writer = new StreamWriter("pupil_dilation.txt", true))

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Baseline Left Pupil Dilation: " + baselineLeftPupilDilation);
                writer.WriteLine("Baseline Right Pupil Dilation: " + baselineRightPupilDilation);
                writer.WriteLine("Baseline Average Pupil Dilation: " + baselineAveragePupilDilation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var pupillometry = gliaBehaviour.GetLastEyeTracking();
        if (pupillometry != null)
        {
            //get the left and right pupil dilation values
            leftPupilDilation = pupillometry.LeftEye.PupilDilation;
            rightPupilDilation = pupillometry.RightEye.PupilDilation;

            if (hasTakenBaseline != true)
            {
                //set the baseline values and mark that the baseline has been taken
                baselineLeftPupilDilation = leftPupilDilation;
                baselineRightPupilDilation = rightPupilDilation;
                baselineAveragePupilDilation = (baselineLeftPupilDilation + baselineRightPupilDilation) / 2;
                hasTakenBaseline = true;

                //write the baseline values to a file
                //using (var writer = new StreamWriter("pupil_dilation.txt", true))

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Baseline Left Pupil Dilation: " + baselineLeftPupilDilation);
                    writer.WriteLine("Baseline Right Pupil Dilation: " + baselineRightPupilDilation);
                    writer.WriteLine("Baseline Average Pupil Dilation: " + baselineAveragePupilDilation);
                }
            }
            //calculate the average pupil dilation
            var averagePupilDilation = (leftPupilDilation + rightPupilDilation) / 2;

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Left Pupil Dilation: " + leftPupilDilation);
                writer.WriteLine("Right Pupil Dilation: " + rightPupilDilation);
                writer.WriteLine("Average Pupil Dilation: " + averagePupilDilation);
            }
        }
    }


    public string GetData(){
        var pupillometry = gliaBehaviour.GetLastEyeTracking();
        if (pupillometry != null)
        {
            //get the left and right pupil dilation values
            leftPupilDilation = pupillometry.LeftEye.PupilDilation;
            rightPupilDilation = pupillometry.RightEye.PupilDilation;
            //calculate the average pupil dilation
            var averagePupilDilation = (leftPupilDilation + rightPupilDilation) / 2;

            string pupilData = leftPupilDilation.ToString() + "," + rightPupilDilation.ToString() + "," + averagePupilDilation.ToString();
            return pupilData;
        }
        else{
            return "PD 1 null,PD 2 null,PD 3 null";
        }
    }


}
