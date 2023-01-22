using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stomper : MonoBehaviour
{
    public ParticleSystem stompParticle;
    private bool particlePlay = false;

    [SerializeField] private SpriteRenderer stompRender;
    private Vector4 krem = new Vector4(1f, 0.8980392f, 0.8f, 1f);
    private Vector4 siyah = new Vector4(0.1294118f, 0.1254902f, 0.1254902f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.y < 0)
        {
            stompParticle.startColor = siyah;
            stompRender.color = siyah;
        }
        else
        {
            stompRender.color = krem; transform.eulerAngles = new Vector3(0, 0, 180); stompParticle.startColor = krem;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

   
