using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCanvas : MonoBehaviour
{

    public static bool CanvasIsOpen = false;
    public GameObject drawMenuUI;
    public GameObject spawn;
    public GameObject theCanvas;
    public GameObject renderCamera;
    public Texture2D cursorTexture;

    void Update()
    {
        if (CanvasIsOpen)
        {
            Resume();
        }
    }

    void Resume()
    {
        drawMenuUI.SetActive(false);
        Time.timeScale = 1f;
        CanvasIsOpen = false;
    }

    public void Pause()
    {
        drawMenuUI.SetActive(true);
        theCanvas.transform.position = spawn.transform.position + (spawn.transform.forward * 10);
        //theCanvas.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

        var coordinates = spawn.transform.position + (spawn.transform.forward * 4);
        renderCamera.transform.position = new Vector3(coordinates.x, coordinates.y, renderCamera.transform.position.z);
        Time.timeScale = 0f; // can be set to higher if we want the game to keep on going
        Cursor.SetCursor(cursorTexture, new Vector2(0f, 75f), CursorMode.Auto);
    }

}
