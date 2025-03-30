using UnityEngine;


public class NPC : MonoBehaviour, IInteractable
{
    [Header("Dialogue Data")]
    public DialogueData dialogueData;

    [Header("Custom Settings (Optional)")]
    [Tooltip("Laissez vide pour utiliser le nom d�fini dans le DialogueData")]
    public string customName;

    public void Interact()
    {
        if(dialogueData == null)
        {
            Debug.LogError("DialogueData not assigned to NPC: " + gameObject.name);
            return;
        }

        // v�rif si DialogueManager existe et d�marrer le dialogue
        if (DialogueManager.Instance != null)
        {
            //Utiliser le nom personnalis� s'il est d�fini sinon celui du DialogueData
            string speakerName = string.IsNullOrEmpty(customName) ? dialogueData.npcNoName : customName;
            DialogueManager.Instance.StartDialogue(speakerName, dialogueData.dialogueLines);
        }
        else
        {
            Debug.LogError("DialogueManager not found in scene !");
        }
    }
    
    public void GetInteractionPrompt()
    {
        //if (dialogueData == null)
       // {
        //    return "Appuyez sur E pour interagir";
        //}

        //string name = string.IsNullOrEmpty(customName) ? dialogueData.npcName : customName;
        //return "Appuyez sur E pour parler � " + name;

    }

    public void Read()
    {

    }
}
