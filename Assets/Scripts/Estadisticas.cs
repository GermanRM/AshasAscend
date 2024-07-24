using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estadisticas : MonoBehaviour
{

    public static Estadisticas Instance { get; private set; }

    

    public float jumpForce = 7, dañoPlayer = 15, vidaMaxima = 90;
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
    public void IncrementJumpForce(int amount)
    {
        jumpForce += amount;
    }

    public void IncrementDañoPlayer(int amount)
    {
        dañoPlayer += amount;
    }

    public void IncrementVidaMaxima(int amount)
    {
        vidaMaxima += amount;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
