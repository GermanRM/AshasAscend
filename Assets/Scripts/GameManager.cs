using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float[] totalClicks;
    public TMP_Text[] totalClicksText;
    public GameObject[] stats;
    public GameObject[] indicadores;

    public float tiempo = 60f; // Duración del temporizador
    public TMP_Text tiempoText; // Texto UI para mostrar el temporizador

    private bool tiempoTerminado = false; // Variable para verificar si el tiempo ha terminado

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (!tiempoTerminado)
        {
            tiempo -= Time.deltaTime; // Restar el tiempo transcurrido

            if (tiempo <= 0)
            {
                tiempo = 0;
                tiempoTerminado = true;
            }

            tiempoText.text = "Tiempo: " + Mathf.Ceil(tiempo).ToString("0"); // Actualizar el texto del temporizador
        }
    }

    public void AddClicks()
    {
        if (tiempoTerminado)
            return; // No permitir clics si el tiempo ha terminado

        totalClicks[0]++;
        totalClicksText[0].text = totalClicks[0].ToString("0");
        stats[0] = EventSystem.current.currentSelectedGameObject;

        if (stats[0].tag == stats[1].tag)
        {
            totalClicks[1]++;
            totalClicksText[1].text = totalClicks[1].ToString("0");
            CheckAndActivateIndicator(1, totalClicks[1]);
            if (totalClicks[1] % 10 == 0)
            {
                Estadisticas.Instance.IncrementVidaMaxima(1);
            }
        }
        else if (stats[0].tag == stats[2].tag)
        {
            totalClicks[2]++;
            totalClicksText[2].text = totalClicks[2].ToString("0");
            CheckAndActivateIndicator(2, totalClicks[2]);
            if (totalClicks[2] % 10 == 0)
            {
                Estadisticas.Instance.IncrementDañoPlayer(1);
            }
        }
        else if (stats[0].tag == stats[3].tag)
        {
            totalClicks[3]++;
            totalClicksText[3].text = totalClicks[3].ToString("0");
            CheckAndActivateIndicator(3, totalClicks[3]);
            if (totalClicks[3] % 10 == 0)
            {
                Estadisticas.Instance.IncrementJumpForce(1);
            }
        }
    }

    private void CheckAndActivateIndicator(int statIndex, float clickCount)
    {
        if (clickCount % 10 == 0) // Check if clickCount is a multiple of 10
        {
            StartCoroutine(ActivateIndicator(statIndex - 1));
        }
    }

    private IEnumerator ActivateIndicator(int indicatorIndex)
    {
        indicadores[indicatorIndex].SetActive(true);
        yield return new WaitForSeconds(1f);
        indicadores[indicatorIndex].SetActive(false);
    }
}



