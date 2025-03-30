using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel; //panel qui contient tout l'UI dialogue
    public TextMeshProUGUI nameText; //texte qui affiche nom du perso qui parle
    public TextMeshProUGUI dialogueText; //texte qui affiche le dialogue
    public GameObject continueIndicator; //indicateur visuel qui montre quon peut continuer

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f; //temps entre l'affichage de chaque caractères (en secondes)

    [Header("Input Settings")]
    public InputActionReference continueDialogueAction; //l'action d'input pour continuer le dialogue

    public string[] currentLines; //tableau contenant les lignes du dialogue actuel
    public int currentLineIndex; //index de la ligne actuellement affichée
    private bool isTyping; // estce que le texte vient detre tapé?
    private Coroutine typeCoroutine; //ref à la coroutine qui affiche txt car. par car.
    private bool isDialogueActive; //estcequ'un dialogue est actif? 


    //Singleton pattern
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        //Singleton pattern
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        //s'assurer que le dialogue est desac au démarrage
        dialoguePanel.SetActive(false);
        if (continueIndicator != null)
            continueIndicator.SetActive(false);

        //config l'action d'input pour continuer le dialogue
        if (continueDialogueAction != null)
        {
            continueDialogueAction.action.started += OnContinueDialogueInput;
        }
    }

    private void OnEnable()
    {
        //activer l'action d'input
        if(continueDialogueAction != null)
        {
            continueDialogueAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if(continueDialogueAction != null)
        {
            continueDialogueAction.action.Disable();
        }
    }

    private void OnDestroy()
    {
        //nettoyer les abonnement d'evenements
        if(continueDialogueAction != null)
        {
            continueDialogueAction.action.started -= OnContinueDialogueInput;
        }
    }

    //cette methode est appelée quand l'action de continuer le dialogue est trigger
    private void OnContinueDialogueInput(InputAction.CallbackContext context)
    {
        if (!isDialogueActive)
        {
            return;
        }

        if (isTyping)
        {
            //si le texte est en train detre taper le completer immediatement
            CompleteTyping();
        }
        else
        {
            //sinon passer à la ligne  suivante
            DisplayNextLine();
        }
    }

    public void StartDialogue(string speakerName, string[] lines)
    {
        //reinitialiser les variables de dialogue
        currentLines = lines;
        currentLineIndex = 0;
        isDialogueActive = true;

        //activer le panel et definir le nom du perso
        dialoguePanel.SetActive(true);
        nameText.text = speakerName;

        //afficher la première ligne
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }

        //si on est à la fin du dialogue: terminer
        if (currentLineIndex >= currentLines.Length)
        {
            EndDialogue();

            return;
        }

        //desac l'indicateur de continuation
        if (continueIndicator)
        {
            continueIndicator.SetActive(false);
        }

        //commencer à taper la line
        typeCoroutine = StartCoroutine(TypeLine(currentLines[currentLineIndex]));
        currentLineIndex++;
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        //activer l'indicateur de continuation
        if (continueIndicator)
        {
            continueIndicator.SetActive(true);
        }
    }

    private void CompleteTyping()
    {
        if(typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }

        dialogueText.text = currentLines[currentLineIndex - 1];
        isTyping = false;

        //activer l'indicateur de continuation
        if (continueIndicator)
        {
            continueIndicator.SetActive(true);
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }

    //methode publique pour verif si un dialogue est en cours
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
