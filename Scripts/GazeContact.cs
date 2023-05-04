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
using HP.Omnicept.Unity;

public class GazeContact : MonoBehaviour
{
    private GliaBehaviour _gliaBehaviour = null;
    private int totalEyeContactDuration = 0;
    private int totalNoEyeContactDuration = 0;
    private EyeGaze gazeTracking; //ref to gaze tracking script
    private bool isGazing = false;
    private float minGazeDuration = 2; // minimum time required to count as a gaze, in seconds
    string filePath = @"C:\Users\ARVR Lab\Documents\eye_contact.txt"; // For testing in lab only
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
       // gazeTracking = FindObjectOfType<GazeTracking>(); //get reference to gaze tracking script
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            var gazeData = gliaBehaviour.GetLastEyeTracking();
            gazeTracking = gazeData.CombinedGaze;
            //check if the player is making eye contact with the collider
         /*   if (//if gazedata is in range of collider boxes)
            {
                //if making eye contact with collider, increment eye contact duration
                if (!isGazing)
                {
                    isGazing = true;
                }
                else
                {
                    totalEyeContactDuration++;
                }
            }
            else
            {
                //if not making eye contact increment not making eye contact duration
                if (isGazing)
                {
                    isGazing = false;
                }
                else
                {
                    totalNoEyeContactDuration++;
                }
            }
         */

            // Visualize the contact point
            // Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    private void OnDestroy()
    {
        //after simulation is complete
        //calculate the percentage of time making eye contact vs not and write to log file
        float contactPercentage = totalEyeContactDuration / (totalEyeContactDuration + totalNoEyeContactDuration) * 100;

        //write values to text file for now
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Total gaze time: " + totalEyeContactDuration + " seconds");
            writer.WriteLine("Total no gaze time: " + totalNoEyeContactDuration + " seconds");
            //calculate percentage of time making eye contact
            writer.WriteLine("Gaze percentage: " + contactPercentage.ToString("F2") + "%"); //F2 formats it to 2 decimal places
        }
    }
}
