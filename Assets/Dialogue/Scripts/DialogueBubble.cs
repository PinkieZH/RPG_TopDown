using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBubble : NPC
{
    [Header("Références Bulles")]
    public Animator anim;
    public GameObject dialogueBubble;
    public DialogueManager dialogueManager;
    private bool isDialogueClosed;
    private Transform parentPosition;
    private GameObject dialogueManagerInstantiated;

    private void Awake()
    {
        parentPosition = GetComponentInParent<Transform>();
        GameObject newBubble = Instantiate(dialogueBubble, 
            new Vector3(parentPosition.position.x, parentPosition.position.y, 0), 
            Quaternion.identity);
    
        newBubble.transform.SetParent(parentPosition);
        anim = newBubble.GetComponent<Animator>();
        dialogueManagerInstantiated = newBubble;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        dialogueManagerInstantiated.transform.localPosition = new Vector3(0, 1.5f, 0);
        dialogueManagerInstantiated.SetActive(true);
        anim.SetBool("isShowing", true);
        isDialogueClosed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isDialogueClosed = false;
        anim.SetBool("isShowing", false);
    }

    private void Update()
    {
        bool dialogueState = dialogueManager.IsDialogueActive();
        if (isDialogueClosed)
        {
            anim.SetBool("isShowing", !dialogueState);
        }
    }
}
