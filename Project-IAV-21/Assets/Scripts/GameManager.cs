using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject homePause;
    public GameObject wildForestPause;
    public GameObject wildForestPopUp;
    public GameObject home;

    public GameObject[] animalsPrefabs;

    public Transform animalSpawn;

    private bool gamePaused = false;

    private bool animalTamed = false;
    private string animalTamedName;

    private GameObject currentPause;
    private GameObject currentPopUp;

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
        currentPopUp = wildForestPopUp;
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
        animalTamed = false;
        home.SetActive(false);
        SceneManager.LoadScene("WildForest", LoadSceneMode.Additive);
        TogglePause();
        currentPause = wildForestPause;

        HidePopUp();
	}

    public void GoToHome()
    {
        SceneManager.UnloadSceneAsync("WildForest");
        home.SetActive(true);
        TogglePause();
        currentPause = homePause;

        if (animalTamed) InstantiateAnimal();
        animalTamed = false;

        HidePopUp();
    }

    public void ShowPopUp()
    {
        if (!gamePaused) TogglePause();
        currentPause.SetActive(false);
        currentPopUp.SetActive(true);
    }

    public void HidePopUp()
    {
        if (gamePaused) TogglePause();
        currentPopUp.SetActive(false);
    }

    public void SetAnimalTamed(string name)
    {
        animalTamed = true;
        animalTamedName = name;
    }

    public void InstantiateAnimal()
    {
        foreach (GameObject pf in animalsPrefabs)
        {
            if (animalTamedName.Contains(pf.name))
            {
                Instantiate(pf, home.transform).transform.position = animalSpawn.position;
                break;
            }
        }
    }
}
