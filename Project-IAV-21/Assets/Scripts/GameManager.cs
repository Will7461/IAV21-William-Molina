using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject homePause;
    public GameObject wildForestPause;
    public GameObject wildForestPopUp;
    public GameObject home;
    public GameObject itemIcons;
    public GameObject playerHUD;
    public GameObject inventoryUI;

    public GameObject[] animalsPrefabs;
    public GameObject[] iconsPrefabs;

    public Transform animalSpawn;

    private bool gamePaused = false;

    private bool animalTamed = false;
    private string animalTamedName;

    private GameObject currentPause;
    private GameObject currentPopUp;

    public string[] inventory = new string[36];
    public GameObject itemsSlots;
    public GameObject itemsSlotsHB;

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
		if (Input.GetKeyDown(KeyCode.Tab))
		{
            inventoryUI.SetActive(true);
            UpdateUIInventory();
		}
    }

    #region Menus and Scenes managment
    public void TogglePause()
    {
        gamePaused = !gamePaused;
        SetPause(gamePaused);
    }

    public void SetPause(bool p)
    {
        playerHUD.SetActive(!p);
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
    #endregion

    public void setEKeyUIActionable(bool b)
	{
        Color color = playerHUD.transform.GetChild(1).GetComponent<Image>().color;
        color.a = (b) ? 1f : 0.2f;
        playerHUD.transform.GetChild(1).GetComponent<Image>().color = color;
    }
    #region Inventory
    public void addToInventory(int index, string name)
    {
        inventory[index] = name;
    }

    public void removeFromInventory(int index)
    {
        inventory[index] = "";
    }
    public int freeInventorySlot()
    {
        int i = -1;
        for (int x = 0; x < inventory.Length; x++)
        {
            if (inventory[x] == "")
            {
                i = x;
                break;
            }
        }
        return i;
    }

    public void InstantiateIcon(string name, int index)
    {
        foreach (GameObject pf in iconsPrefabs)
        {
            if (name.Contains(pf.name))
            {
                GameObject icon = Instantiate(pf, itemIcons.transform);
                if(index < itemsSlots.transform.childCount)
				{
                    icon.GetComponent<RectTransform>().anchoredPosition = itemsSlots.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition;
				}
				else
				{
                    icon.GetComponent<RectTransform>().anchoredPosition = itemsSlotsHB.transform.GetChild(index - itemsSlots.transform.childCount).GetComponent<RectTransform>().anchoredPosition;
                }
                break;
            }
        }
    }

    public void UpdateUIInventory()
    {
        foreach (Transform child in itemIcons.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < inventory.Length; i++)
        {
			if (inventory[i] != "")
			{
                InstantiateIcon(inventory[i], i);
			}
        }
    }
    #endregion
}
