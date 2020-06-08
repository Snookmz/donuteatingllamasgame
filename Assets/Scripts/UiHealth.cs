using UnityEngine;
using UnityEngine.UI;

public class UiHealth : MonoBehaviour
{
    private static Text _healthText;
    
    void Start()
    {
        _healthText = gameObject.GetComponent<Text>();

    }

    public static void UpdateHealth(int health)
    {
        _healthText.text = "HEALTH + " + health;
    }
    
}
