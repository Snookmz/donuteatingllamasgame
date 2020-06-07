// using UnityEngine;
//
// public class Grounded : MonoBehaviour
// {
//     private GameObject _playerMovement;
//     
//     // Start is called before the first frame update
//     void Start()
//     {
//         _playerMovement = gameObject.transform.parent.gameObject; 
//     }
//
//     private void OnCollisionEnter2D(Collision2D other)
//     {
//         if (other.collider.CompareTag("Tilemap"))
//         {
//             Debug.Log("Grounded, isGrounded true");
//             _playerMovement.GetComponent<PlayerMovement>().isGrounded = true;
//         }
//     }
//
//     private void OnCollisionExit2D(Collision2D other)
//     {
//         if (other.collider.CompareTag("Tilemap"))
//         {
//             Debug.Log("Grounded, has left ground");
//             _playerMovement.GetComponent<PlayerMovement>().isGrounded = false;
//         }
//     }
// }
