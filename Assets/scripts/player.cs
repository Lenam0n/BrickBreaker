using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class player : MonoBehaviour
{
    private float speed = 100f;
    public Rigidbody2D rb { get;  set; }
    private Vector2 posi;
    private Vector2 direction { get; set; }
    public float MaxAngle = 65f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        resetPaddle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            direction = Vector2.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rb.AddForce(direction * speed);
        }
    }


private void OnCollisionEnter2D(Collision2D collision)
    {
        ball Ball = collision.gameObject.GetComponent<ball>();
        if (Ball != null)
        {
            
        
            Vector3 paddlePos = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePos.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, Ball.rb.velocity);
            float bounceAngle = (offset / width) * this.MaxAngle ;
            float newAngle = Mathf.Clamp( currentAngle + bounceAngle,-this.MaxAngle,this.MaxAngle) ;

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward) ;
            Ball.rb.velocity = rotation * Vector2.up * Ball.rb.velocity.magnitude;
      
        }
    
    }
    public void resetPaddle()
    {
        this.transform.position = new Vector2 (0f,this.transform.position.y);
        this.rb.velocity = Vector2.zero;
    }
}
