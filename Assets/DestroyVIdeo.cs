using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyVIdeo : MonoBehaviour
{
    public int waitTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        DestroySelfCoroutine();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void DestroySelfCoroutine()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);

    }
}
