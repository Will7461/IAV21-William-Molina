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
        int randomDegree = Random.Range(0, 271);
        randomAnimal = animals[randomIndex];

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("WildForest"));
        Instantiate(randomAnimal, spawnPoint.position, Quaternion.identity);
        GameObject.Find("Player").transform.Rotate(new Vector3(0, randomDegree, 0));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    }

}