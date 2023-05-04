using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizeScene : MonoBehaviour
{
    private string sceneFilePath = @"..\scenes.txt";
    private string surveyFilePath = @"..\survey.txt";
    private string sensorDataPath = @"..\SensorData.csv";
    
    public void LoadIntroScene(){
        
        // Clean up/delete all temporary files and data csv for new user session if they have not been deleted prior
        File.Delete(sceneFilePath); 
        File.Delete(surveyFilePath);
        File.Delete(sensorDataPath);
        File.Delete(@"..\videoName.txt");

        SceneManager.LoadScene(1); //in build settings the intro scene is 1
        // ^^^ WILL NEED TO CHANGE IF BUILD SETTINGS CHANGE
    }

    public void LoadRandomScene()
    {
        if(!File.Exists(sceneFilePath) && !File.Exists(surveyFilePath)){ // Load first random scene
            List<string> availableScenes = new List<string> {"3","4","5","6"}; //hardcoding scene list
            // !!!  KEEP IN MIND THIS  ^^^  WOULD NEED TO UPDATED EVERYTIME YOU WANTED TO ADD A SCENE TO THE RANDOM LIST  !!!
            
            System.Random rnd = new System.Random(); // Make sys.random object (look into Latin Squares further)
            int index = rnd.Next(0, availableScenes.Count); // Obtain scene index randomly from all options
            int sceneNum = Int32.Parse(availableScenes[index]); // Obtain scene number
            MakeVideoNameFile(sceneNum); // Write scene video file to temp file for data marking/labeling
            availableScenes.RemoveAt(index); // Remove scene number from list
            File.WriteAllLines(sceneFilePath,availableScenes); // Write remaining scene options to temp scene file
            SceneManager.LoadScene(sceneNum); // Load the first random scene
        }
        else if (!File.Exists(sceneFilePath) && File.Exists(surveyFilePath)){ // Load goodbye scene

            while (!IsFileReady(surveyFilePath)) { } // Wait for survey file to be avalible before proceeding
            //while (!IsFileReady(sensorDataPath)) { } // Did not resolve issue

            List<string> surveyResponses = new List<string>(File.ReadAllLines(surveyFilePath)); // Read survey responses
            File.Delete(surveyFilePath); // Delete temp survey file

            TextWriter tw = new StreamWriter(sensorDataPath,true);

            tw.WriteLine(); // Added for spacing
            tw.WriteLine(); // Added for spacing
            tw.WriteLine(); // Added for spacing

            for(int i = 0; i < surveyResponses.Count; i++){
                tw.WriteLine($"Likert Response #{i+1}   " + surveyResponses[i]);
            }

            tw.WriteLine(); // Added for spacing
            tw.WriteLine(); // Added for spacing
            tw.WriteLine(); // Added for spacing

            tw.Close();

            SceneManager.LoadScene(7);// loading goodbye scene
            // !!!  KEEP IN MIND THIS  ^^^  WOULD NEED TO UPDATED EVERYTIME YOU WANTED TO ADD A SCENE TO THE RANDOM LIST  !!!
        }
        else{ // This is not the first scene...
            List<string> availableScenes = new List<string>(File.ReadAllLines(sceneFilePath)); // Load file contents in list

            if(availableScenes.Count == 1){ // Load last random scene
                File.Delete(sceneFilePath); // Delete temp file
                MakeVideoNameFile(Int32.Parse(availableScenes[0])); // Write scene video file to temp file for data marking/labeling
                SceneManager.LoadScene(Int32.Parse(availableScenes[0])); // Load last scene 
            }
            else{ // Load a random scene
                System.Random rnd = new System.Random(); // Make sys.random object(look into Latin Squares further)
                int index = rnd.Next(0, availableScenes.Count); // Obtain scene index randomly from whatever options left
                int sceneNum = Int32.Parse(availableScenes[index]); // Obtain scene number
                MakeVideoNameFile(sceneNum); // Write scene video file to temp file for data marking/labeling
                availableScenes.RemoveAt(index); // Remove scene number from list
                File.WriteAllLines(sceneFilePath,availableScenes); // Update scene file
                SceneManager.LoadScene(sceneNum); // Load the random scene
            }
        }
    }


    // Helper method for waiting for temp files to be avaliable.
    // Obtained from: https://stackoverflow.com/questions/1406808/wait-for-file-to-be-freed-by-process
    public static bool IsFileReady(string filename)
    {
        // If the file can be opened for exclusive access it means that the file
        // is no longer locked by another process.
        try
        {
            using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                return inputStream.Length > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    
    // Creates a temporary file containing the video played prior to a likert survey scene session.
    // PRO: This is a relatively simple approach to more finely mark/label our user data. New scene/video names
    //      can be added relatively easily in the switch statement below.
    // CON: You will have to update the switch statement below everytime you add or remove a 360 video scene, otherwise the
    //      resulting final data may not be marked/labeled correctly. 
    public static void MakeVideoNameFile(int videoIndex){

        string videoName;

        switch(videoIndex) 
            {
            case 3:
                videoName = "Say a Prayer";
                break;
            case 4:
                videoName = "No Pain";
                break;
            case 5:
                videoName = "Junior";
                break;
            case 6:
                videoName = "Hands Cold";
                break;
            default:
                videoName = "Invalid video name. If you altered the scenes in build for any reason (such as adding or removing a scene), you will need to update MakeVideoNameFile() in RandomizeScene.cs";
                break;
            }


        using (StreamWriter writer = new StreamWriter(@"..\videoName.txt",false)) // Will over-write, but should be creating new file each time
        {
            writer.WriteLine(videoName);
        }
        
    }


}
