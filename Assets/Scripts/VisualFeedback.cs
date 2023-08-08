using Unity.VectorGraphics;
using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour
{
    private int maxReba = 15;
    private int currentReba;
    [Range(1, 15)]
    public int rebaScore;
    [HideInInspector] public bool rebaBarEnabled = true; // Add this variable to control the visibility of the RebaBar Slider and Fill
    [HideInInspector] public RebaBar rebaBar;
    public bool useInverseRebaBar = true;
    // Start is called before the first frame update
    [HideInInspector] public UnityEngine.UI.Image extraImage;

    [HideInInspector] public SVGImage rebaImage;  // The SVGImage object in the canvas
    [HideInInspector] public Sprite level1Sprite;  // Add a variable for each image
    [HideInInspector] public Sprite level2Sprite;
    [HideInInspector] public Sprite level3Sprite;
    [HideInInspector] public Sprite level4Sprite;
    [HideInInspector] public Sprite level5Sprite;

    [HideInInspector] public TextMeshProUGUI rebaScoreText;
    [HideInInspector] public bool RebaScoreTextEnabled = true;  // Add a variable to enable or disable the reba text
    [HideInInspector] public TextMeshProUGUI rebaScoreNumber;
    [HideInInspector] public bool RebaScoreNumberEnabled = true;  // Add a variable to enable or disable the reba number
    [HideInInspector] public bool SamFaceEnabled = true;  // Add a variable to enable or disable the SVG images

    [HideInInspector] public bool ImageEnabled = false;  // Add a variable to enable or disable the SVG images

    void Start()
    {
        currentReba = 1;
        rebaBar.SetMaxReba(maxReba);
        rebaBar.SetRebaBar(maxReba - currentReba + 1);
    }

    // Update is called once per frame
    void Update()
    {
        rebaScore = REBA_Score.Score;
        UpdateRebaBar();
        UpdateRebaImage();
        UpdateRebaScoreText();
        UpdateRebaScoreNumber();
    }

    void UpdateRebaBar()
    {
        // Check the value of the new bool variable
        if (useInverseRebaBar)
        {
            // Inverse the REBA bar
            if (rebaScore != currentReba)
            {
                currentReba = rebaScore;
                rebaBar.SetRebaBar(maxReba - currentReba + 1);
            }
            rebaBar.fill.color = rebaBar.gradientAscending.Evaluate(1f - rebaBar.slider.normalizedValue);
        }
        else
        {
            // Regular behavior of the REBA bar
            if (rebaScore != currentReba)
            {
                currentReba = rebaScore;
                rebaBar.SetRebaBar(currentReba);
            }
            rebaBar.fill.color = rebaBar.gradientDescending.Evaluate(1f - rebaBar.slider.normalizedValue);
        }
        rebaBar.border.gameObject.SetActive(rebaBarEnabled);
        rebaBar.fill.gameObject.SetActive(rebaBarEnabled);
    }

    void UpdateRebaImage()
    {
        // Both SamFaceEnabled and ImageEnabled are true
        if (SamFaceEnabled && ImageEnabled)
        {
            rebaImage.enabled = false;
            extraImage.enabled = false;
        }
        // SamFaceEnabled is true and ImageEnabled is false
        else if (SamFaceEnabled && !ImageEnabled)
        {
            UpdateRebaImageSprite();
            rebaImage.enabled = true;
            extraImage.enabled = false;
        }
        // SamFaceEnabled is false and ImageEnabled is true
        else if (!SamFaceEnabled && ImageEnabled)
        {
            rebaImage.enabled = false;
            extraImage.enabled = true;
        }
        // Both SamFaceEnabled and ImageEnabled are false
        else if (!SamFaceEnabled && !ImageEnabled)
        {
            rebaImage.enabled = false;
            extraImage.enabled = false;
        }
    }

    void UpdateRebaImageSprite()
    {
        if (currentReba == 1)
        {
            rebaImage.sprite = level1Sprite;
        }
        else if (currentReba >= 2 && currentReba <= 3)
        {
            rebaImage.sprite = level2Sprite;
        }
        else if (currentReba >= 4 && currentReba <= 7)
        {
            rebaImage.sprite = level3Sprite;
        }
        else if (currentReba >= 8 && currentReba <= 10)
        {
            rebaImage.sprite = level4Sprite;
        }
        else if (currentReba >= 11 && currentReba <= 15)
        {
            rebaImage.sprite = level5Sprite;
        }
    }



    public void UpdateRebaScoreText()
    {
        // If RebaScoreTextEnabled is true, enable the RebaScoreText
        rebaScoreText.enabled = RebaScoreTextEnabled;
        if (RebaScoreTextEnabled)
        {
            if (currentReba == 1)
            {
                rebaScoreText.text = "negligible risk, no action required";
            }
            else if (currentReba >= 2 && currentReba <= 3)
            {
                rebaScoreText.text = "low risk, change may be needed";
            }
            else if (currentReba >= 4 && currentReba <= 7)
            {
                rebaScoreText.text = "medium risk, further investigation, change soon";
            }
            else if (currentReba >= 8 && currentReba <= 10)
            {
                rebaScoreText.text = "high risk, investigate and implement change";
            }
            else if (currentReba >= 11 && currentReba <= 15)
            {
                rebaScoreText.text = "very high risk, implement change";
            }
            else
            {
                rebaScoreText.text = "Invalid REBA score";
            }
            // If RebaScoreTextEnabled is true, enable the RebaScoreText
            rebaScoreText.enabled = true;
        }
        else
        {
            // If RebaScoreTextEnabled is false, disable the RebaScoreText
            rebaScoreText.enabled = false;
        }
    }

    public void UpdateRebaScoreNumber()
    {
        // If RebaScoreNumberEnabled is true, enable the RebaScoreNumber
        rebaScoreNumber.enabled = RebaScoreNumberEnabled;
        if (RebaScoreNumberEnabled)
        {
            Color newColor;
            if (currentReba == 1)
            {
                rebaScoreNumber.text = currentReba.ToString();
                rebaScoreNumber.fontSize = 44;
                ColorUtility.TryParseHtmlString("#92D050", out newColor); // Green in HEX
                rebaScoreNumber.color = newColor;
            }
            else if (currentReba >= 2 && currentReba <= 3)
            {
                rebaScoreNumber.text = currentReba.ToString();
                rebaScoreNumber.fontSize = 54;
                ColorUtility.TryParseHtmlString("#FFDA65", out newColor); // Green in HEX
                rebaScoreNumber.color = newColor;
            }
            else if (currentReba >= 4 && currentReba <= 7)
            {
                rebaScoreNumber.text = currentReba.ToString();
                rebaScoreNumber.fontSize = 65;
                ColorUtility.TryParseHtmlString("#FFC000", out newColor); // Green in HEX
                rebaScoreNumber.color = newColor;
            }
            else if (currentReba >= 8 && currentReba <= 10)
            {
                rebaScoreNumber.text = currentReba.ToString();
                rebaScoreNumber.fontSize = 74;
                ColorUtility.TryParseHtmlString("#F60000", out newColor); // Green in HEX
                rebaScoreNumber.color = newColor;
            }
            else if (currentReba >= 11 && currentReba <= 15)
            {
                rebaScoreNumber.text = currentReba.ToString();
                rebaScoreNumber.fontSize = 84;
                ColorUtility.TryParseHtmlString("#C00000", out newColor); // Green in HEX
                rebaScoreNumber.color = newColor;
            }
            else
            {
                rebaScoreNumber.text = "Invalid REBA score";
                rebaScoreNumber.fontSize = 44;
                ColorUtility.TryParseHtmlString("#92D050", out newColor); // Green in HEX
                rebaScoreNumber.color = newColor;
            }
            // If RebaScoreNumberEnabled is true, enable the RebaScoreNumber
            rebaScoreNumber.enabled = true;
        }
        else
        {
            // If RebaScoreNumberEnabled is false, disable the RebaScoreNumber
            rebaScoreNumber.enabled = false;
        }
    }
}
