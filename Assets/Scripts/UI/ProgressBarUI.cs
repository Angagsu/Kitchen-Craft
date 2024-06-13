using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image progressBarImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("GameObject " + hasProgressGameObject + " has no component that implemented IHasProgress Interface");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        progressBarImage.fillAmount = 0f;
        transform.forward = Camera.main.transform.forward;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        progressBarImage.fillAmount = e.ProgressNormalized;

        if (e.ProgressNormalized == 0 || e.ProgressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
