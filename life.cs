using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class life : MonoBehaviour
{
    [SerializeField]
    private float maxLife = 3;

    public float currentLife = 3;

    public GameObject player;
    public Collider playerCollider;

    public TextMeshProUGUI counter;
    [Space(10)]
    public Vector3 spawnPos = new Vector3(0, 1, 0);

    [SerializeField]
    private audioManager audioManager;
    [SerializeField, Range(0.0f, 1.0f)]
    private float hurtSfxVol = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = this.gameObject;
        playerCollider = this.gameObject.GetComponent<Collider>();
        audioManager = this.gameObject.GetComponent<audioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = currentLife.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            audioManager.source.pitch = Random.Range(0.9f, 1.1f);
            audioManager.playSfx("hurt", hurtSfxVol);
            if (currentLife > 1)
            {
                Debug.Log("hit");
                currentLife -= 1;
            }
            else if (currentLife <= 1)
            {
                Debug.Log("die");
                currentLife = 3;
                player.transform.position = spawnPos;
                // run respawn script
            }
        }
        else if (collision.gameObject.layer == 7)
        {
            audioManager.source.pitch = Random.Range(0.9f, 1.1f);
            audioManager.playSfx("hurt", hurtSfxVol);
            Debug.Log("die");
            currentLife = 3;
            player.transform.position = spawnPos;
        }
    }
}
