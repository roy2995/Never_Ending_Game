using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player_Behevaior_Test>();
        var slime = collision.collider.GetComponent<Slime>();
        if (player)
        {
            player.TakeHit(2);
        }
        else if (slime)
        {
            slime.TakeHit(2);
        }
        //Destroy(gameObject);
    }
}
