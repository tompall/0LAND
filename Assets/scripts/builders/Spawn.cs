using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    //objects to use to spawn
    public GameObject[] SpawnableObjects;

    //origins of spawns
    public Transform[] SpawnLocations;

    public float minDistance,maxDistance,minScale,maxScale,minObjects,maxObjects;

    //fallback if SpawnLocations is empty
    private float originX = 50f,originZ = 50f;
    private float tempScale;

    private static Vector3 pos, hitpos;

    float[] rotations = { 0f,90f,180f,270f};

    Quaternion tempRotation;

    public void Start()
    {
        //fallback for min maxDistance isnt set from editor
        if(minDistance == 0) { minDistance = 30f; }
        if(maxDistance == 0) { maxDistance = 30f; }
        if(minObjects == 0) { minObjects = 1; }
        if(maxObjects == 0) { maxObjects = 2; }

        Scatter();
    }

   private void Scatter()
    {
        if (SpawnLocations.Length > 0)
        {
            foreach (Transform t in SpawnLocations)
            {
                originX = t.position.x;
                originZ = t.position.z;
                SpawnAll(originX, originZ,t);
            }
        }
        else
        {
            SpawnAll(originX, originZ,this.gameObject.transform);
        }
    }

    private void SpawnAll(float x,float z, Transform parent)
    {
        GameObject SpawnParent = new GameObject();
        SpawnParent.transform.parent = parent;
        SpawnParent.name = "debris";

        for (int i = 0; i < Random.Range(minObjects,maxObjects); i++)
        {
            GameObject obj = GameObject.Instantiate(SpawnableObjects[Random.Range(0,SpawnableObjects.Length)], Vector3.zero, Quaternion.identity);

            obj.transform.SetParent(SpawnParent.transform);

            obj.transform.Rotate(new Vector3(Random.Range(0, 90), rotations[Random.Range(0, rotations.Length)], Random.Range(0, 90)));

            obj.transform.position = RaycastLocation(x + (Random.insideUnitCircle*Random.Range(minDistance,maxDistance)).x, z + (Random.insideUnitCircle *Random.Range(minDistance, maxDistance)).y, 1000f);

            tempScale = Random.Range(minScale, maxScale);

            obj.transform.localScale = new Vector3(obj.transform.localScale.x * tempScale, obj.transform.localScale.y * tempScale, obj.transform.localScale.z * tempScale);
        }
    }

    private static Vector3 RaycastLocation(float rX,float rZ,float rayStartY)
    {
        pos = new Vector3(rX,rayStartY, rZ);
        hitpos = Vector3.zero;
        RaycastHit hit;
        if(Physics.Raycast(pos,-Vector3.up,out hit))
        {
            hitpos = hit.point;
            Debug.DrawLine(pos, hitpos, Color.red, 30f);
        }
        else
        {
            hitpos = RaycastLocation(Random.Range(rX, -rX), Random.Range(rZ, -rZ), 1000f);
        }

        pos = hitpos;
        return pos;
    }    
}
