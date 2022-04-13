using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Set turning speed
    public float turnSpeed = 20f;
    // Call the animator component
    Animator m_Animator;
    // Call the rigidbody
    Rigidbody m_Rigidbody;
    // Audio source
    AudioSource m_AudioSource;
    // Character position : member movement
    Vector3 m_Movement;
    // Store rotations
    Quaternion m_Rotation =  Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame : Physics
    void FixedUpdate()
    {
        // Create new float variables and call horizontal and vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        // True if we get input
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        // Set the value of the animator parameter
        m_Animator.SetBool("IsWalking", isWalking);
        // Sound check
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }
        // Calculate character's forward vector
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        // Create a rotation looking in the direction of the given parameter
        m_Rotation =  Quaternion.LookRotation(desiredForward);
    }

    // Apply root motion : movement and rotation can be applied separately
    void OnAnimatorMove()
    {
        // Movement
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        // Rotation
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
