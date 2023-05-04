using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
public List<QuestionsAndAnswers> QnA; 
public GameObject[] options;
public int currentQuestion = 0; //which question is being presented 
public TextMeshProUGUI qText; //text for questions to be displayed


private void Start()
{
    qText.text = QnA[currentQuestion].Question; // Set the first question
}

public void nextQuestion(){
    currentQuestion += 1; // increment question counter

    if (currentQuestion < QnA.Count){ // if there are still more questions to ask then load next question
        qText.text = QnA[currentQuestion].Question;
    }
    else{ // otherwise advance to the next scene

        File.Delete(@"..\videoName.txt"); // TODO: Delete temp video name file. TODO: Delete this whole comment after successful tests
        new RandomizeScene().LoadRandomScene();
    } 
}

}
