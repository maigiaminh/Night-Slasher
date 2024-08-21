using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] int poolSize;
    private GameObject[] pool;
    
    private void Awake()
    {
        PopulatePool();
    }


    private void PopulatePool(){
        pool = new GameObject[poolSize];
        
        for(int i = 0; i < pool.Length; i++){
            pool[i] = Instantiate(enemyBulletPrefab, transform);
            pool[i].transform.parent = transform;   
            pool[i].SetActive(false);
        }
    }

    public GameObject FindActiveBullet(){
        for(int i = 0; i < pool.Length; i++){
            if(!pool[i].activeInHierarchy){
                return pool[i];
            }
        }

        return null;
    }
}
