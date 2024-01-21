using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject hUDcanvas;
    public static bool isPaused;

    void Start()
    {
        inventoryMenu.SetActive(false);
        isPaused = false;
        hUDcanvas.SetActive(true);
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
        hUDcanvas.SetActive(false);
        isPaused = true;
    }

    public void ResumeGame()
    {
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        hUDcanvas.SetActive(true);
        isPaused = false;
    }

    public void CloseInventoryMenu()
    {
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        hUDcanvas.SetActive(true);
        isPaused = false;
    }
}