using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public const string AMMO_FILE_PATH = "Ammo.txt";

    public Transform player;
    public Transform startPosition;

    public PauseMenu pauseMenu;

    private Transform _lastCheckpoint;

    private LevelManager _currentLevelManager;

    private MovementController _playerMovement;
    private ShooterController _playerShooter;
    private MouseLook _cameraLook;

    void Start()
    {
        Instance = this;
        SetCheckpoint(startPosition);
        ResetGame();

        _playerMovement = player.gameObject.GetComponent<MovementController>();
        _playerShooter = player.gameObject.GetComponent<ShooterController>();
        _cameraLook = Camera.main.gameObject.GetComponent<MouseLook>();
    }

    public void StartGame()
    {
        PlayPauseGame(true);
        EnablePauseMenu();
        CreateAmmoFile();
    }

    public void StartLevel()
    {
        _currentLevelManager.enemySpawner.enabled = true;
        _currentLevelManager.lightsController.TurnLightsOn(true);
    }

    public void ResetGame()
    {
        player.position = _lastCheckpoint.position;

        if (_currentLevelManager != null)
        {
            _currentLevelManager.lightsController.TurnLightsOn(false);
            _currentLevelManager.enemySpawner.ResetState();
        }
    }

    public void EndLevel()
    {
        _currentLevelManager.lightsController.EndLevel();
        _currentLevelManager.puzzle.ShowSentence();
    }

    public void SetCheckpoint(Transform position)
    {
        _lastCheckpoint = position;
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        _currentLevelManager = levelManager;
    }

    public void PlayPauseGame(bool play)
    {
        _playerMovement.enabled = play;
        _playerShooter.enabled = play;
        _cameraLook.enabled = play;
        if(_currentLevelManager != null) _currentLevelManager.enemySpawner.enabled = play;
    }

    private void EnablePauseMenu()
    {
        pauseMenu.enabled = true;
    }

    private void CreateAmmoFile()
    {
        FileManager.CreateFile(AMMO_FILE_PATH);
    }
}
