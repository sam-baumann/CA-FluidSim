using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
TODO:
 - Add a struct to represent the state of each cell
 - In the update loop, update the state of each cell based on the state of the struct
 - Create compute shader to process the rules of the CA
*/

public struct Cell {
    public float previousState;
    public float currentState;
}

public class GridManagerBehavior : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject sceneCamera;

    public GameObject[,] gridArray;
    public Vector2Int gridSize;

    public Cell[] cellArray;

    public ComputeShader computeShader;

    [SerializeField]
    public int frameInterval = 1;

    [SerializeField]
    public int cellCountMultiplier = 1;

    int kernel;
    ComputeBuffer cellBuffer;
    int frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        //get the camera component
        Camera camera = sceneCamera.GetComponent<Camera>();
        int height = 2 * (int)camera.orthographicSize * cellCountMultiplier;
        int width = (int)((float)height * camera.aspect);
        gridArray = new GameObject[width, height];
        cellArray = new Cell[width * height];
        gridSize = new Vector2Int(width, height);

        frameCounter = 0;

        //create the compute shader and buffer
        cellBuffer = new ComputeBuffer(cellArray.Length, sizeof(float) * 2);
        cellBuffer.SetData(cellArray);
        computeShader.SetBuffer(0, "cells", cellBuffer);
        computeShader.SetInt("width", width);
        computeShader.SetInt("height", height);
        kernel = computeShader.FindKernel("AutomataProcessor");

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++)
            {
                float screenX = (-width / 2 + x) / (float)cellCountMultiplier;
                float screenY = (float)(-height / 2 + y) / (float)cellCountMultiplier;
                if (height % 2 == 0) screenY += (.5f / (float)cellCountMultiplier);
                if (width % 2 == 0) screenX += (.5f / (float)cellCountMultiplier);
                GameObject cell = Instantiate(cellPrefab, transform);
                cell.transform.position = new Vector3(screenX, screenY, 0);
                cell.transform.localScale = new Vector3(1f / (float)cellCountMultiplier, 1f / (float)cellCountMultiplier, 1);
                SpriteFillBehavior myFillBehavior = cell.GetComponentInChildren<SpriteFillBehavior>();
                myFillBehavior.cellPosition = new Vector2Int(x, y);
                cell.name = "Cell " + x + ", " + y;

                gridArray[x, y] = cell;

                //create a new cell struct
                Cell newCell = new Cell();
                //set the previous state to 0
                newCell.previousState = 1;
                //set the current state to 0
                newCell.currentState = 0;
                //add that cell to the cell array
                cellArray[x + y * width] = newCell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        frameCounter %= frameInterval;
        if (frameCounter != 0) return;
        //move the current value to be the previous value
        for (int i = 0; i < cellArray.Length; i++)
        {
            cellArray[i].previousState = cellArray[i].currentState;
        }
        //run the compute shader
        cellBuffer.SetData(cellArray);
        computeShader.Dispatch(kernel, ((gridSize.x * gridSize.y) + 63) / 64, 1, 1);
        cellBuffer.GetData(cellArray);
                
        //loop over all cells and draw them
        foreach (GameObject cell in gridArray)
        {
            SpriteFillBehavior myFillBehavior = cell.GetComponentInChildren<SpriteFillBehavior>();
            //handle the case where the cell has been clicked
            if (myFillBehavior.hasUpdated){
                myFillBehavior.hasUpdated = false;
                cellArray[myFillBehavior.cellPosition.x + myFillBehavior.cellPosition.y * gridSize.x].currentState = myFillBehavior.fillAmount;
                cellArray[myFillBehavior.cellPosition.x + myFillBehavior.cellPosition.y * gridSize.x].previousState = myFillBehavior.fillAmount;
            }
            myFillBehavior.Draw();
        }
    }

    void OnDestroy()
    {
        cellBuffer.Release();
    }
}
