using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private GameObject audioGameObject;

    private float warningSoundTaimer;
    private float warningSoundTimerMax = 0.2f;
    private bool playWarningSound;


    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTaimer -= Time.deltaTime;
            if (warningSoundTaimer < 0)
            {
                warningSoundTaimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(transform.position);
            }
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProggersAmount = 0.5f;
        playWarningSound = stoveCounter.IsFried() && e.ProgressNormalized >= burnShowProggersAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStatChangedEventArgs e)
    {
        bool showOrHide = e.State == StoveCounter.State.Fried || e.State == StoveCounter.State.Frying;

        ShowOrHide(showOrHide);
    }

    private void ShowOrHide(bool showOrHide)
    {
        stoveOnGameObject.SetActive(showOrHide);
        particlesGameObject.SetActive(showOrHide);
        audioGameObject.SetActive(showOrHide);
    }
}
