using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialTrigger : MonoBehaviour
{
    [SerializeField]
    private float speed = .05f;
    [SerializeField]
    public float enemyHealth = 10f;
    public float enemyMaxHealth = 10f;
    private float enemyRange = 3f;
    private float playerDistance;
    private Vector3 pdv;
    private Vector3 pdn;
    private bool moveNow;
    private Image healthBar;
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
    /*
    private void OnTriggerEnter(Collider other){
        enemyHealth--;
        Debug.Log("took damage! enemyHealth is "+enemyHealth);
    }
    */
    private void OnCollisionEnter(Collision bulletCollision){
        if(bulletCollision.gameObject.tag == "Hurts" && enemyHealth >=0){
            enemyHealth--;
            Debug.Log("took damage! enemyHealth is "+enemyHealth);
        }
        
    }
}
