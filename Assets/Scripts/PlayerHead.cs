using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////<SUMMARY>///////
//PlayerHead contains important data about the players current state,
//-so that other scripts can quickly make changes to the player's HP/other stats. 
public class PlayerHead : MonoBehaviour
{
    [SerializeField]private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            TakeDamage(value);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
    public void TakeDamage(int incomingDamage)
    {
        hp -= incomingDamage;
    }
}
