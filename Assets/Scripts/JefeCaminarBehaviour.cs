using UnityEngine;

public class JefeCaminarBehaviour : StateMachineBehaviour
{
    private Jefe jefe;
    private Rigidbody2D bossRb;
    [SerializeField] private float velocidadMovimiento;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe = animator.GetComponent<Jefe>();
        bossRb = jefe.bossRb;
        jefe.MirarJugador();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the player's position
        Vector2 playerPosition = jefe.GetPlayerTransform().position;

        // Calculate direction towards the player, but only horizontally
        Vector2 direction = new Vector2(playerPosition.x - animator.transform.position.x, 0).normalized;

        // Move the Jefe towards the player on the x-axis
        bossRb.velocity = new Vector2(direction.x * velocidadMovimiento, bossRb.velocity.y);

        // Ensure that Jefe faces the player
        jefe.MirarJugador();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossRb.velocity = new Vector2(0, bossRb.velocity.y);
    }
}
