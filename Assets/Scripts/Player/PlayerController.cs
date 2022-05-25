using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    [Header("Movement Attributes")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float gravity = 9.81f;
    private float speed;
    
    [Header("Velocity Smoothness")]
    [SerializeField] private float speedAccTimeCooldown = 0.8f;
    private float speedAccTime = 0;
    
    private Rigidbody playerRB;
    private Transform cameraTransform;

    private void Start(){
        playerRB = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void Update(){
        MovePlayer();

        if(Input.GetKey(KeyCode.LeftShift))
            speed = sprintSpeed;
        else
            speed = walkSpeed;
    }

    private void MovePlayer(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        if((x != 0 || z != 0) && speedAccTime < speedAccTimeCooldown){
            speedAccTime += Time.deltaTime;
        } else if((x == 0 && z == 0) && speedAccTime > 0){
            speedAccTime -= Time.deltaTime;
        } else if(speedAccTime < 0) speedAccTime = 0;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Vector3 move = cameraTransform.right * x + transform.forward * z;

        if(x != 0 || z != 0)
            playerRB.velocity = new Vector3(Mathf.Clamp(move.x, -1f, 1f) * speed * speedAccTime / speedAccTimeCooldown, playerRB.velocity.y, Mathf.Clamp(move.z, -1f, 1f) * speed * speedAccTime / speedAccTimeCooldown);
        else
            playerRB.velocity = new Vector3(playerRB.velocity.x * speedAccTime / speedAccTimeCooldown, playerRB.velocity.y, playerRB.velocity.z * speedAccTime / speedAccTimeCooldown);

        playerRB.AddForce(Vector3.down * gravity * playerRB.mass, ForceMode.Force);
    }
}
