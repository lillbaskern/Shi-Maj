using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour, IEnemy
{
    Transform _player;
    [SerializeField] float _moveSpeed = 1f;
    Rigidbody _rigidbody;

    private void Start()
    {
        _player = GameManager.Instance.PlayerTransform;
        this._rigidbody = GetComponent<Rigidbody>();
        SendToGameManager();
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.transform != _player) return;

        _player.GetComponent<PlayerHead>().HP = 5;
    }

    public void SendToGameManager()
    {
        GameManager.Instance.Enemies.Add(this);
    }

    public void EnemyFixedUpdate()
    {
        Vector3 direction = (_player.position - this._rigidbody.position).normalized;
        this._rigidbody.MovePosition(this._rigidbody.position + direction * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Enemies.Remove(this);
    }
}
