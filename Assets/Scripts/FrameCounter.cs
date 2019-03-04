using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FrameCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayScore;
    [SerializeField] private TextMeshProUGUI displayFps;
    private void Start()
    {
        StartCoroutine(Fps());
    }

    private void Update()
    {
        Global.FrameCount++;
    }
    
    private IEnumerator Fps()
    {
        while (true)
        {
            Display();
            Global.FrameCount = 0;
            yield return new WaitForSeconds(1.0f);
        }
    }
    
    public void Display()
    {
        displayFps.text = "FPS: "+Convert.ToString(Global.FrameCount);
        displayScore.text = "Score: "+Convert.ToString(Global.Score);
    }
}