using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    #region Player
    [SerializeField]
    private GameObject player;
    public Rigidbody rb;
    #endregion

    #region Movement
    [Header("Movement Values")]
    [SerializeField, Range(0.0f, 100f)]
    private float speed = 15.0f;

    [SerializeField, Range(0.0f, 100f)]
    private float jumpForce = 10.0f;

    [SerializeField, Range(0.0f, 100f)]
    private float turnSpeed = 45.0f;
    [SerializeField, Range(0.0f, 100f)]
    private float turnMultiplier = 4.0f;

    [Space(10)]
    #endregion

    #region Camera
    [Tooltip("Script for camera movement")]
    public camera camScript;
    #endregion

    #region Input
    [HideInInspector]
    public Vector2 moveInput;
    [HideInInspector]
    public float jumpInput;

    #endregion

    [SerializeField]
    private audioManager audioManager;
    [SerializeField, Range(0.0f, 1.0f)]
    private float jumpSfxVol = 0.5f;


    private float distToGround = 1f;
    [SerializeField]
    bool isGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody>();
        camScript = this.GetComponent<camera>();
        distToGround = player.transform.localScale.y;
        audioManager = this.GetComponent<audioManager>();
    }
    void FixedUpdate()
    {
        moveInput.x = Input.GetAxis("Vertical");
        moveInput.y = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        if (Physics.Raycast(player.transform.position, Vector3.down, distToGround + 0.1f))
        {
            //Debug.Log("Grounded");
            isGrounded = true;
        }
        else
        {
            //Debug.Log("Airborne");
            isGrounded = false;
        }
        
    }
    private void Update()
    {
        move(moveInput.x, moveInput.y);
        jump(jumpInput);
    }

    private void move(float inputX, float inputY)
    {
        //forward/backward movement
        player.transform.Translate(new Vector3(-inputX, 0, 0) * Time.deltaTime * speed);


        //strafe or spin
        if (camScript.rotatePlayer)
        {
            //rotate with camera
            player.transform.Translate(new Vector3(0, 0, inputY) * Time.deltaTime * speed);
        }
        else
        {
            //rotate with movement
            player.transform.Rotate(new Vector3(0, inputY, 0) * Time.deltaTime * (turnSpeed * turnMultiplier));
        }
    }

    private void jump(float jinp)
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2")) && isGrounded)
        {
            //jump
            rb.AddForce(player.transform.up * jumpForce, ForceMode.Impulse);
            audioManager.source.pitch = Random.Range(0.9f, 1.1f);
            audioManager.playSfx("jump", jumpSfxVol);

        }
    }
}
