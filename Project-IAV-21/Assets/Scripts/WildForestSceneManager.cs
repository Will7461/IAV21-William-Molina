using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WildForestSceneManager : MonoBehaviour
{

    public GameObject[] animals;

    public Transform spawnPoint;

    private GameObject randomAnimal;

    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0,animals.Length);
        randomAnimal = animals[randomIndex];

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("WildForest"));
        Instantiate(randomAnimal, spawnPoint.position, Quaternion.identity);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    }

}