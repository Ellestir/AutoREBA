using UnityEngine;
using UnityEngine.UI;

public class REBAScoreHUD : MonoBehaviour
{
    public Text scoreText;

    // The size of the font when the REBA score is 1
    public int initialFontSize = 14;

    // The amount to increase the font size for each increase in REBA score
    public int fontSizeIncreasePerPoint = 2;

    private void Start()  // Changed from Start to Awake
    {
        scoreText = GetComponent<Text>();
        UpdateScoreText(0);  // Initialize with 0 score
    }

    public void UpdateScore(int newREBAScore)
    {
        this.UpdateScoreText(newREBAScore);
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = "REBA Score: " + score;
        scoreText.fontSize = initialFontSize + score * fontSizeIncreasePerPoint;
    }
}
