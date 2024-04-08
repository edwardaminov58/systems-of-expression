using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    //what the camera moves relative to
    [SerializeField] private Transform target;

    //lerp - smooth rotation
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private float _vertSpeed;
    //store collission data between functions
    private ControllerColliderHit _contact;

    private CharacterController _charController;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _vertSpeed = minFall;
        //accessing other components
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //start with 0,0,0 and progressively add movement
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        //only handle movement while arrow keys are pressed
        //horizontal movement
        if (horInput != 0 || vertInput != 0)
        {
            //movement.x = horInput;
            //movement.z = vertInput;
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            //keep initial rotation to restore after finishing with target
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            //transform movement direction from local to global coordinates
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            //LookRotation() calculates a quaternion facing in that direction
            //transform.rotation = Quaternion.LookRotation(movement);
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        //vertical movement
        //check if character on the ground
        //if on ground vertical speed reset to slight down movement
        //if jump button is pressed, set vertical speed high and reduced
        bool hitGround = false;
        RaycastHit hit;
        //using raycasting check if character is falling
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)//(_charController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            } else
            {
                _vertSpeed = minFall;//-1.5f
                _animator.SetBool("Jumping", false);
            }
        } else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
            if (_contact != null)
            {   // not right at level start
                _animator.SetBool("Jumping", true);
            }

            if (_charController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                } else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }
        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    // store collision to use in Update
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
}
