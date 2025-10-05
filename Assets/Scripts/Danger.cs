using UnityEngine;

public class Danger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().isAngel = true;
            collision.GetComponent<Player>().anim.SetBool("angel", true);
        }
    }
}
