using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SpriteFillBehavior : MonoBehaviour
{
    [Range(0f, 1f)]
    public float fillAmount = 1f;

    // Update is called once per frame
    void Update()
    {
        //if fill amount is over 1, set it to 1
        float adjustedFillAmount = fillAmount;
        if (adjustedFillAmount > 1) adjustedFillAmount = 1;
        //update the sprite's transform to match the fill amount (and adust it up or down by that much)
        transform.localScale = new Vector3(1, adjustedFillAmount, 1);
        transform.localPosition = new Vector3(0, (adjustedFillAmount - 1) / 2, 0);
    }
}
