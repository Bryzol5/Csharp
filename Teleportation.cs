using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
//    public GameObject Portal;
    public GameObject Player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Duszek");
        //Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
    }
}
