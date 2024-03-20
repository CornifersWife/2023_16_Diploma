using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public float time;
    public GameObject Player;

    public GameObject target;
    public float speed;

    private Vector3 targetVector;
    // Start is called before the first frame update
    void Start()
    {
        targetVector = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());
        Move(Player);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
    }

    private void Move(GameObject player)
    {
        // Maintain distance from player to floor
        float playerDistanceToFloor = transform.position.y - targetVector.y;
        targetVector.y += playerDistanceToFloor;
        
        Vector3 direction = targetVector - transform.position;
        Vector3 movement = speed * Time.deltaTime * direction.normalized;
        player.GetComponent<CharacterController>().Move(movement);

        direction.y = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), speed *Time.deltaTime);
        targetVector = direction;
    }
}
