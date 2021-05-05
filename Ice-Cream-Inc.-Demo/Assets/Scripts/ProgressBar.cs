using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    Slider slider;
    float targetProgress = 0;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (slider.value<targetProgress)
        {
            slider.value += Time.deltaTime * 5;
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value = newProgress;
    }
}
