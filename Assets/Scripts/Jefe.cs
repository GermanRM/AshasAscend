using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D bossRb;
    public Transform jugador;
    [SerializeField] private float vida;
    [SerializeField] private float maximaVida;
    [SerializeField] private BarraVidaJefe barraVidaJefe;
    private bool mirandoDerecha = true;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField]private float dañoAtaque;
    
    public void Start()
    {
        animator =GetComponent<Animator>();
        bossRb = GetComponent<Rigidbody2D>();
        vida = maximaVida;
        barraVidaJefe.InicializadorDeBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        if(vida <= 0)
        {
            animator.SetTrigger("Muerte");
            
        }
    }
    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha =!mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }
    public void Ataque(){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);
        foreach(Collider2D collision in objetos)
        {
            if(collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().TomarDaño(dañoAtaque);
            }
        }
    }
     private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}
