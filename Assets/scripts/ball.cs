using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody2D rb { get; set; }
    private float speed = 15;

    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        resetBall();
    }
    private void setRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = 90f;

        Debug.Log(force.normalized * speed);

        rb.AddForce(force.normalized * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            FindObjectOfType<GameManager>().Miss();
        }
    }
    public void resetBall()
    {
        this.transform.position = new Vector2(0, -6.92f);
        this.rb.velocity = Vector2.zero;
        Invoke(nameof(setRandomTrajectory), 0f);
    }
    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }



}