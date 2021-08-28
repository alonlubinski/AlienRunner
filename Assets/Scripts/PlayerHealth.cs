using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] public TextMeshProUGUI hpText;
    [SerializeField] Canvas bloodOnScreenCanvas;

    // Start is called before the first frame update
    void Start()
    {
        bloodOnScreenCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        bloodOnScreenCanvas.enabled = true;
        StartCoroutine(hideBloodOnScreenCanvas());
        hitPoints -= damage;
        Debug.Log("You suffer " + damage + " damage, now you have " + hitPoints + " hp.");
        if(hitPoints <= 0)
        {
            hitPoints = 0;
            GetComponent<DeathHandler>().HandleDeath();
        }
        hpText.text = hitPoints.ToString();
    }

    IEnumerator hideBloodOnScreenCanvas()
    {
        yield return new WaitForSeconds(1);
        bloodOnScreenCanvas.enabled = false;
    }
}
