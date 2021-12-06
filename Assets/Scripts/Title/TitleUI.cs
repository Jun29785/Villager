using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Define;
using TMPro;

public class TitleUI : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public GameObject TapToStart;
    public Slider loadingBar;

    public void SetLoadStateDescription(IntroPhase loadState)
    {
        switch (loadState)
        {
            case IntroPhase.Start:
                loadingText.text = "Start";
                break;
            case IntroPhase.ApplicationSetting:
                loadingText.text = "ApplicationSetting";
                break;
            case IntroPhase.StaticData:
                loadingText.text = "StaticData";
                break;
            case IntroPhase.UserData:
                loadingText.text = "UserData";
                break;
            case IntroPhase.Complete:
                loadingText.text = "Complete";
                break;
            default:
                break;
        }
    }

    public IEnumerator LoadGaugeUpdate(float loadPer)
    {
        while (!Mathf.Approximately(loadingBar.value, loadPer))
        {
            loadingBar.value = Mathf.Lerp(loadingBar.value, loadPer, Time.deltaTime * 2f);

            yield return null;
        }
    }

    public void TapToStartActive()
    {
        if (!TapToStart.activeSelf)
        {
            TapToStart.SetActive(true);
        }
    }
}
