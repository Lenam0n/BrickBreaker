using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    private float speed = 0.05f;
    public Rigidbody2D rb { get;  set; }
    private bool contact = true;
    [SerializeField] private GameObject Ball;
    private Vector2 posi;
    private Vector2 direction { get; set; }
    public float MaxAngle = 75f;

    // Start is called before the first frame update
    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        resetPaddle();
    }

    // Update is called once per frame
    void Update()
    {
        posi = new Vector2(this.gameObject.transform.position.x, 0);
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Vector2 nextPos = posi + Vector2.left * speed;
            if (!(nextPos.x < -15.5f))
            {
                this.gameObject.transform.position += Vector3.left * speed;
            }

        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Vector2 nextPos = posi + Vector2.left * speed;

            if (!(nextPos.x > 15.5f))
            {
                this.gameObject.transform.position -= Vector3.left * speed;
            }

        }
        else
        {
            this.gameObject.transform.position -= Vector3.zero;
        }



    }
    /*private void FixedUpdate()
    {
        if(this.direction != Vector2.zero)
        {
            this.rb.AddForce(this.direction);
        }
    }*/

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
