using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    Transform _player;
    [SerializeField] float _moveSpeed = 1f;

    private void Start()
    {
        _player = GameManager.Instance.PlayerTransform;
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform != _player) return;

        _player.GetComponent<PlayerHead>().HP = 5;
    }

    void Update()
    {
        var moveToward = Vector3.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
        transform.position = new(moveToward.x, transform.position.y, moveToward.z);
    }
}
