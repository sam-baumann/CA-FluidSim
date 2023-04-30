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

    void OnMouseDown()
    {
        myFillBehavior.fillAmount = 1f;
        myFillBehavior.hasUpdated = true;
    }
}
