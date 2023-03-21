using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

///////<SUMMARY>///////
//PlayerHead contains important data about the players current state,
//-so that other scripts can quickly make changes to the player's HP/other stats. 
//for the sake of this assignment, it also handles characters and character switching
public class PlayerHead : MonoBehaviour
{
    public static event EventHandler<CharChangeEventArgs> TextChanged; //handling events in such disorganized ways could be very dangerous
    public static Action UpdateWeaponNameUIUnarmed;

    //the viewmodel's camera's GameObject
    GameObject _uiCamera;

    InputHandler _input;


    PlayerShoot _shooter;

    public static List<ICharacter> Characters = new();
    ICharacter _currChar;

    public ICharacter CurrentCharacter
    {
        get { return _currChar; }
    }

    private int _currCharIndex = 0;
    [SerializeField] private int hp;
    private bool hasInit;
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


    IEnumerator Start()
    {
        CurrentWeaponListener.PlayerHead = this;
        //let character scripts do their thing
        yield return new WaitForEndOfFrame();

        _currChar = Characters[_currCharIndex];

        //invoke event
        CharChangeEventArgs args = new(_currChar.GetName());
        TextChanged?.Invoke(this, args);

        _uiCamera = GameObject.Find("UICamera");

        InitAllChars();

        _input = GetComponent<InputHandler>();
        hasInit = true;
    }
    void InitAllChars()
    {
        foreach (ICharacter character in Characters)
        {
            Debug.Log("initalizing char: " + character);
            character.CharacterInit();
        }
    }
    void Update()
    {
        if (!hasInit) return;

        if (_input.NextChar.WasPressedThisFrame())
        {
            _currCharIndex++;
            if (_currCharIndex > Characters.Count - 1)
            {
                _currCharIndex = Characters.Count - 1;
                return;
            }
            _currChar = Characters[_currCharIndex];
            Debug.Log("switched to " + _currChar);

            //invoke events
            CharChangeEventArgs args = new(_currChar.GetName());
            TextChanged?.Invoke(this, args);
            var weapon = _currChar.GetCurrWeapon();
            if (weapon == null)
            {
                UpdateWeaponNameUIUnarmed?.Invoke();
                return;
            }
            WeaponUIEventArgs eventArgs = new(weapon);
            weapon?.UpdateUI(eventArgs, this);
        }

        if (_input.PrevChar.WasPressedThisFrame())
        {
            _currCharIndex--;
            if (_currCharIndex < 0)
            {
                _currCharIndex = 0;
                return;
            }
            _currChar = Characters[_currCharIndex];
            Debug.Log("switched to " + _currChar);

            //invoke event
            CharChangeEventArgs args = new(_currChar.GetName());
            TextChanged?.Invoke(this, args);
            var weapon = _currChar.GetCurrWeapon();
            WeaponUIEventArgs eventArgs = new(weapon);
            weapon?.UpdateUI(eventArgs, this);


        }

        _currChar.CharacterLoop(_input);
    }

    void TakeDamage(int incomingDamage)
    {
        hp -= incomingDamage;
        Debug.Log(hp);
    }
}

//event argument classes
public class WeaponUIEventArgs : EventArgs
{
    public WeaponUIEventArgs(Weapon weapon)
    {
        if (weapon == null)
        {
            WeaponName = "Unarmed";
            AmmoCache = -1;
            return;
        }
        WeaponName = weapon.WeaponName;
        currMag = weapon.CurrMag;
        AmmoCache = weapon.AmmoStock;
    }
    public string WeaponName;
    public int currMag;
    public int AmmoCache;
    public GameObject WeaponPrefab;
}
public class CharChangeEventArgs : EventArgs
{
    public CharChangeEventArgs(string input)
    {
        text = input;
    }
    public string text;
}
