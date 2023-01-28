using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private Rigidbody rb;
    private Vector3 input;
    private bool isJumping;
    private float score = 0;
    public Texture melon;
    

    private void Awake(){
        rb = GetComponent<Rigidbody>();       
    }

    private void OnTriggerEnter(Collider other){
        if ( other.gameObject.CompareTag("Collectable")){
            Destroy(other.gameObject);
            score += 1;
        }
    }

    private void FixedUpdate(){
        rb.velocity = rb.velocity.y * Vector3.up + input * speed;
        if (isJumping){
            isJumping = false;
            rb.AddForce(speed * Vector3.up, ForceMode.Impulse);
        }
        if (score >= 3){
            GetComponent<Renderer>().material.SetTexture("_MainTex", melon);
        }
    }

    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(Input.GetButtonDown("Jump")){
            isJumping = true;
        }
    }
}
