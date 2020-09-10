using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Bird
{
    public GameObject explosion;
    private bool doneexplode;
    private void OnCollisionEnter2D(Collision2D collision)
    {   
            if (!doneexplode)
            {
                doneexplode = true;
                // Destroy(gameObject);
                Instantiate(explosion, this.transform.position, Quaternion.identity).transform.parent = null;
            }
        
    }
}
