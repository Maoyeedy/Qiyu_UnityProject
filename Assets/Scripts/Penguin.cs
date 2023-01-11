using UnityEngine;

public class Penguin : MonoBehaviour
{
    public GameObject dialog, finishDialog, switcher, tipText;
    public float dialogDistance = 6.0f;
    public float rotationSpeed = 3.5f, maxLookDistance = 15f;
    public bool lookAtPlayer = true, canBeDestroyed, haveTipText, haveDialog, haveSwitcher, haveFinishDialog;

    public GameObject player, dna, scanner;
    public float distanceToGameObject;
    public bool gameIsOn;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        distanceToGameObject = Vector3.Distance(transform.position, player.transform.position);

        Interact();

        if (lookAtPlayer)
            LookAtPlayer();

        if (haveSwitcher)
            if (dna.activeInHierarchy || scanner.activeInHierarchy)
                gameIsOn = true;
    }

    protected void LookAtPlayer()
    {
        if (distanceToGameObject <= maxLookDistance)
        {
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    protected void Interact()
    {
        if (distanceToGameObject <= dialogDistance)
        {
            if (haveDialog)
                dialog.SetActive(true);
            if (haveSwitcher)
                switcher.SetActive(true);
            if (haveTipText)
                tipText.SetActive(true);
        }
    }

    protected void OnTabPressed()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && gameIsOn)
        {
            if (haveSwitcher)
                switcher.SetActive(false);
            if (haveFinishDialog)
                finishDialog.SetActive(true);
            if (canBeDestroyed)
                Destroy(gameObject);
            gameIsOn = false;
        }
    }
}
