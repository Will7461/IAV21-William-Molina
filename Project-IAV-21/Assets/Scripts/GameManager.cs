using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject homePause;
    public GameObject wildForestPause;
    public GameObject home;
    public GameObject[] animals;

    private bool gamePaused = false;

    private GameObject currentPause;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

	private void Start()
	{
        Cursor.visible = false;
        currentPause = homePause;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            TogglePause();
        }
    }

    public void TogglePause()
	{
        gamePaused = !gamePaused;
        SetPause(gamePaused);
    }

    public void SetPause(bool p)
	{
        currentPause.SetActive(p);
        Cursor.lockState = p ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = p;
        Time.timeScale = p ? 0 : 1;
    }

	public void Exit()
	{
        Application.Quit();
	}

    public void GoToWildForest()
	{
        home.SetActive(false);
        SceneManager.LoadScene("WildForest", LoadSceneMode.Additive);
        TogglePause();
        currentPause = wildForestPause;
	}

    public void GoToHome()
    {
        SceneManager.UnloadSceneAsync("WildForest");
        home.SetActive(true);
        TogglePause();
        currentPause = homePause;
    }
}
