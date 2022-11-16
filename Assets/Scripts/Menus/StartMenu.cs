using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    public KeyCode startKey;
    public GameObject entranceDoor;

    private float _yPositionOpen = 15f;

    void Update()
    {
        if (Input.GetKeyDown(startKey)) {
            GameManager.Instance.StartGame();
            gameObject.SetActive(false);
            entranceDoor.transform.DOMoveY(_yPositionOpen, 2f);
        }    
    }
}
