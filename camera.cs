using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class camera : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Vector3 cameraOffset = Vector3.zero;

    public Transform playerTransform;

    public bool rotatePlayer = true;


    private void Awake()
    {
        player = this.gameObject;
        playerTransform = player.transform;
        cameraTransform = playerCamera.transform;
    }

    private void FixedUpdate()
    {
        if (rotatePlayer)
        {
            playerTransform.eulerAngles = new Vector3(playerTransform.rotation.x,
            cameraTransform.localEulerAngles.y + 90,
            playerTransform.rotation.z);
        }
    }
}
