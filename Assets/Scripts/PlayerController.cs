using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float jumpForce,tamañoRayCast,speedPlayer, ataquePorSec, dañoPlayer, vidaPlayer, vidaMaxima;
    public float rangoDeAtaque = 1.5f;
    public LayerMask enemyLayer;
    private float movimientoHorizontal;
    private Rigidbody2D playerRb;
    private Animator animator;
    private float siguienteAtaque;
    [SerializeField] private float inputHorizontal;
    [SerializeField] GameObject detectorSuelo, puntoDeAtaque;
    [SerializeField] LayerMask layer;
    [SerializeField] private BarraVidaJefe barraVidaPlayer;
    [SerializeField] bool dejarSaltar = false;
    [SerializeField] private float cooldownSalto;
    [SerializeField] private Jefe jefe;
    // Start is called before the first frame update
    void Start()
    {
        detectorSuelo = GameObject.Find("DetectorSuelo");
        Physics2D.gravity *= 2;
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vidaPlayer = vidaMaxima;
        barraVidaPlayer.InicializadorDeBarraDeVida(vidaPlayer);
        jefe = GameObject.FindGameObjectWithTag("Boss").GetComponent<Jefe>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        if(Time.time >= siguienteAtaque)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                PlayerAttack();
                siguienteAtaque = Time.time + 1f / ataquePorSec;
            }
        }

        Salto();
    }
    private void FixedUpdate() 
    {
        Movimiento();
    }
        private void PlayerAttack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] pegarEnemigos = Physics2D.OverlapCircleAll(puntoDeAtaque.transform.position, rangoDeAtaque, enemyLayer);
        foreach(Collider2D enemy in pegarEnemigos)
        {
            jefe.TomarDaño(dañoPlayer);
            Debug.Log("Pegaste a " + enemy.name);
        }
    }
    private void OnDrawGizmosSelected() {
        if(puntoDeAtaque == null )
        return;

        Gizmos.DrawWireSphere(puntoDeAtaque.transform.position, rangoDeAtaque);
        
    }

    public void TomarDaño(float daño)
    {
        vidaPlayer -= daño;
        barraVidaPlayer.CambiarVidaActual(vidaPlayer);
        if(vidaPlayer <= 0)
        {
            animator.SetTrigger("Muerte");
            
        }       
    }
        private void Movimiento()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        if(inputHorizontal != 0)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            float movimientoVertical = playerRb.velocity.y;
            movimientoHorizontal = inputHorizontal*speedPlayer;
            Vector2 movimiento = new Vector2 (movimientoHorizontal * speedPlayer, movimientoVertical);
            
            playerRb.velocity = movimiento;
            


            if(inputHorizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if(inputHorizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        
        }
        else{
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }
    }
        private void Salto()
    {
        
        Debug.DrawLine(detectorSuelo.transform.position, detectorSuelo.transform.position + Vector3.down * tamañoRayCast, Color.red);
        if(Physics2D.Raycast(detectorSuelo.transform.position, Vector2.down, tamañoRayCast, layer))
        {
            dejarSaltar = true;
            if(Input.GetKeyDown(KeyCode.Space) && dejarSaltar != false)
            {
                dejarSaltar = false;
                animator.SetBool("Idle", false);
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger("Jump");
            }
        }
    }
}
