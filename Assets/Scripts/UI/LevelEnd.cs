using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Sprite shinyStar;
    [SerializeField] private Sprite darkStar;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    [SerializeField] private TMPro.TextMeshProUGUI failedText;
    [SerializeField] private Image[] starsImages;
    [SerializeField] private GameObject winScreen;

    private readonly string levelString = "Level ";

    public void FillWinScreenData(string levelNumber, int numberOfStars)
    {
        levelText.text = levelString + levelNumber;

        FillStars(numberOfStars);
    }

    public void FillLoseScreenData(string losingText)
    {
        failedText.text = losingText;
    }

    private void FillStars(int numberOfStars)
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            starsImages[i].sprite = shinyStar;
        }

        for (int i = numberOfStars; i < starsImages.Length; i++)
        {
            starsImages[i].sprite = darkStar;
        }
    }

    public void CloseWinScreen()
    {
        winScreen.SetActive(false);
    }
}
