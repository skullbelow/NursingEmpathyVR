using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using HP.Omnicept.Unity; //this connects to the gliaBehaviour script

public class HeartBPM : MonoBehaviour
{
    private float bpm;
    private string filePath = @"C:\Users\ARVR Lab\Documents\heartrate.txt"; // For testing in lab only
    private GliaBehaviour _gliaBehaviour = null;
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
    // Start is called before the first frame update
    void Start()
    {
        //baseline will go first
    }

    // Update is called once per frame
    void Update()
    {
        var heartrate = gliaBehaviour.GetLastHeartRate();
        bpm = heartrate.Rate;

        //Writes value to a text file
        using (var writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Heart Rate: " + bpm);

            //Continuously read heart rate and spit out heart rate
        }
    }

    public string GetData(){
        var heartrate = gliaBehaviour.GetLastHeartRate();
        if (heartrate != null)
            return heartrate.Rate.ToString();
        else
            return "HB null";
    }

}
