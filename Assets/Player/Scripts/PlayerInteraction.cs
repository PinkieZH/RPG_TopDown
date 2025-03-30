using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using JetBrains.Annotations;

public class PlayerInteraction : InputHandler
{
    [Header("UI References")]
    public GameObject interactionPromptPanel;
    public TextMeshProUGUI interactionPromptText;
    //public GameObject dialogueMark;
    //public Animator anim;

    // L'interactable actuellement � port�e
    private IInteractable currentInteractable;

    private void Awake()
    {
        // S'assurer que le prompt est d�sactiv� au d�marrage
        if (interactionPromptPanel != null)
        {
            interactionPromptPanel.SetActive(false);
        }

    }


    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Interact"].started += OnInteract;
            playerInput.actions["Read"].started += OnRead;
        }
        else
        {
            Debug.LogError("PlayerInput is null in MovementInputHandler");
        }
    }


    protected override void UnregisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Interact"].started -= OnInteract;
            playerInput.actions["Read"].started -= OnRead;
        }
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void OnRead(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Read();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        currentInteractable = interactable;

        if (currentInteractable != null && interactionPromptPanel != null)
        {
            //interactionPromptPanel.SetActive(true);
            //interactionPromptText.text = currentInteractable.GetInteractionPrompt();*/
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (currentInteractable == interactable)
        {
            currentInteractable = null;

            if (interactionPromptPanel != null)
            {
                interactionPromptPanel.SetActive(false);
            }
        }
    }
}



