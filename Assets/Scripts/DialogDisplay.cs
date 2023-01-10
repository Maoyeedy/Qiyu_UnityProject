using UnityEngine;
using UnityEngine.Serialization;

public class DialogDisplay : MonoBehaviour
{
    public bool displayDialog = true;
    public GameObject dialog;
    public float dialogDistance = 3f;
    public bool haveDelay;
    public float delay = 1f;
    public bool haveTipText;
    public GameObject tipText;
    protected GameObject Player;
    protected float DistanceToGameObject;
    protected bool DialogDisplayed, InRange, AlreadyActivated;

    [Header("For Scene 1 Portal")] public bool activatePenguinSoul;
    public PenguinSoul penguinSoul;
    [Header("For Scene 2 Portal")] public bool activatePortal;
    [FormerlySerializedAs("portal")] public FadeIn3D portalBlockCube;
    public GameObject portal;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        if (activatePortal)
            portal.SetActive(false);
    }

    private void Update()
    {
        if (!AlreadyActivated)
            CalculateDistance();
    }

    protected void CalculateDistance()
    {
        DistanceToGameObject = Vector3.Distance(transform.position, Player.transform.position);
        InRange = DistanceToGameObject <= dialogDistance;

        if (InRange)
        {
            AlreadyActivated = true;
            if (activatePenguinSoul)
            {
                GameObject.FindGameObjectWithTag("UI").SetActive(false);
                penguinSoul.displayDialog = true;
            }

            if (!DialogDisplayed && displayDialog)
            {
                DialogDisplayed = true;
                if (haveDelay)
                    Invoke(nameof(ActivateDialog), delay);
                else
                    ActivateDialog();
            }

            if (activatePortal)
                portal.SetActive(true);
        }
    }

    protected void ActivateDialog()
    {
        dialog.SetActive(true);
        if (haveTipText)
            tipText.SetActive(true);
    }
}
