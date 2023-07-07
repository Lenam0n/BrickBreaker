using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ball Ball { get; private set; }
    public player paddle { get; private set; }
    public Bricks[] bricks { get; private set; }
    private int level = 1;
    private int Score = 0;
    private int lives = 3;
    //[SerializeField] private Text text;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        Score = 0;
        level = 0;
        lives = 3;
        loadLevel(1);
    }
    private void loadLevel(int level)
    {
        this.level = level;
        if (level > 10)
        {
            SceneManager.LoadScene("Endgame");
        }
        SceneManager.LoadScene("level" + level);


    }
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.Ball = FindObjectOfType<ball>();
        this.paddle = FindObjectOfType<player>();
        this.bricks = FindObjectsOfType<Bricks>();
    }
    public void Hit(Bricks brick)
    {
        this.Score += brick.points;
        if (Cleared())
        {
            loadLevel(this.level + 1);
        }
    }
    public void Miss()
    {
        lives--;
        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }
    private void ResetLevel()
    {
        this.Ball.resetBall();
        this.paddle.resetPaddle();
    }
    public void GameOver()
    {
        NewGame();
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }

        }
        return true;
    }
}