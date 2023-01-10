using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    public float characterInterval = 0.05f;

    public string textInput = "而我来到这里，\n探寻企鹅灭绝的真相";

    private TextMeshProUGUI _tmPro;

    private void Start()
    {
        _tmPro = GetComponent<TextMeshProUGUI>();
        _tmPro.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (var x in textInput.ToCharArray())
        {
            _tmPro.text += x;
            yield return new WaitForSeconds(characterInterval);
        }
    }
}