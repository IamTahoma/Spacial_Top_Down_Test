using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPoints;
    [SerializeField] TextMeshProUGUI endText;

    public static UIController instance;
    private int pointsAmmount;

    private void Awake()
    {
        instance = this;
    }

    public void AddPoint(int point)
    {
        pointsAmmount += point;
        textPoints.text = "" + pointsAmmount;
    }

    public void EndGameText(string text)
    {
        endText.text = "" + text;
        Invoke(nameof(ReloadScene), 2);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);

    }
}
