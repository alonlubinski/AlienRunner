using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickGun : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public GameObject gunOnFloor;
    [SerializeField] public GameObject gunInHand;
    [SerializeField] public GameObject reticle;
    [SerializeField] public GameObject pointer;
    private float distanceToPlayer = Mathf.Infinity;

    private void OnMouseDown()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(distanceToPlayer);
        if (!gunInHand.activeSelf && distanceToPlayer < 2.5f)
        {
            gunOnFloor.SetActive(false);
            pointer.SetActive(false);
            reticle.SetActive(true);
            gunInHand.SetActive(true);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
