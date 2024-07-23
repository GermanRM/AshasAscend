using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeHabilidadBehaviour : StateMachineBehaviour
{
    [SerializeField] GameObject habilidad;
    private Jefe jefe;
    private KunaiManager kunaiManager;
    [SerializeField] Transform spawnKunais;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe = animator.GetComponent<Jefe>();
        jefe.MirarJugador();
        kunaiManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<KunaiManager>();
        spawnKunais = kunaiManager.spawnKunai.transform;
        GameObject proyectil = kunaiManager.SpawnearKunais();
        proyectil.transform.rotation = spawnKunais.transform.rotation;
        proyectil.transform.position = spawnKunais.transform.position;

       


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
