using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawn : MonoBehaviour
{
    public int maxNum = 5;
    public int currentNum = 0;
    public float time = 3;
    private float timer = 0;
    public GameObject prefab;

    void Update()
    {
        if (currentNum < maxNum)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-5, 5);
                pos.z += Random.Range(-5, 5);
               GameObject go= GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
                go.GetComponent<WolfBaby>().wolfSpawn = this;
                timer = 0;
                currentNum++;
            }
        }
    }
    public void MinusNumber()
    {
        currentNum--;
    }
}
