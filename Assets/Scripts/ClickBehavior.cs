using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickBehavior : MonoBehaviour
{
    public SpriteFillBehavior myFillBehavior;
    int rightClickTimer;

    void Start()
    {
        myFillBehavior = GetComponentInChildren<SpriteFillBehavior>();
        rightClickTimer = 0;
    }

    void Update(){
        if (rightClickTimer < 120){
            rightClickTimer++;
        }
    }

    void OnMouseOver()
    {
       
        if (Input.GetMouseButton(0)){
            myFillBehavior.fillAmount = 1f;
            myFillBehavior.hasUpdated = true;
        } else if (Input.GetMouseButton(1) && rightClickTimer > 60){
            rightClickTimer = 0;
            if (myFillBehavior.fillAmount == 10f){
                myFillBehavior.fillAmount = 0f;
            } else {
                myFillBehavior.fillAmount = 10f;
            }
            myFillBehavior.hasUpdated = true;
        }
    }
}
