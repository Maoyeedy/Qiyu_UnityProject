using UnityEngine;

public class AudioContinuePlay : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}