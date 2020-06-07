using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private static readonly int Fade = Shader.PropertyToID("_Fade");

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Dissolver(Material material, float health)
    {
        health = health / 5;
        material.SetFloat(Fade, health);
    }
    
}
