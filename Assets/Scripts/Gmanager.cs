using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gmanager : MonoBehaviour
{
    public float[] totalClicks;
    public TMP_Text[] totalClicksText;
    public GameObject[] stats;
    public void AddClicks()
    {
        totalClicks[0]++;
        totalClicksText[0].text = totalClicks[0].ToString("0");
        stats[0] = EventSystem.current.currentSelectedGameObject;
        if (stats[0].tag == stats[1].tag)
        {
            totalClicks[1]++;
            totalClicksText[1].text = totalClicks[1].ToString("0");
        }
        else if (stats[0].tag == stats[2].tag)
        {
            totalClicks[2]++;
            totalClicksText[2].text = totalClicks[2].ToString("0");
        }
        else if (stats[0].tag == stats[3].tag)
        {
            totalClicks[3]++;
            totalClicksText[3].text = totalClicks[3].ToString("0");
        }
    }
}
