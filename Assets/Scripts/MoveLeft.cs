using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    
    [SerializeField] public static float speed=5;
    [SerializeField] public static float speedSaver=2;
    [SerializeField] private static int slowDownRate = 17;
    [SerializeField] private static int speedLimit = 30;
    [SerializeField] private static float speedUpRate = 1.3f;

    [SerializeField] private SpriteRenderer enemyRender;
    private Vector4 krem = new Vector4(1f, 0.8980392f, 0.8f, 1f);
    private  Vector4 siyah = new Vector4(0.1294118f, 0.1254902f, 0.1254902f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.y < 0)
        {
            enemyRender.color = siyah;
        }
        else
        {
            enemyRender.color = krem; transform.eulerAngles = new Vector3(0, 0, 180);
        }

      
    }

    // Update is called once per frame
    void Update()
    {
       

        if (transform.position.x < -25.51f)
        {
            Destroy(this.gameObject);
        }

        speedSaver += Time.deltaTime/4;

        if (speed >5)
        {
            speed -= (Time.deltaTime/slowDownRate);
        }

       
    }

    private void FixedUpdate()
    {
        if (GameManager.isPlaying)
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
    }
    public static void SpeedUp()
    {
        if (speed < speedLimit)
        {
            speed += (speedUpRate*Time.deltaTime);
        }
        
    }


}
