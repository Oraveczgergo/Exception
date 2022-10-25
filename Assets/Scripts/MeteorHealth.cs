using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth : MonoBehaviour
{    
    public int hitPoints = 1;    

    public void TakeDamage(int x)
    {
        hitPoints -= x;
        if (hitPoints <= 0)
            Death();
    }
 
    public void Death()
    {
        gameObject.GetComponent<MeteorScript>().Deactivate();        
    }
}
