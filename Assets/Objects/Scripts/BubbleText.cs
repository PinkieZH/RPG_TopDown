using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleText : MonoBehaviour
{
    [Header("Références Bulles")]
    public Animator anim;
    public GameObject textBubble;
    public ObjectsManager objectsManager;
    private bool isTextClosed;
    private Transform parentPosition;
    private GameObject objectsManagerInstantiated;

    private void Awake()
    {
        parentPosition = GetComponentInParent<Transform>();
        GameObject newBubble = Instantiate(textBubble,
            new Vector3(parentPosition.position.x, parentPosition.position.y, 0),
            Quaternion.identity);

        newBubble.transform.SetParent(parentPosition);
        anim = textBubble.GetComponent<Animator>();
        objectsManagerInstantiated = newBubble;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsManagerInstantiated.transform.localPosition = new Vector3(0.5f, 3.4f, 0);
        objectsManagerInstantiated.SetActive(true);
        anim.SetBool("isShowing", true);
        isTextClosed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isTextClosed = false;
        objectsManagerInstantiated.SetActive(false);
    }

    private void Update()
    {
        bool textState = objectsManager.IsTextActive();
        if (isTextClosed)
        {
            anim.SetBool("isShowing", !textState);
        }
    }
}
