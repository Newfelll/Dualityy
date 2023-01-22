using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : MonoBehaviour
{
    public Transform centerObject;
    [SerializeField] private float rotateSpeed = 5;
    public SpriteRenderer enemyRender;

    private Vector4 krem = new Vector4(1f, 0.8980392f, 0.8f, 1f);
    private Vector4 siyah = new Vector4(0.1294118f, 0.1254902f, 0.1254902f, 1f);
    private Vector3 zAxis = new Vector3(0, 0, 1);
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
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


    private void FixedUpdate()
    {
        transform.RotateAround(centerObject.position, zAxis, rotateSpeed);
    }
}
