using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class VisualMathMinigame : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text questionText;
    public Transform imageHolderA;
    public Transform imageHolderB;
    public GameObject spriteTemplate; // Disabled prefab Image
    public List<Button> answerButtons;

    [Header("Feedback UI")]
    public GameObject feedbackPanel;
    public Image feedbackIcon;
    public TMP_Text feedbackText;
    public Sprite correctSprite;
    public Sprite incorrectSprite;
    public TMP_Text pointsText;  // assign this in the Inspector
    private int points = 0;

    [SerializeField] private minigameManager mini;

    [Header("Game Data")]
    public Sprite[] objectSprites; // Pool of different sprites to choose from

    private int correctAnswer;
    private int incorrectAttempts = 0;  // To track the number of incorrect attempts on the current question
    //public canvasmanager canvasmanager;
    void UpdatePointsUI()
    {
        pointsText.text = "" + points;
       
    }

    void Start()
    {
        points = 0;
        UpdatePointsUI();
        GenerateQuestion();
        feedbackPanel.SetActive(false);
    }

    void GenerateQuestion()
    {
        ClearImages();

        int countA = Random.Range(1, 6); // Between 1–5
        int countB = Random.Range(1, 6);
        correctAnswer = countA + countB;

        // Choose two random sprites for A and B groups
        Sprite spriteA = objectSprites[Random.Range(0, objectSprites.Length)];
        Sprite spriteB = objectSprites[Random.Range(0, objectSprites.Length)];

        questionText.text = $"What is {countA} + {countB}?";

        ShowImages(imageHolderA, countA, spriteA);
        ShowImages(imageHolderB, countB, spriteB);
        SetupAnswerButtons(correctAnswer);
    }

    void ShowImages(Transform parent, int count, Sprite sprite)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(spriteTemplate, parent);
            obj.GetComponent<Image>().sprite = sprite;
            obj.SetActive(true);
        }
    }

    void ClearImages()
    {
        foreach (Transform child in imageHolderA) Destroy(child.gameObject);
        foreach (Transform child in imageHolderB) Destroy(child.gameObject);
    }

    void SetupAnswerButtons(int correct)
    {
        int correctIndex = Random.Range(0, answerButtons.Count);

        for (int i = 0; i < answerButtons.Count; i++)
        {
            int answerValue;

            if (i == correctIndex)
                answerValue = correct;
            else
            {
                // Generate a unique incorrect answer
                do
                {
                    answerValue = Random.Range(correct - 2, correct + 3);
                } while (answerValue == correct);
            }

            TMP_Text btnText = answerButtons[i].GetComponentInChildren<TMP_Text>();
            btnText.text = answerValue.ToString();

            int captured = answerValue;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(captured));
        }
    }

    void ShowFeedback(bool isCorrect)
    {
        feedbackPanel.SetActive(true);
        feedbackText.text = isCorrect ? "Correct!" : "Try Again!";
        feedbackIcon.sprite = isCorrect ? correctSprite : incorrectSprite;

        CancelInvoke(nameof(HideFeedback));
        Invoke(nameof(HideFeedback), 1.2f); // Hide after delay
    }

    void HideFeedback()
    {
        feedbackPanel.SetActive(false);
    }

    void OnAnswerSelected(int selected)
    {
        feedbackPanel.SetActive(true);

        if (selected == correctAnswer)
        {
            Debug.Log("Correct!");
            ShowFeedback(true);
            points += 2;
            UpdatePointsUI();  
            incorrectAttempts = 0;
            if (points >= 20)
            {
                Debug.Log("Player reached 20 points! Switching canvas...");
                mini.ShowGoodEnding(); // or whichever method you use to switch canvases
                return; // Skip generating a new question
            }

            Invoke(nameof(GenerateQuestion), 1.5f);
           
        }
        else
        {
            Debug.Log("Try Again!");
            incorrectAttempts++;  // Increment incorrect attempts

            if (incorrectAttempts >= 2)  // If 2 incorrect attempts are made
            {
                Debug.Log("Decreasing points and moving to next question...");
                points = Mathf.Max(points - 1, 0);  // Decrease points by 1, but not below 0
                UpdatePointsUI();  // Update the UI
                incorrectAttempts = 0;  // Reset incorrect attempts
                Invoke(nameof(GenerateQuestion), 1.5f); // Move to the next question after delay
            }
            else
            {
                ShowFeedback(false);  // Show "Try Again" feedback
            }
        }
    }
}
