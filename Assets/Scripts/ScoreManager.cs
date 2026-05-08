using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public AudioClip scoreSound;
    private AudioSource audioSource;

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreText.text = "0";
    }

    public void AddPoint()
    {

        score++;
        audioSource.PlayOneShot(scoreSound);
        scoreText.text = score.ToString();
        Debug.Log("Score: " + score);
    }
}