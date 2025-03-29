using NUnit.Framework.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("NPC Settings")]
    public string npcNoName;
    public string npcName;

    [Header("Dialogue")]
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [Header("Choices")]
    public string[] choices;
    public DialogueData nextDialogue;

}
