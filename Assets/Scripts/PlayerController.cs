using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    Rigidbody thisRB;

    // Start is called before the first frame update
    void Start()
    {
        thisRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        thisRB.velocity = thisRB.rotation * new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 
                                      thisRB.velocity.y, 
                                      Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetKey(KeyCode.Space))
        {
            thisRB.AddForce(thisRB.transform.up * jumpForce);
        }
    }
}
