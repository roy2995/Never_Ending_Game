using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behevaior_Test : MonoBehaviour
{
    private float healthPoints;
    [Range(10, 500)] public float maxHealthPoints;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeHit(float damage)
    {
        healthPoints -= damage;
        Debug.Log("Player Health: " + healthPoints + " minus -" + damage);
        if (healthPoints == 0)
        {
            Destroy(gameObject);
        }


    }
}
