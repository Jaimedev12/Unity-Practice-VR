using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnInController : MonoBehaviour
{

    public GameObject chidori;
    public Vector3 spawnChidoriPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnChidoriPos = gameObject.transform.position;

        SpawnChidori();

    }

    void SpawnChidori()
    {
        Instantiate(chidori, spawnChidoriPos, gameObject.transform.rotation);
    }

}
