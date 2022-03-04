using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public float dieTime;
    //public float damage;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownToDeath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Death();
    }

    IEnumerator CountDownToDeath()
    {
        yield return new WaitForSeconds(dieTime);

        Death();
    }

    void Death()
    {
        Destroy (gameObject);
    }
}
