using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LifeLongWrestler : PlayerMove, IPlayerCharacter
{

    void Start()
    {

    }

    void Update()
    {
        MoveAndTurnLoop();
    }
    public void OnSpecial(InputValue val)
    {
        
    }
    public void OnShoot(InputValue val)
    {

    }
}
