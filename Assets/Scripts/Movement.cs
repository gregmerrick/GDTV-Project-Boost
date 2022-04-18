using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketThrust = 1.0f;
    [SerializeField] float rotationThrust = 1.0f;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime); // Vector3 calculates direction and magnitude (speed and direction). Vector3.up is shorthand for 0,1,0. Relative force is relative to object not game world. **It wasn't flying because MASS was too heavy!!**  
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D)) // This one gets the else if because we're ok if you're boosting and pressing left, but you can't be going left and right at the same time.
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame) // We added this parameter so we can handle the positive and negative numbers to change direction on Z axis.
    {
        rb.freezeRotation = true; // We are freezing rotation of the physics systems before manually taking control below. It fixes the bug when hitting an object and it can't launch anymore afterward (acts weird dragging on ground etc.)
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
