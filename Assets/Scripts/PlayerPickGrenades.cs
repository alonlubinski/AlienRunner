using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickGrenades : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public GameObject grenadesOnFloor;
    [SerializeField] public GameObject grenadesInHand;
    private float distanceToPlayer = Mathf.Infinity;

    private void OnMouseDown()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(distanceToPlayer);
        if (!grenadesInHand.activeSelf && distanceToPlayer < 2.9f)
        {
            grenadesOnFloor.SetActive(false);
            grenadesInHand.SetActive(true);
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
