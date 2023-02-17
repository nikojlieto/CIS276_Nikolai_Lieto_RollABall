using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private LayerMask groundCollisionLayerMask;
    private Rigidbody rb;
    private Vector3 input;
    private bool isJumping;
    private float score = 0;
    [SerializeField]
    public Texture melon;
    private float distanceToGround;
    [SerializeField]
    private Transform grabPoint;
    private Grabbable selectedGrabbable;

    private void OnDrawGizmos(){
        Color raycastColor;
        if(isGrounded()){
            raycastColor = Color.green;
        } else {
            raycastColor = Color.red;
        }
        Vector3 startPosition = transform.position + (distanceToGround - 0.05f)* Vector3.down;
        Vector3 endPosition = startPosition + 0.3f * Vector3.down;
        Debug.DrawLine(startPosition, endPosition, raycastColor);
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();  
        distanceToGround = GetComponent<Collider>().bounds.extents.y;     
    }

    private bool isGrounded(){
        Vector3 startPosition = transform.position + (distanceToGround -0.05f)* Vector3.down;
        return Physics.Raycast(startPosition, Vector3.down, 0.3f, groundCollisionLayerMask);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Collectable")){
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
            //this part does not work ;_; i want it to turn to a melon
            GetComponent<Renderer>().material.SetTexture("_MainTex", melon);
            Debug.Log("you win!");
        }
    }

    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(Input.GetButtonDown("Jump") && isGrounded()){
            isJumping = true;
        }
        if(input.magnitude != 0){
            transform.forward = input;
        }

        if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, 1f, transform.forward,
            out RaycastHit hitInfo, maxDistance: 1f, preview: RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Editor) 
            && hitInfo.transform.TryGetComponent(out Grabbable grabbable) && Input.GetKey(KeyCode.Z)){
            selectedGrabbable = grabbable;
            grabbable.Grab(grabPoint);
        }
        if(Input.GetKeyDown(KeyCode.C) && selectedGrabbable){
            selectedGrabbable.Drop();
            selectedGrabbable = null;
        }
    }
}
