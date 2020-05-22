using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool PaddleL;
    public float speed;

    private Rigidbody2D rb;
    private float movement;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start() {
        startPos = transform.position; //Get original position
        rb = GetComponent<Rigidbody2D>(); //Reference to rigidbody2D
    }

    // Update is called once per frame
    void Update() {
        PlayerMove();
    }

    public void PlayerMove() {
        if (PaddleL) {
            movement = Input.GetAxisRaw("Vertical2"); //Left Paddle will move with WS
        } else {
            movement = Input.GetAxisRaw("Vertical1"); //Right Paddle will move with Arrow keys
        }
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }

    /// <summary>
    /// Reset paddles to original starting point
    /// </summary>
    public void ResetPaddle() {
        rb.velocity = Vector2.zero;
        transform.position = startPos;
    }
}
