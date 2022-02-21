using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _customer;

    [SerializeField]
    float interval;

   public bool Spawned;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCustomer();
        Spawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Spawned)
        {
            StartCoroutine(Spawnroutine());
            Spawned = true;
        }

    }

    public void SpawnCustomer()
    {
        GameObject customer = Instantiate(_customer, transform.position, transform.rotation);
        customer.GetComponent<Customer>().customerSpawner = this;
       
    }

    public IEnumerator Spawnroutine()
    {

        yield return new WaitForSeconds(interval);
        SpawnCustomer();

    }
}
