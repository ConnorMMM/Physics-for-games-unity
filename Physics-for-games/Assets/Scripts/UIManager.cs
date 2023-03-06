using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI finalTimer;
    public TextMeshProUGUI death;
    public TextMeshProUGUI sorry;

    private float time = 0;
    private bool isRunning = false;

    private int deathCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = "00:00";
        finalTimer.gameObject.SetActive(false);
        sorry.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        death.text = $"Deaths: {deathCount}";

        if (isRunning)
        {
            time += Time.deltaTime;
        }

        if (time > 0 && isRunning)
        {
            string min = $"{(int)time / 60}";
            if ((int)time / 60 < 10)
                min = $"0{(int)time / 60}";

            string sec = $"{(int)time % 60}";
            if ((int)time % 60 < 10)
                sec = $"0{(int)time % 60}";

            timer.text = $"{min}:{sec}";
        }


    }

    public void StartTimer() { isRunning = true; }
    public void StopTimer() 
    {
        if (!isRunning)
        {
            timer.gameObject.SetActive(false);
            sorry.gameObject.SetActive(true);
            return;
        }

        isRunning = false;
        timer.gameObject.SetActive(false);
        finalTimer.gameObject.SetActive(true);

        string min = $"{(int)time / 60}";
        if ((int)time / 60 < 10)
            min = $"0{(int)time / 60}";

        string sec = $"{(int)time % 60}";
        if ((int)time % 60 < 10)
            sec = $"0{(int)time % 60}";

        finalTimer.text = $"{min}:{sec}";
    }

    public void AddDeath() { deathCount++; }
}
