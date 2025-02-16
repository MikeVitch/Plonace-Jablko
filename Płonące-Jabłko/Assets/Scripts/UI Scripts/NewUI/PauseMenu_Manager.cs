using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_Manager : MonoBehaviour
{
    public KeyCode Pause;
    public KeyCode Inventory;
    public KeyCode Diary;
    public KeyCode Map; 
    public KeyCode Character;
    public GameObject PauseMenu;
    public GameObject InventoryPanel;
    public GameObject DiaryPanel;
    public GameObject MapPanel;
    public GameObject CharacterPanel;




    public void OnClosePanel(GameObject activePanel)
    {
        // close current panel
        activePanel.SetActive(false);
        Time.timeScale = 1;

    }
    public void OnPanelChange(GameObject openPanel)
    {
        Debug.Log("PanelChange");
        GameObject ActivePanel = GameObject.FindGameObjectWithTag("UIpanel");
       if(ActivePanel!=null) ActivePanel.SetActive(false);
        openPanel.SetActive(true);
    }

    public void OnMainMenu()
    {
        // save game state then ...

        SceneManager.LoadScene("StartingScene", LoadSceneMode.Single);
    }



    void Update()
    {
        // input
        if (Input.GetKeyDown(Pause))
        {
            if (!PauseMenu.activeSelf)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
           

            Debug.Log("Pause klicked");
        }
         if (Input.GetKeyDown(Inventory)) 
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            OnPanelChange(InventoryPanel);
        }  
         if (Input.GetKeyDown(Diary)) {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            OnPanelChange(DiaryPanel);
        }
        if (Input.GetKeyDown(Map)) {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            OnPanelChange(MapPanel);
        }
        if (Input.GetKeyDown(Character)) {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            OnPanelChange(CharacterPanel);
        }
        
    }
}
