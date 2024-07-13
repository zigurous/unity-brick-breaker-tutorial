using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const int NUM_LEVELS = 2;

    private Ball ball;
    private Paddle paddle;
    private Brick[] bricks;

    public int level { get; private set; } = 1;
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FindSceneReferences();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > NUM_LEVELS)
        {
            // Start over again at level 1 once you have beaten all the levels
            // You can also load a "Win" scene instead
            LoadLevel(1);
            return;
        }

        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        FindSceneReferences();
    }

    public void OnBallMiss()
    {
        lives--;

        if (lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    private void GameOver()
    {
        // Start a new game immediately
        // You can also load a "GameOver" scene instead
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        LoadLevel(1);
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points;

        if (Cleared()) {
            LoadLevel(level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }

}
