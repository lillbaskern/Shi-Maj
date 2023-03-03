using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////<SUMMARY>///////
//PlayerHead contains important data about the players current state,
//-so that other scripts can quickly make changes to the player's HP/other stats. 
public class PlayerHead : MonoBehaviour
{

    //the viewmodel's camera's GameObject
    GameObject _uiCamera;

    InputHandler _input;

    PlayerShoot _shooter;
    [SerializeField] private int hp;
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
        _uiCamera = GameObject.Find("UICamera");
        _shooter = gameObject.AddComponent<PlayerShoot>();
    }

    void Update()
    {



    }

    private void ManageViewmodel()
    {

    }

    void TakeDamage(int incomingDamage)
    {
        hp -= incomingDamage;
        Debug.Log(hp);
    }
}
