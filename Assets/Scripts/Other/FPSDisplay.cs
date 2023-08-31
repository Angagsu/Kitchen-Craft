using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textFPS;
    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    private void Update()
    {
        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            textFPS.text = frameRate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
