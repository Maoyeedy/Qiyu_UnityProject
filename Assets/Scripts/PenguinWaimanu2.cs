using UnityEngine;

public class PenguinWaimanu2 : Penguin
{
    public GameObject[] waimanus;
    public GameObject portal, arrow;

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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (var waimanu in waimanus) waimanu.SetActive(true);

            portal.SetActive(true);
            arrow.SetActive(true);
            finishDialog.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}