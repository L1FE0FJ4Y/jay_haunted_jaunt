using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    
    bool m_IsPlayerInRange;
    Vector3 angles;

    void Start()
    {
        angles = new Vector3(0.0f, 1.0f, 0.0f);
    }

    // Detection trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }
    
    // Leaving trigger
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Lemon position minus the object; Vector.up = (0, 1, 0)
        Vector3 direction = player.transform.position - transform.position + Vector3.up;
        if (m_IsPlayerInRange)
        {
            Ray ray = new Ray(transform.position, direction);

            // Store whatever ray hits
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
        direction.Normalize();
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(Vector3.forward, direction));
        angles.y = angle;
        transform.eulerAngles = angles;
    }
}
