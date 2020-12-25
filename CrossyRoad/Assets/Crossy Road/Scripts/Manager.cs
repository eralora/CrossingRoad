using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Text coin = null; // Text in GUI tracking coin
    public Text distance = null; //tracking distance
    public Camera camera = null; // Using camera for movement or shaking
    public GameObject guiGameOver = null; // gui showing or not showing
    public LevelGenerator levelGenerator = null;
    public int levelCount = 50;

    private int currentCoins = 0; // current coin 
    private int currentDistance = 0; // current distance when player go through it will add 1
    private bool canPlay = false; // Manager check whetrher game can or can not play

    // Singleton patterns for tracking any kind of events
    private static Manager s_Instance;
    public static Manager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Manager)) as Manager;
            }

            return s_Instance;
        }
    }

    private void Start()
    {
        for (int i = 0; i < levelCount; i++)  // Level generator start up 
        {
            levelGenerator.RandomGenerator();
        }
    }

    public void UpdateCoinCount(int value)
    {
        Debug.Log("Player picked up another coin for" + value);

        currentCoins += value;

        coin.text = currentCoins.ToString();
    }

    public void UpdateDistanceCount()
    {
        Debug.Log("Player moved forward for one point");

        currentDistance += 1;

        distance.text = currentDistance.ToString();

        levelGenerator.RandomGenerator();  // generate new level piece here
    }

    public bool CanPlay() // if CanPlay method using so return will be canPlay
    {
        return canPlay;
    }

    public void StartPlay() // if StartPlay method so return CanPlay and all other scripts enable
    {
        canPlay = true;
    }
    public void GameOver() // CameraShake will be active and CameraFollow will be disable
    {
        camera.GetComponent<CameraShake>().Shake();
        camera.GetComponent<CameraFollow>().enabled = false;

        GuiGameOver();
    }

    void GuiGameOver() 
    {
        Debug.Log("Game Over!");

        guiGameOver.SetActive(true);
    }
    public void PlayAgain() // Play again the scene from SceneManager
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void Quit() // Quit from application
    {
        Application.Quit();
    }


}
