using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject Popup;
    protected bool interacted = false;

    private void Update()
    {
        Interact();
        ShowTooltip();
    }

    protected void Interact()
    {
        if (Input.GetKey(KeyCode.E) && CheckForInteractions())
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        //insert here logic for an object after interaction
    }

    private bool CheckForInteractions()
    {
        return Physics2D.OverlapCircle(transform.position, 2.5f, layerMask);
    }

    private void ShowTooltip()
    {
        if(CheckForInteractions() && !interacted)
        {
            Popup.SetActive(true);
        }
        else if(!CheckForInteractions() || interacted)
        {
            Popup.SetActive(false);
        }
    }
}
