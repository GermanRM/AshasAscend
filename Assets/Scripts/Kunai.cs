using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private PlayerController playerController;
    private float speed = 7;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Suelo") || other.CompareTag("Player"))
        {
            if(other.CompareTag("Player"))
            {
                playerController.TomarDaño(15);
                gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}
