using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickBehavior : MonoBehaviour
{
    public SpriteFillBehavior myFillBehavior;

    void Start()
    {
        myFillBehavior = GetComponentInChildren<SpriteFillBehavior>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)){
            myFillBehavior.fillAmount = 1f;
            myFillBehavior.hasUpdated = true;
        } else if (Input.GetMouseButtonDown(1)){
            if (myFillBehavior.fillAmount == 10f){
                myFillBehavior.fillAmount = 0f;
            } else {
                myFillBehavior.fillAmount = 10f;
            }
            myFillBehavior.hasUpdated = true;
        }
    }
}
