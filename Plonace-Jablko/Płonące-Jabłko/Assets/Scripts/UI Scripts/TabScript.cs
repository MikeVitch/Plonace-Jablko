using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabScript : MonoBehaviour
{
    public GameObject[] tabs;
    public PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void TurnOnTabs(int tab)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }
        tabs[tab - 1].SetActive(true);
    }
}
