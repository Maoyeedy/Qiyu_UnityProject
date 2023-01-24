using Invector.vCharacterController;
using UnityEngine;

public class PlayerDisableRun : MonoBehaviour
{
    public float range = 6f;
    private GameObject _player;
    private vThirdPersonInput _controller;
    private float _distanceToGameObject;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _controller = _player.GetComponent<vThirdPersonInput>();
    }

    private void Update()
    {
        _distanceToGameObject = Vector3.Distance(transform.position, _player.transform.position);
        if (_distanceToGameObject <= range) _controller.sprintInput = KeyCode.None;
    }
}