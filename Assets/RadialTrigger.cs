using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    [SerializeField]
    private float speed = .05f;
    private float enemyRange = 3f;
    private float playerDistance;
    private Vector3 pdv;
    private Vector3 pdn;
    private bool moveNow;
    void Update()
    {
        pdv = transform.position - GameObject.Find("Player").transform.position;
        playerDistance = Mathf.Sqrt(Mathf.Pow(pdv.x, 2f)+ Mathf.Pow(pdv.y, 2f)+Mathf.Pow(pdv.z, 2f));
        moveNow = playerDistance< enemyRange;
        pdn = (pdv)/playerDistance;
        if(moveNow){
            transform.position -= pdn*(speed * Time.deltaTime);
        }
    }
}
