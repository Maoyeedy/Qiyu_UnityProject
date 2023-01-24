using Invector.vCharacterController;
using UnityEngine;

public class EnableJump : DialogDisplay
{
    private vThirdPersonInput _input;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _input = Player.GetComponent<vThirdPersonInput>();
    }

    private void Update()
    {
        DistanceToGameObject = Vector3.Distance(transform.position, Player.transform.position);

        if (DistanceToGameObject <= dialogDistance)
        {
            ActivateDialog();
            EnableSpaceJump();
        }
    }

    private void EnableSpaceJump()
    {
        _input.jumpInput = KeyCode.Space;
    }
}