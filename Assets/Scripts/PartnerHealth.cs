using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PartnerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] public TextMeshProUGUI hpText;
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
        //GameObject target = gameObject.transform.parent.gameObject;
        bool isAlive = GetComponent<PartnerAI>().IsAlive();
        if (isAlive)
        {
            hitPoints -= damage;
            Debug.Log("Partner have " + hitPoints + " health");
        }
        if (hitPoints <= 0 && isAlive)
        {
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<PartnerAI>().Kill();
        }
    }
}
