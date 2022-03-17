using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    [Range(10, 50)] public float bulletSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveDir * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            
            var player = collision.GetComponent<Player_Behevaior_Test>();
            player.TakeHit(2);
            //var slime = collision.GetComponent<Slime>();
            //slime.TakeHit(2);

        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var player = collision.collider.GetComponent<Player_Behevaior_Test>();
    //    var slime = collision.collider.GetComponent<Slime>();
    //    if (player)
    //    {
    //        player.TakeHit(2);
    //    }
    //    else if (slime)
    //    {
    //        slime.TakeHit(2);
    //    }
    //    Destroy(gameObject);
    //}
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
