using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    public List<explorerData> explorerDB = new List<explorerData>();
    public List<GameObject> explorers = new List<GameObject>();

    private void Start()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("explorer"))
        {
            explorers.Add(o);
            explorerDB.Add(o.GetComponent<explorerData>());
        }
    }
}
