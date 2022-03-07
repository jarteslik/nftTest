using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Screens;
    [SerializeField]
    AnimationCurve AnimCurve;
    [SerializeField]
    GameObject Logo, LogoScreen, PopupScreen,PopupDwnld,PopupError;
    [SerializeField]
    Image LogoText, LogoBack;

    private void Start()
    {
        StartCoroutine(Intro());
    }

    public void ShowScreen(GameObject choosedScreen)
    {
        GameObject tempScreen1 = null;
        GameObject tempScreen2 = null;
        foreach (GameObject screen in Screens)
        {
            if (screen == choosedScreen)
            {
                tempScreen1 = screen;
            }
            else
            {
                tempScreen2 = screen;
            }
        }
        StartCoroutine(ChangeScreen(tempScreen1, tempScreen2));
    }

    public void ShowPopup(bool toggle, string popupToShow)
    {
        GameObject showingPopup = popupToShow == "ERR" ? PopupError : PopupDwnld;
        StartCoroutine(ShowingPopup(toggle, showingPopup));
    }

    IEnumerator Intro()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.025f);
        float time = 0;
        Vector3 size = new Vector3(0, 0, 1);
        while (time < 1)
        {
            yield return delay;
            time += 0.025f;
            size.x = AnimCurve.Evaluate(time);
            size.y = AnimCurve.Evaluate(time);
            Logo.transform.localScale = size;
        }

        yield return new WaitForSecondsRealtime(1f);

        time = 0;
        Color fadeColor = new Color(1, 1, 1, 1);
        while (time < 1)
        {
            yield return delay;
            time += 0.025f;
            fadeColor.a -= 0.025f;
            LogoText.color = fadeColor;
            LogoBack.color = fadeColor;
        }
        LogoScreen.SetActive(false);

    }

    IEnumerator ShowingPopup(bool toggle, GameObject showingPopup)
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.025f);
        float time = toggle ? 0 : 1;
        Vector3 size = new Vector3(toggle ? 0 : 1, toggle ? 0 : 1, 1);
        if (toggle)
        {
            PopupScreen.SetActive(toggle);
            showingPopup.SetActive(true);
            while (time < 1)
            {
                time += 0.025f;
                size.x = AnimCurve.Evaluate(time);
                size.y = AnimCurve.Evaluate(time);
                showingPopup.transform.localScale = size;
                yield return delay;
            }
        }
        else 
        {
            while (time > 0)
            {
                time -= 0.025f;
                size.x = AnimCurve.Evaluate(time);
                size.y = AnimCurve.Evaluate(time);
                showingPopup.transform.localScale = size;
                yield return delay;
            }
            showingPopup.SetActive(false);
            PopupScreen.SetActive(false);
        }
    }

    IEnumerator ChangeScreen(GameObject currentScreen, GameObject nextScreen)
    {
        float time = 0;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.01f);
        Vector3 startPos = currentScreen.transform.position;
        Vector3 endPos = nextScreen.transform.position;
        while (time < 1)
        {
            yield return delay;
            time += 0.025f;
            currentScreen.transform.position = Vector3.Lerp(startPos, endPos, time);
            nextScreen.transform.position = Vector3.Lerp(endPos, startPos, time);
        }
    }

    static ScreenController instance;
    public static ScreenController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScreenController>();
            }
            return instance;
        }
    }
}
