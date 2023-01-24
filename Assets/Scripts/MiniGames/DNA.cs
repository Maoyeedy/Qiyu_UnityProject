using UnityEngine;
using TMPro;
using Random = System.Random;

public class Dna : MonoBehaviour
{
    public GameObject[] letterPrefabs;
    public float spawnInterval = 1.5f;
    public float yMax = -2f;
    public float yMin = -3f;

    public GameObject dna;
    public GameObject nextPenguin;
    public Camera camMain, camDna;
    public TextMeshProUGUI textComponent;
    public Progress progress;
    public ProgressFade progressFade;

    private int _index;
    private bool _gameFinished, _canPressEnter = true;
    private GameObject _letter;
    private Transform _letterTransform;
    private Rigidbody2D _rb;
    private Vector3 _scale;
    private Quaternion _rotation;
    private GameObject[] _lettersInstantiated;
    private AudioSource _bingo;

    private void Start()
    {
        _bingo = gameObject.GetComponent<AudioSource>();
        _rotation = dna.transform.rotation;
        _scale = dna.transform.localScale;
        RandomizeIndex();
        InvokeSpawn();
    }

    private void Update()
    {
        if (_letterTransform.position.y >= yMin && _letterTransform.position.y <= yMax)
            if (_canPressEnter && Input.GetKeyDown(KeyCode.Return))
            {
                _rb.constraints = RigidbodyConstraints2D.FreezePosition;
                _bingo.Play();
                NextLetter();
                _canPressEnter = false;
                return;
            }

        if (Input.GetKeyDown(KeyCode.Tab) && _gameFinished)
        {
            _lettersInstantiated = GameObject.FindGameObjectsWithTag("Letter");
            foreach (var letter in _lettersInstantiated) Destroy(letter);

            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x - 1.8f * (letterPrefabs.Length - 1), position.y, position.z);
            transform1.position = position;

            textComponent.text = "当字母掉落到黄色区域时 按下回车";
            _index = 0;
            RandomizeIndex();
            InvokeSpawn();

            camDna.enabled = false;
            camMain.enabled = true;
            Cursor.visible = false;
            nextPenguin.SetActive(true);
            progressFade.TempDisplayProgressBar();
            progress.UpdateScore();
            dna.SetActive(false);
        }
    }

    private void SpawnLetter()
    {
        if (gameObject.activeInHierarchy)
        {
            _letter = Instantiate(letterPrefabs[_index], transform.position, _rotation);
            _letter.transform.localScale = _scale * 0.7f;
            _letter.transform.SetParent(dna.transform);
            _rb = _letter.GetComponent<Rigidbody2D>();
            _letterTransform = _letter.GetComponent<Transform>();
            _canPressEnter = true;
        }
    }

    private void NextLetter()
    {
        if (_index < letterPrefabs.Length - 1)
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x + 1.8f, position.y, position.z);
            transform1.position = position;
            _index++;
        }
        else
        {
            CancelInvoke();
            _gameFinished = true;
            textComponent.text = "恭喜你完成了 DNA 测序，请按 Tab 退出";
        }
    }

    private void RandomizeIndex()
    {
        var rng = new Random();

        for (var i = 0; i < letterPrefabs.Length - 1; i++)
        {
            var j = rng.Next(i, letterPrefabs.Length);
            (letterPrefabs[i], letterPrefabs[j]) = (letterPrefabs[j], letterPrefabs[i]);
        }
    }

    private void InvokeSpawn()
    {
        InvokeRepeating(nameof(SpawnLetter), 0f, spawnInterval);
    }
}