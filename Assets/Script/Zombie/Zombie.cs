using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    GameObject TheGameController;
    public GameObject player;
    public Rigidbody2D rb;
    public bool follow;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        follow = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (follow) { 
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rb.rotation = angle - 90;
        direction.Normalize();
        movement = direction;
        }
    }
    private void FixedUpdate()
    {
        if (follow) {
        moveCharacter(movement);
        }
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * 1f * Time.deltaTime));
    }
}
