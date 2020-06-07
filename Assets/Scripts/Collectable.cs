using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject coinParticle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PerformCollection();
        }
    }

    private void PerformCollection()
    {
        var currentPosition = gameObject.transform.position;
        Instantiate(coinParticle, currentPosition, Quaternion.identity);
        Destroy(gameObject);
    }
    
    // private IEnumerator DestroyAfterDelay (GameObject ob, float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     Destroy(ob);
    //     // Destroy(_animator);
    // }
}