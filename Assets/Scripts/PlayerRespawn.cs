using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint;
    public float respawnDelay = 0.5f;
    public bool haveRespawnAudio;
    public AudioSource respawnAudio;

    private GameObject _player;
    private Transform _playerTransform;
    private Rigidbody _rb;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        _rb = _player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Respawn), respawnDelay);
            if (haveRespawnAudio)
                respawnAudio.Play();
        }
    }


    private void Respawn()
    {
        var transform1 = _playerTransform.transform;
        var transform2 = respawnPoint.transform;

        transform1.rotation = transform2.rotation;
        transform1.position = transform2.position;

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}