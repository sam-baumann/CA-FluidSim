using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SpriteFillBehavior : MonoBehaviour
{
    [Range(0f, 1f)]
    public float fillAmount = 1f;

    public Vector2Int cellPosition;

    public GridManagerBehavior gridManager;

    public bool hasUpdated;

    // Start is called before the first frame update
    void Start()
    {
        //get the grid manager
        gridManager = transform.parent.parent.GetComponent<GridManagerBehavior>();
    }

    public void Draw()
    {
        //get the fill amount from the parent's Cells array
        fillAmount = gridManager.cellArray[cellPosition.x + cellPosition.y * gridManager.gridSize.x].currentState;
        //if fill amount is over 1, set it to 1
        float adjustedFillAmount = fillAmount;
        if (adjustedFillAmount > 1) adjustedFillAmount = 1;
        //now check the cell above. If it has any content, adjust the fill amount to 1
        if (cellPosition.y < gridManager.gridSize.y - 1 && adjustedFillAmount < 10 && adjustedFillAmount > 0)
        {
            float aboveState = gridManager.cellArray[cellPosition.x + (cellPosition.y + 1) * gridManager.gridSize.x].currentState;
            if (aboveState > 0 && aboveState < 10)
            {
                adjustedFillAmount = 1;
            }
        }
        //update the sprite's transform to match the fill amount (and adust it up or down by that much)
        transform.localScale = new Vector3(1, adjustedFillAmount, 1);
        transform.localPosition = new Vector3(0, (adjustedFillAmount - 1) / 2, 0);

        //now update the color
        //first get the sprite renderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //now if it's less than or equal to 1, set it to our blue color
        if (fillAmount <= 1)
        {
            spriteRenderer.color = new Color32(0, 162, 255, 255);
        } else if (fillAmount <= 5){
            //lerp between our previous blue and a darker blue
            spriteRenderer.color = Color32.Lerp(new Color32(0, 162, 255, 255), new Color32(0, 0, 255, 255), (fillAmount - 1) / 4);
        } else if (fillAmount <= 10){
            spriteRenderer.color = new Color(0, 0, 0);
        }
    }
}
