using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
TODO:
 - Add a struct to represent the state of each cell
 - In the update loop, update the state of each cell based on the state of the struct
 - Create compute shader to process the rules of the CA
*/

public class GridManagerBehavior : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject sceneCamera;

    public GameObject[,] gridArray;

    // Start is called before the first frame update
    void Start()
    {
        //get the camera component
        Camera camera = sceneCamera.GetComponent<Camera>();
        int height = 2 * (int)camera.orthographicSize;
        int width = (int)((float)height * camera.aspect);
        gridArray = new GameObject[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++)
            {
                float screenX = -width / 2 + x;
                float screenY = (float)(-height / 2 + y);
                if (height % 2 == 0) screenY += .5f;
                if (width % 2 == 0) screenX += .5f;
                GameObject cell = Instantiate(cellPrefab, transform);
                cell.transform.position = new Vector3(screenX, screenY, 0);
                cell.transform.localScale = new Vector3(.9f, .9f, 1);
                cell.name = "Cell " + x + ", " + y;
                gridArray[x, y] = cell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
