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
    public GameObject[] foodPrefabs;
    public GameObject[] iconsPrefabs;

    public Transform animalSpawn;

    private bool gamePaused = false;
    private bool inventoryInUse = false;

    private bool animalTamed = false;
    private string animalTamedName;

    private GameObject currentPause;
    private GameObject currentPopUp;

    public string[] inventory = new string[36];
    public GameObject itemsSlots;
    public GameObject itemsSlotsHB;
    public GameObject hotBarItems;
    public GameObject hotBarCursor;
    public GameObject itemHolded;

    private int hBCursorPosition;

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

    public void UpdateItemHolded()
	{
        if (inventory[itemsSlots.transform.childCount + hBCursorPosition] == "")
		{
            itemHolded.GetComponent<CanvasGroup>().alpha = 0;
            return;
        }
        itemHolded.GetComponent<Image>().sprite = hotBarItems.transform.GetChild(hBCursorPosition).GetComponent<Image>().sprite;
        itemHolded.GetComponent<CanvasGroup>().alpha = 1;
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !inventoryInUse)
		{
            TogglePause();
        }
		if (Input.GetKeyDown(KeyCode.Tab) && !gamePaused)
		{
            ToggleInventory();
            if (inventoryInUse) UpdateUIInventory();
            else UpdateHotBar();
		}

        float scrollwheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollwheel != 0 && !gamePaused && !inventoryInUse)
		{
			if (scrollwheel < 0)
			{
                hBCursorPosition++;
                hBCursorPosition %= hotBarItems.transform.childCount;
            }
			else
			{
                hBCursorPosition--;
                if (hBCursorPosition < 0) hBCursorPosition = hotBarItems.transform.childCount - 1;
			}
            hotBarCursor.transform.position = hotBarItems.transform.GetChild(hBCursorPosition).transform.position;
            UpdateItemHolded();
		}
    }

    #region Menus and Scenes managment
    public void TogglePause()
    {
        gamePaused = !gamePaused;
        SetPause(gamePaused);
    }
    public void ToggleInventory()
    {
        inventoryInUse = !inventoryInUse;
        showInventory(inventoryInUse);
    }

    public void showInventory(bool p)
    {
        playerHUD.SetActive(!p);
        inventoryUI.SetActive(p);
        Cursor.lockState = p ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = p;
        Time.timeScale = p ? 0 : 1;
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

	#region PlayerHUD
    public GameObject InstantiateFood(string name)
	{
        foreach (GameObject gO in foodPrefabs)
        {
            if (name.Contains(gO.name))
            {
                if(currentPause == homePause)
				{
                    GameObject home = GameObject.Find("Home");
                    return Instantiate(gO, new Vector3(0,0,0), Quaternion.identity, home.transform);
                }
                else if(currentPause == wildForestPause)
				{
                    return Instantiate(gO, new Vector3(0, 0, 0), Quaternion.identity);
                }
                break;
            }
        }
        return null;
    }
    public bool canDropItem(out string name)
	{
        if (!gamePaused && !inventoryInUse && inventory[itemsSlots.transform.childCount + hBCursorPosition] != "")
        {
            name = inventory[itemsSlots.transform.childCount + hBCursorPosition];
            removeFromInventory(itemsSlots.transform.childCount + hBCursorPosition);
            UpdateHotBar();
            return true;
        }
        else
        {
            name = "";
            return false;
        }
    }
	public void setEKeyUIActionable(bool b)
	{
        Color color = playerHUD.transform.GetChild(5).GetComponent<Image>().color;
        color.a = (b) ? 1f : 0.2f;
        playerHUD.transform.GetChild(5).GetComponent<Image>().color = color;
    }

    public Sprite getItemIcon(string name)
	{
        Sprite s = null;

		foreach (GameObject gO in iconsPrefabs)
		{
			if (name.Contains(gO.name))
			{
                s = gO.GetComponent<Image>().sprite;
                break;
			}
		}

        return s;
	}

    public void UpdateHotBar()
	{
        for(int i = 0; i < itemsSlotsHB.transform.childCount; i++)
		{
            string name = inventory[itemsSlots.transform.childCount + i];
			if (name == "")
			{
                hotBarItems.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
            }
			else
			{
                hotBarItems.transform.GetChild(i).GetComponent<Image>().sprite = getItemIcon(name);
                hotBarItems.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
            }
		}
        UpdateItemHolded();
    }

	#endregion
	#region Inventory
	public void addToInventory(int index, string name)
    {
        inventory[index] = name;
        if (index >= itemsSlots.transform.childCount) UpdateHotBar();
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
                    icon.GetComponent<DragHandler>().wasInstantiatedAt(index);
				}
				else
				{
					icon.GetComponent<RectTransform>().anchoredPosition = itemsSlotsHB.transform.GetChild(index - itemsSlots.transform.childCount).GetComponent<RectTransform>().anchoredPosition;
                    icon.GetComponent<DragHandler>().wasInstantiatedAt(index);
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
