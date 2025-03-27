using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [Header("Mouvement")]
    [SerializeField]
    private float moveSpeed = 4f;

    [Header("Réferences")]
    [SerializeField]
    private Rigidbody2D rb;

    public Animator anim;
    private Vector2 lastMoveDirection;

    private Vector2 moveDirection;
    /*private bool isFacingDown = true;*/


    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
    private void Start()
    {

        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.SetPlayerInput(playerInput);
            }
            else
            {
                Debug.LogError("InputManager is not in the scene");
            }
        }
        else
        {
            Debug.LogError("Missing PlayerInput on GameObject");
        }
    }
    private void Update()
    {
        Animate();
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;

        /*if (direction.x > 0 && isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && !isFacingRight)
        {
            Flip();
        } pour flip le sprite si symétrique plus simple que beaucoup d'animation */
    }


    /*private void Flip()
     {
       isFacingRight = !isFacingRight;
       Vector3 scale = transform.localScale;
       scale.x *= -1;
       transform.localScale = scale;
     }*/



    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        if (rb)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on PlayerController");
        }
    }

    void Animate()
    {
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }

        anim.SetFloat("MoveX", moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.y);
        anim.SetFloat("MoveMagnitude", moveDirection.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }


}

