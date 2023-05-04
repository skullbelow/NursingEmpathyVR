using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetButtonNull : MonoBehaviour
{

    public void DeselectClickedButton(GameObject button)
    {
        if (EventSystem.current.currentSelectedGameObject == button)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    
}
