using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickBehavior : MonoBehaviour
{
    public SpriteFillBehavior myFillBehavior;
    public GridManagerBehavior gridManager;

    void Start()
    {
        myFillBehavior = GetComponentInChildren<SpriteFillBehavior>();
        gridManager = transform.parent.GetComponent<GridManagerBehavior>();
    }

    void OnMouseDown()
    {
        myFillBehavior.fillAmount = 1f;
        myFillBehavior.hasUpdated = true;
    }
}
