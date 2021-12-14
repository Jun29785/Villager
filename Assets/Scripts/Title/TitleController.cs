using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    private bool loadComplete;

    public bool LoadComplete
    {
        get => loadComplete;
        set
        {
            loadComplete = value;
            if (loadComplete && !allLoaded)
                NextPhase();
        }
    }

    public static bool allLoaded;

    IntroPhase phase = IntroPhase.Start;

    public TitleUI UI;

    Coroutine loadGaugeUpdateCoroutine;

    public void Initialize()
    {
        OnPhase(phase);
    }

    private void OnPhase(IntroPhase phase)
    {
        UI.SetLoadStateDescription(phase);
        if(loadGaugeUpdateCoroutine != null)
        {
            StopCoroutine(loadGaugeUpdateCoroutine);
            loadGaugeUpdateCoroutine = null;
        }
        if (phase != IntroPhase.Complete)
        {
            var loadPer = (float)phase / (float)IntroPhase.Complete;
            loadGaugeUpdateCoroutine = StartCoroutine(UI.LoadGaugeUpdate(loadPer));

        }
        else
        {
            UI.loadingBar.value = 1f;
        }
        switch (phase)
        {
            case IntroPhase.Start:
                LoadComplete = true;
                break;
            case IntroPhase.UserData:
                UserDataManager.Instance.LoadUserData();
                LoadComplete = true;
                break;
            case IntroPhase.StaticData:
                DataBaseManager.Instance.LoadTable();
                LoadComplete = true;
                break;
            case IntroPhase.ApplicationSetting:
                GameManager.Instance.ApplicationSetting();
                LoadComplete = true;
                break;
            case IntroPhase.Complete:
                allLoaded = true;
                LoadComplete = true;
                break;
        }
    }

    private void NextPhase()
    {
        StartCoroutine(WaitForSeconds());

        IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(0.7f);

            loadComplete = false;
            OnPhase(++phase);
            
        }
    }
}
