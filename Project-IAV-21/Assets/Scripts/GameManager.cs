using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject home;
    public GameObject fox;
    public GameObject chicken;
    public struct AnimalInfo
	{
        string name;
        Transform pos;
        int life;
	}

    private bool gamePaused = false;

    private List<AnimalInfo> animals;

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
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            TogglePause();
        }
    }

	public void AddAnimal(AnimalInfo aInfo)
	{
        animals.Add(aInfo);
	}

    public void ChangeScene(string name)
	{
        SceneManager.LoadSceneAsync(name);
    }

    public void TogglePause()
	{
        gamePaused = !gamePaused;
        SetPause(gamePaused);
    }

    public void SetPause(bool p)
	{
        GameObject.FindGameObjectWithTag("UI").transform.Find("PauseMenu").gameObject.SetActive(p);
        Cursor.lockState = p ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = p;
        Time.timeScale = p ? 0 : 1;
    }

	public void Exit()
	{
        Application.Quit();
	}
}
