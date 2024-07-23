using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiManager : MonoBehaviour
{
    public List<GameObject> listaKunais;
    public GameObject kunaiPrefab;
    public GameObject spawnKunai;
    public int tamañoList;



    private static KunaiManager instance;
    public static  KunaiManager Intance {get {return instance;}}

    private void Awake() {
            if ( instance == null )
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
    }
    private void Start() {
        AñadirKunais(tamañoList);
    }

    public void AñadirKunais(int amount)
    {
        for(int i = 0 ; i < amount; i++)
        {
            GameObject kunai = Instantiate(kunaiPrefab);
            kunai.SetActive(false);
            listaKunais.Add(kunai);
            kunai.transform.parent = spawnKunai.transform;
        }
    }
    public GameObject SpawnearKunais()
    {
        for(int i = 0; i < listaKunais.Count; i++)
        {
            if(!listaKunais[i].activeSelf)
            {
                listaKunais[i].SetActive(true);
                return listaKunais[i];
            }
        }
        AñadirKunais(1);
        listaKunais[listaKunais.Count - 1].SetActive(true);
        return listaKunais[listaKunais.Count - 1];
    }

}
