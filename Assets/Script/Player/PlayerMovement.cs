using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 mousePosition;
    Vector2 movement;
    public Light2D light;
    public bool activeLight;
    public AudioSource audio;
    public GameObject mainCamera = null;

    //Disable component
    Behaviour[] componentsToDisable;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) { 
            mainCamera.SetActive(true);
            rb = GetComponent<Rigidbody2D>();
            activeLight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
        if (photonView.IsMine) { 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed);
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        if (Input.GetKeyDown(KeyCode.F))
        {
            light = GameObject.Find("Torch").GetComponent<Light2D>();
            if (activeLight)
            {
                light.intensity = 0;
                activeLight = false;
            }
            else
            {

                light.intensity = 0.21f;
                activeLight = true;
            }

        }


    }


    }
}
