using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    int startScene = 1;
   // int savedScene = 2;
    public GameState gameState;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject controlsPanel;



    /* public Texture2D cursorTexture;
     public CursorMode cursorMode = CursorMode.Auto;
     public Vector2 hotSpot = Vector2.zero;*/

    public void OnNewGame()
    {
        // prompt window to override or start on new save

        Time.timeScale = 1;
        SceneManager.LoadScene(startScene);
    }
    public void OnStartGame()
    {
        // starts from last save
        SceneManager.LoadScene(gameState.currentScene);

    }
    public void OnLoadGame()
    {
        // open choice window

    }
    public void OnOptions()
    {
        // open options window
       // Debug.Log("Show Options...");
        optionsPanel.SetActive(true);

    }

    public void OnControls()
    {
        // open controls window
       // Debug.Log("Show Controls...");
        controlsPanel.SetActive(true);

    }
    public void OnCredits()
    {
        // open credits window
        //Debug.Log("Show Credits...");
        creditsPanel.SetActive(true);


    }
    public void OnQuit()
    {
        // quit game - works only on build
        //Debug.Log("Quitting...");
        Application.Quit();
    }

    public void OnClosePanel(GameObject activePanel)
    {
       // close current panel
       activePanel.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
      /*  Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);*/
    }

   
}
