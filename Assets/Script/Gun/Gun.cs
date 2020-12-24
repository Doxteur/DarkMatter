using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Vector3 mousePosition;
    public AudioSource sound;
    public GameObject muzzleFlash;
    public LayerMask IgnoreMe;
    // Start is called before the first frame update
    void Start()
    {
      

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(IgnoreMe);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), Mathf.Infinity, ~IgnoreMe);
            GameObject go = Instantiate(muzzleFlash, transform.position, transform.rotation);
            Destroy(go, 0.05f);
            sound.Play();
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 50);

            if (hit.collider != null) 
            {
                Debug.Log(hit.transform.name);
            }

            if (hit.collider.CompareTag("zombie")) 
            {
                Destroy(hit.transform.gameObject);
            }
            if (hit.collider == null)
            {
                Debug.Log("Balle loupé");
            }

        }
    }
}
