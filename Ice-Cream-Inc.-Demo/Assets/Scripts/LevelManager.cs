using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Material chocolate, vanillia;
    TransformGenerator transformGenerator;
    CreamGenerator creamGenerator;
    List<Material> userAnswer;
    List<Material> answer;
    int trues = 0;
    float percentage;
    [SerializeField] Image levelImage;
    [SerializeField] Sprite[] images;
    [SerializeField] GameObject panel;
    [SerializeField] Text matchRateTxt,levelTxt;
    [SerializeField]Slider slider;
    float targetProgress = 0;
    int c=0;
    void Start()
    {
        userAnswer = new List<Material>();
        answer = new List<Material>();

        transformGenerator = FindObjectOfType<TransformGenerator>();
        creamGenerator = FindObjectOfType<CreamGenerator>();
        levelImage.sprite = images[CreamGenerator.currentLevel - 1];

        levelTxt.text = "Level" + CreamGenerator.currentLevel;

    }
    void Update()
    {
        IncrementProgress(percentage);
    }
    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value = newProgress;
    }
    public void CalculateMatch()
    {
        
        answer.Clear();
        panel.SetActive(true);
        userAnswer = creamGenerator.getAnswer();
        
        if (CreamGenerator.currentLevel == 1)
            FillAnswer1();
        else if (CreamGenerator.currentLevel == 2)
            FillAnswer2();

        for (int i = 0; i < userAnswer.Count; i++)
        {
            if (userAnswer[i].name.Contains(answer[i].name))
                trues++;
        }

         percentage= (float)trues / answer.Count * 100f;

        matchRateTxt.text= "% "+percentage.ToString();
        levelImage.sprite = images[CreamGenerator.currentLevel - 1];

    }
    void FillAnswer1()
    {
        for (int i = 0; i < userAnswer.Count; i++)
        {
            answer.Add(chocolate);
        }
    }
    void FillAnswer2()
    {
        answer.Clear();
        for (int i = 0; i < TransformGenerator.baseTransformCount[0] +
                            TransformGenerator.baseTransformCount[1] +
                             TransformGenerator.baseTransformCount[2]; i++)
        {
             answer.Add(chocolate);
        }
        for (int i = 0; i < TransformGenerator.baseTransformCount[3] +
                           TransformGenerator.baseTransformCount[4] +
                             TransformGenerator.baseTransformCount[5] +
                             TransformGenerator.baseTransformCount[6]; i++)
        {
            answer.Add(vanillia);
        }

    }
    public void NextLevel()
    {
        trues = 0;
        percentage = 0f;
        Debug.Log(CreamGenerator.currentLevel);
        levelImage.sprite = images[CreamGenerator.currentLevel - 1];
        answer.Clear();
        userAnswer.Clear();
        levelTxt.text = "Level" + CreamGenerator.currentLevel;
        panel.SetActive(false);
    }
}
