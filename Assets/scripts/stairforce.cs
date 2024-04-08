using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairforce : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();
        Rigidbody rb = GetComponent<Rigidbody>();
        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);

        if ((vertInput != 0 || horizInput != 0) && OnSlope())
            rb.AddForce(Vector3.down * capsule.height / 2 * slopeForce * Time.deltaTime);


    }

    private bool OnSlope()
    {
      

        RaycastHit hit;
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();
        if (Physics.Raycast(transform.position, Vector3.down, out hit, capsule.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }
}
