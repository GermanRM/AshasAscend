using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D bossRb;
    public Transform jugador;
    public LayerMask enemyLayer;
    [SerializeField] public float vida;
    [SerializeField] private float maximaVida;
    [SerializeField] private BarraVidaJefe barraVidaJefe;
    [SerializeField] private PlayerController playerController;
    private bool mirandoDerecha = true;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque = 20;

    // Add a public variable for speed
    [Header("Movimiento")]
    public float velocidadMovimiento = 12f;
    
    public void Start()
    {
        animator = GetComponent<Animator>();
        bossRb = GetComponent<Rigidbody2D>();
        vida = maximaVida;
        barraVidaJefe.InicializadorDeBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("DistaciaJugador", distanciaJugador);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        barraVidaJefe.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            Destroy(gameObject, 1f);
            animator.SetTrigger("Die");
        }
    }

    private void Muerte()
    {
        Destroy(gameObject, 1f);
    }

    public void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Ataque()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque, enemyLayer);
        foreach (Collider2D enemy in player)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<PlayerController>().TomarDaño(dañoAtaque);
                Debug.Log("Pegaste a " + enemy.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }

    // Method to get the player's transform
    public Transform GetPlayerTransform()
    {
        return jugador;
    }
}