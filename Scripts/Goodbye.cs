using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goodbye : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayLittle());
    }

    IEnumerator DelayLittle()
    {
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
