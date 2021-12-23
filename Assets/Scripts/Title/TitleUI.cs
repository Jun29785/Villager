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
    public TextMeshProUGUI InputName;

    private bool HasName = false;

    private void Update()
    {
        if(TitleController.allLoaded && HasName) { TapToStartActive(); }
    }

    public void SetLoadStateDescription(IntroPhase loadState)
    {
        switch (loadState)
        {
            case IntroPhase.Start:
                loadingText.text = "Start";
                break;
            case IntroPhase.StaticData:
                loadingText.text = "StaticData";
                break;
            case IntroPhase.UserData:
                loadingText.text = "UserData";
                break;
            case IntroPhase.ApplicationSetting:
                loadingText.text = "ApplicationSetting";
                break;
            case IntroPhase.Complete:
                loadingText.text = "Complete";
                HasName = true;
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
        if (!TapToStart.activeSelf && !UserDataManager.Instance.NameInput.activeSelf)
        {
            TapToStart.SetActive(true);
        }
    }

    public void SumitNameInput()
    {
        if (InputName.text.Length < 3)
        {
            // Warn Text : "닉네임은 3글자 이상이어야 됩니다.
            return;
        }
        var user = UserDataManager.Instance;
        user.userData.UserName = InputName.text;
        user.NameInput.SetActive(false);
        StartCoroutine(user.SaveData());
        HasName = true;
    }
}
