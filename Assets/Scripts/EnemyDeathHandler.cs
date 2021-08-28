using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] Canvas youWonCanvas;

    // Start is called before the first frame update
    void Start()
    {
        youWonCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckWin()
    {
        bool enemy1StillAlive = enemy1.GetComponent<AlienAI>().IsAlive();
        bool enemy2StillAlive = enemy2.GetComponent<AlienAI>().IsAlive();
        if (!enemy1StillAlive && !enemy2StillAlive)
        {
            yield return new WaitForSeconds(3);
            // Both enemies are dead - player win
            youWonCanvas.enabled = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        yield return null;
    }

    public void CheckForWin()
    {
        StartCoroutine(CheckWin());
    }
}
