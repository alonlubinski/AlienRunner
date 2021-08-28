using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        GameObject target = gameObject.transform.parent.gameObject;
        bool targetIsAlive = target.GetComponent<AlienAI>().IsAlive();
        if (targetIsAlive)
        {
            hitPoints -= damage;
            Debug.Log("Enemy have " + hitPoints + " health");
        }
        if (hitPoints <= 0 && targetIsAlive)
        {
            target.GetComponent<Animator>().SetTrigger("dead");
            target.GetComponent<NavMeshAgent>().enabled = false;
            target.gameObject.GetComponent<AlienAI>().Kill();
            target.gameObject.transform.parent.gameObject.GetComponent<EnemyDeathHandler>().CheckForWin();
        }
    }
}
