using UnityEngine;

public class Bricks : MonoBehaviour
{
    public int health {  get; private set; }
    public SpriteRenderer sr { get; private set; }
    public Sprite[] states;
    public bool unbreakable;
    public int points = 100;

    private void Awake()
    {
        this.sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!this.unbreakable)
        {
            this.health = this.states.Length;
            this.sr.sprite = this.states[this.health-1];
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ball") 
        {
            Hit();
        }
    }
    private void Hit()
    {
        if (this.unbreakable)
        {
            return;
        }
        this.health --;
        if (this.health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.sr.sprite = this.states[this.health - 1];
        }

        FindObjectOfType<GameManager>().Hit(this);
        

    }

}
