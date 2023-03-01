using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////<SUMMARY>///////
//PlayerHead contains important data about the players current state,
//-so that other scripts can quickly make changes to the player's HP/other stats. 
public class PlayerHead : MonoBehaviour
{

    //bools for managing weapon viewmodel
    //could quickly become deprecated
    bool _lastFrameWasUnarmed;
    bool _unarmed;
    //the viewmodel's camera's GameObject
    GameObject _uiCamera;

    IPlayerCharacter[] characters = new IPlayerCharacter[2];
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
        //previously, we set a bunch of bools pertaining to the viewmodel in the beginning of update
        //therefore, this method must be called first (i want to keep the logic on a per-frame basis)
        ManageViewmodel();



    }

    private void ManageViewmodel()
    {
        _lastFrameWasUnarmed = _unarmed;
        _unarmed = _shooter.Weapon == null;

        if (_unarmed && !_lastFrameWasUnarmed) _uiCamera.SetActive(false);
        if (!_unarmed && _lastFrameWasUnarmed) _uiCamera.SetActive(true);
    }

    void TakeDamage(int incomingDamage)
    {
        hp -= incomingDamage;
        Debug.Log(hp);
    }
}
