using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float jumpForce = 7f;

    public Rigidbody rb;
    public LayerMask groundLayers;

    private CharacterController charCont;

    private bool isJumping = false;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        charCont = GetComponent<CharacterController>();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 move = new Vector3(deltaX, 0, deltaZ);
        move = Vector3.ClampMagnitude(move, speed); // Speed Limit

        move.y = gravity * 0.5f;

        move *= Time.deltaTime;     // Speed does not change depending on framerate
        move = transform.TransformDirection(move);
        charCont.Move(move);

        
        // Jumping
        if(!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
       
    }
    
    private IEnumerator JumpEvent()
    {
        charCont.slopeLimit = 90.0f;

        float airTime = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(airTime);
            charCont.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            airTime += Time.deltaTime;
            yield return null;

        } while (!charCont.isGrounded && charCont.collisionFlags != CollisionFlags.Above);

        charCont.slopeLimit = 45.0f;
        isJumping = false;
    }
}