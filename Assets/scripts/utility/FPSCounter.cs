using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    public Text fpsdisplay;

    void Start()
    {
        //StartCoroutine(QualityCheck());
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fpsdisplay.text = "FPS: "+(1.0f / deltaTime).ToString();
    }

    /*public IEnumerator QualityCheck()
    {
        int count = 0;
        while (count < 1000)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            if((1.0f / deltaTime) <= 5f)
            { 
                QualitySettings.SetQualityLevel(1, true);
            }
            if ((1.0f / deltaTime) <= 10f && (1.0f / deltaTime) > 5f)
            {
                QualitySettings.SetQualityLevel(2, true);
            }
            if ((1.0f / deltaTime) <= 20f && (1.0f / deltaTime) > 10f)
            {
                QualitySettings.SetQualityLevel(3, true);
            }
            if ((1.0f / deltaTime) <= 25f && (1.0f / deltaTime) > 20f)
            {
                QualitySettings.SetQualityLevel(4, true);
            }
            else
            {
                QualitySettings.SetQualityLevel(6, true);
            }
            yield return null;
            count++;
        }
    }*/
}