using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "map n+1"){
                SceneManager.LoadScene("Map 1");
            }
            else if(SceneManager.GetActiveScene().name == "Map 1"){
                SceneManager.LoadScene("Map 2");
            }
            else if(SceneManager.GetActiveScene().name == "Map 2"){
                Vector3 pos = new Vector3(-15, 43, 0);
                Instantiate(boss, pos, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
