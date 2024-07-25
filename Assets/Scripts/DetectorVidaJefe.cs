using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DetectorVidaJefe : MonoBehaviour
{
    private Jefe jefe;
    private PlayerController playerController;
    public GameObject panelCambioEscena;
    public GameObject panelLose;

    // Start is called before the first frame update
    void Start()
    {
        jefe = GameObject.FindGameObjectWithTag("Boss").GetComponent<Jefe>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jefe.vida <= 0)
        {
            panelCambioEscena.SetActive(true);
            Time.timeScale = 0;
        }
        if(playerController.vidaPlayer <= 0)
        {
            panelLose.SetActive(true);
        }

    }

    public void BotonCambio(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1;
    }
    public void BotonLose(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

}
