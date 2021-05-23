using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Instantiate(animals[0], spawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
