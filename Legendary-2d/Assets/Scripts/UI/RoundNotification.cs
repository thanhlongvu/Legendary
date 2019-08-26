using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundNotification : MonoBehaviour
{
    [SerializeField]
    private Text roundText;
    [SerializeField]
    private Text roundTextIcon;
    //[SerializeField]
    //private Image containerImg;

    private Color colorContainer;
    private Color colorShow;
    private Color colorHide;

    private int roundIndex = 0;
    void Start()
    {
        colorContainer = roundText.color;
        colorShow = new Color(colorContainer.r, colorContainer.g, colorContainer.b, 1);
        colorHide = new Color(colorContainer.r, colorContainer.g, colorContainer.b, 0);

        EventManager.AddListener(GameEvent.ROUND_DONE, ChangeText);
        EventManager.AddListener(GameEvent.ROUND_DONE, Notification);
    }


    private void OnEnable()
    {
    }

    public void Notification()
    {
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction()
    {
        Show();
        yield return StaticObjects.WAIT_TIME_TWO;
        Hide();
    }

    private void Show()
    {
        //DOTweenModuleUI.DOColor(containerImg, colorShow, 1.5f);
        DOTweenModuleUI.DOColor(roundText, colorShow, 2.5f);
    }

    private void Hide()
    {
        //DOTweenModuleUI.DOColor(containerImg, colorHide, 0.5f);
        DOTweenModuleUI.DOColor(roundText, colorHide, 1f);
    }

    private void ChangeText()
    {
        roundIndex++;

        switch (roundIndex)
        {
            case 1:
                roundText.text = "Round I";
                roundTextIcon.text = "I";
                break;
            case 2:
                roundText.text = "Round II";
                roundTextIcon.text = "II";
                break;
            case 3:
                roundText.text = "Round III";
                roundTextIcon.text = "III";
                break;
            case 4:
                roundText.text = "Round IV";
                roundTextIcon.text = "IV";
                break;
            case 5:
                roundText.text = "Round V";
                roundTextIcon.text = "V";
                break;
            default:
                roundText.text = "Round Endless";
                break;
        }
    }
}
