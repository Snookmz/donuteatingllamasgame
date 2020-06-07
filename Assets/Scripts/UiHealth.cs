using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealth : MonoBehaviour
{
    private static Text _healthText;
    
    // Start is called before the first frame update
    void Start()
    {
        _healthText = gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateHealth(int health)
    {
        _healthText.text = "HEALTH + " + health;
    }
    
}
