using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class UIButtonClick : MonoBehaviour
{
    public void SurveyAnswer(QuestionManager questionManager)
    {
        string videoName;

        try
        {
            // TODO: Read from temp video name file and save in string var. TODO: Delete this whole comment after successful tests
            List<string> videoNames = new List<string>(File.ReadAllLines(@"..\videoName.txt")); // Technically does not ever need to be list but I'm lazy and using ReadAllLines() works easier this way...
            videoName = videoNames[0];
        }
        catch
        {
            videoName = "No video played prior (testing likert scale or some other error occured)";
        }


        string ClickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log(ClickedButtonName);
        using (StreamWriter writer = new StreamWriter(@"..\survey.txt", true))
        {
            writer.WriteLine("Scene/360 Video: " + videoName + "    \"" + questionManager.qText.text + "\"" + "    Survey Answer: " + ClickedButtonName); // TODO: add the video name to be saved
        }
        questionManager.nextQuestion();
    }
}
