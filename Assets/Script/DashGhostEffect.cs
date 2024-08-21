// using System.Collections;
// using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DashGhostEffect : MonoBehaviour
{
    [SerializeField] float effectDelay;
    [SerializeField] float effectDuration;
    [SerializeField] GameObject ghostEffect;
    [SerializeField] GameObject player;
    public bool isDash = false;
    private float effectDelaySec;
    private float effectDurationSec;
    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        effectDelaySec = effectDelay;
        effectDurationSec = effectDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDash)
        {
            StartCoroutine(effectLoop());
        }
    }

    IEnumerator effectLoop()
    {
        while (elapsedTime < effectDurationSec)
        {
            Debug.Log("dashing ghost effect................");
            if (effectDelaySec > 0)
            {
                effectDelaySec -= Time.deltaTime;
            }
            else
            {
                //generate effect
                GameObject currentGhost = Instantiate(ghostEffect, transform.position, transform.rotation);
                // Sprite currentSprite = player.GetComponent<SpriteRenderer>().sprite;
                // currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                effectDelaySec = effectDelay;
                Destroy(currentGhost, 1f);
            }
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime >= effectDurationSec)
        {
            Debug.Log("Stopping..............");
            elapsedTime = 0;
            StopCoroutine(effectLoop());
        }

    }
}
