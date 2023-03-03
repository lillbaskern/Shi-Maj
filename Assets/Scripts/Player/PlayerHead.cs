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
    public static List<ICharacter> Characters = new();
    ICharacter _currChar;
    [SerializeField] private int hp;
    private bool hasInit;
    private int _currCharIndex = 0;
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
        //let character scripts do their thing
        yield return new WaitForEndOfFrame();

        //cache input handler
        _currChar = Characters[_currCharIndex];
        _uiCamera = GameObject.Find("UICamera");
        InitAllChars();
        _input = (InputHandler)FindObjectOfType(typeof(InputHandler));
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
            //TODO: MAKE CURRCHARINDEX STAY WITHIN THE LISTS RANGE
            _currChar = Characters[++_currCharIndex];
            Debug.Log("switched to "+ _currChar);
        }
        if (_input.PrevChar.WasPressedThisFrame())
        {
            _currChar = Characters[--_currCharIndex];
            Debug.Log("switched to "+ _currChar);
        }

        _currChar.CharacterLoop(_input);
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
