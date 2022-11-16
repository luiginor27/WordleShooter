using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PauseMenu : MonoBehaviour
{
    public KeyCode pauseKey;

    public GameObject background;
    public GameObject main;
    public GameObject controls;

    private List<GameObject> _menus;

    private void Start()
    {
        _menus = new List<GameObject>();
    }


    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            ShowPreviousMenu();
        }
    }

    private void ShowPreviousMenu()
    {
        switch (_menus.Count)
        {
            case 0:
                main.SetActive(true);
                _menus.Add(main);
                background.SetActive(true);
                GameManager.Instance.PlayPauseGame(false);
                break;

            case 1:
                main.SetActive(false);
                _menus.Remove(main);
                background.SetActive(false);
                GameManager.Instance.PlayPauseGame(true);
                break;

            default:
                GameObject menu = _menus[_menus.Count - 1];
                menu.SetActive(false);
                _menus.Remove(menu);

                GameObject previousMenu = _menus[_menus.Count - 1];
                previousMenu.SetActive(true);
                break;
        }
    }

    private void ShowNextMenu(GameObject menu)
    {
        _menus[_menus.Count - 1].SetActive(false);

        _menus.Add(menu);
        menu.SetActive(true);
    }

    public void OnReturnClick()
    {
        ShowPreviousMenu();
    }

    public void OnControlsClick()
    {
        ShowNextMenu(controls);
    }

    public void OnOpenAmmoFileClick()
    {
        string path = GameManager.AMMO_FILE_PATH;

        Process foo = new Process();
        foo.StartInfo.FileName = "Notepad.exe";
        foo.StartInfo.Arguments = path;
        foo.Start();
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

}
