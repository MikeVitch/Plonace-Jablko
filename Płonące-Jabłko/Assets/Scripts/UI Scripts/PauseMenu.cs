using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject inventoryMenu;
    public static bool isPaused;

    void Start()
    {
        inventoryMenu.SetActive(false);
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        inventoryMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void CloseInventoryMenu()
    {
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}