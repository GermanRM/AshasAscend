using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float tamañoRayCast, speedPlayer, ataquePorSec, vidaPlayer;
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

    // New variables for special effects
    [SerializeField] private GameObject attackEffectPrefab;
    [SerializeField] private GameObject jumpEffectPrefab;
    [SerializeField] private AudioSource attackSound;

    void Start()
    {
        detectorSuelo = GameObject.Find("DetectorSuelo");
        Physics2D.gravity *= 2;
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vidaPlayer = Estadisticas.Instance.vidaMaxima;
        barraVidaPlayer.InicializadorDeBarraDeVida(vidaPlayer);
        jefe = GameObject.FindGameObjectWithTag("Boss").GetComponent<Jefe>();

        // Initialize audio source if not set
        if (attackSound == null)
        {
            attackSound = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        if (Time.time >= siguienteAtaque)
        {
            if (Input.GetButtonDown("Fire1"))
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
        
        // Instantiate the attack effect at the attack point and destroy it after a delay
        if (attackEffectPrefab != null)
        {
            GameObject effect = Instantiate(attackEffectPrefab, puntoDeAtaque.transform.position, Quaternion.identity);
            Destroy(effect, 2f); // Adjust the duration to match your particle effect's length
        }

        // Play the attack sound
        if (attackSound != null)
        {
            attackSound.Play();
        }

        Collider2D[] pegarEnemigos = Physics2D.OverlapCircleAll(puntoDeAtaque.transform.position, rangoDeAtaque, enemyLayer);
        foreach (Collider2D enemy in pegarEnemigos)
        {
            jefe.TomarDaño(Estadisticas.Instance.dañoPlayer);
            Debug.Log("Pegaste a " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoDeAtaque == null)
            return;

        Gizmos.DrawWireSphere(puntoDeAtaque.transform.position, rangoDeAtaque);
    }

    public void TomarDaño(float daño)
    {
        vidaPlayer -= daño;
        barraVidaPlayer.CambiarVidaActual(vidaPlayer);
        if (vidaPlayer <= 0)
        {
            animator.SetTrigger("Die");
        }
    }

    private void Movimiento()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        if (inputHorizontal != 0)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            float movimientoVertical = playerRb.velocity.y;
            movimientoHorizontal = inputHorizontal * speedPlayer;
            Vector2 movimiento = new Vector2(movimientoHorizontal * speedPlayer, movimientoVertical);

            playerRb.velocity = movimiento;

            if (inputHorizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (inputHorizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }
    }

    private void Salto()
    {
        Debug.DrawLine(detectorSuelo.transform.position, detectorSuelo.transform.position + Vector3.down * tamañoRayCast, Color.red);
        if (Physics2D.Raycast(detectorSuelo.transform.position, Vector2.down, tamañoRayCast, layer))
        {
            dejarSaltar = true;
            if (Input.GetKeyDown(KeyCode.Space) && dejarSaltar != false)
            {
                if (jumpEffectPrefab != null)
                {
                    GameObject effect = Instantiate(jumpEffectPrefab, transform.position - new Vector3(0,1.3f,0), Quaternion.identity);
                    Destroy(effect, 2.0f); // Adjust the duration to match your particle effect's length
                }
                dejarSaltar = false;
                animator.SetBool("Idle", false);
                playerRb.AddForce(Vector2.up * Estadisticas.Instance.jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger("Jump");

                // Instantiate the jump effect at the player's position and destroy it after a delay
                /*if (jumpEffectPrefab != null)
                {
                    GameObject effect = Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
                    Destroy(effect, 2f); // Adjust the duration to match your particle effect's length
                }*/
            }
        }
    }
}