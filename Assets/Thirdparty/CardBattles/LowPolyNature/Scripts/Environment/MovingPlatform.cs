using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    public float Speed = 1.5f;

    private int mCurrentIndex;

    public Transform[] Positions;

    private PlayerController mPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentIndex = 0;

        transform.position = GetCurrentTarget();
    }

    private Vector3 GetCurrentTarget()
    {
        if (Positions.Length == 0)
            return transform.position;

        return Positions[mCurrentIndex].position;
    }


    void Update()
    {
        var targetPos = GetCurrentTarget() - transform.position;

        var moveDir = targetPos.normalized * Speed * Time.deltaTime;

        if(targetPos.magnitude < 0.05f || moveDir.magnitude > targetPos.magnitude )
        {
            if (++mCurrentIndex == Positions.Length) mCurrentIndex = 0;
            moveDir = targetPos;
        }

        if(mPlayer != null)
        {
            // Set the external movement for the player
            // so that this can be applied in the character controller
            mPlayer.ExternalMovement = moveDir;
        }

        transform.position += moveDir;

    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            var height = transform.GetComponent<MeshRenderer>().bounds.extents.y;

            var platformY = transform.position.y + height - 0.1f;

            if(other.transform.position.y > platformY)
            {
                mPlayer = player;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && mPlayer != null)
        {
            mPlayer.ExternalMovement = Vector3.zero;
            mPlayer = null;
        }
    }




}
