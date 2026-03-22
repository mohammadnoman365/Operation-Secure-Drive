using UnityEngine;
using System.Collections;

/**
 *	Rapidly enables/disables an object (e.g., muzzle flash).
 *	
 *	(c) 2015, Jean Moreno (Modified)
**/

public class WFX_FlashFlicker : MonoBehaviour
{
    public float time = 0.05f; // Flicker duration
    private float timer;
    private bool isFlickering = false;

    void Start()
    {
        timer = time;
    }

    public void StartFlicker()
    {
        if (!isFlickering)
            StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        isFlickering = true;
        gameObject.SetActive(true);

        do
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        while (timer > 0);

        gameObject.SetActive(false);
        timer = time;
        isFlickering = false;
    }
}
