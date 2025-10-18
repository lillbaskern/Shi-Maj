using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

///////<SUMMARY>///////
//PlayerHead contains important data about the players current state,
//-so that other scripts can quickly make changes to the player's HP and character data
[DefaultExecutionOrder(-2)]
public class PlayerHead : MonoBehaviour
{
    public static event EventHandler<CharChangeEventArgs> TextChanged; //handling events in such a disorganized way like this is a slight hassle
    public static Action UpdateWeaponNameUIUnarmed;

    TextMeshProUGUI _hpDisplay;

    //unity event so i can quickly trigger the death state with minimal coding
    public UnityEvent OnDeath;

    //the viewmodel's camera's GameObject
    GameObject _uiCamera;

    InputHandler _input;
    PlayerShoot _shooter;

    public static List<ICharacter> Characters;
    ICharacter _currChar;

    public float XP { get; private set; }
    public float Gold { get; private set; }
    float _gold = 0;



    public ICharacter CurrentCharacter
    {
        get { return _currChar; }
    }


    private int _currCharIndex = 0;


    private bool hasInit;

    //neat way of using properties yet still having inspector editable variables below
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

    public bool FreezePlayer = true;

    IEnumerator Start()
    {
        if (!FreezePlayer) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
        Characters = new();
        CurrentWeaponTextListener.PlayerHead = this;
        //let character scripts do their thing
        yield return new WaitForEndOfFrame();

        _currChar = Characters[_currCharIndex];
        
        _hpDisplay = GameObject.Find("HP Display").GetComponent<TextMeshProUGUI>();
        
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

    public void AddXP(int xp) 
    {
        _currChar.AddXP(xp);
    }

    void Update()
    {
        if (!hasInit) return;
        if (FreezePlayer) return;

        //code for switching between chars below [DEPRECATED]
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
        if(hp <=0) return; //sanity check

        hp -= incomingDamage;
        
        //TODO: update HP display
        _hpDisplay.text = $"HP: {hp}";

        if(hp <= 0) OnDeath?.Invoke();
    }

    public void FreezeUnFreeze()
    {
        FreezePlayer = !FreezePlayer;
        if (!FreezePlayer) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
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

    public WeaponUIEventArgs()
    {
        WeaponName = "Unarmed";
        AmmoCache = -1;
    }

    public bool IsSimple;
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
