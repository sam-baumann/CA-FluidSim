using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBehavior : MonoBehaviour
{
    bool pressed;
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!pressed && Input.GetKeyDown(KeyCode.Escape))
        {
            pressed = true;
            timer = 0;
        } else if (pressed && Input.GetKey(KeyCode.Escape))
        {
            timer++;
            if (timer > 60)
            {
                Application.Quit();
            }
        } else if (pressed && Input.GetKeyUp(KeyCode.Escape))
        {
            //reload the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            pressed = false;
            timer = 0;
        }
    }
}
