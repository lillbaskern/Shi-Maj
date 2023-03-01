using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public interface IPlayerCharacter
{
    //not sure what im gonna do in here 
    //this is just so i can easily store characters 
    public void OnShoot(InputValue val);
    public void OnSpecial(InputValue val);
}
