using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move speed
    public float moveSpeed;
    // check for moving
    private bool isMoving;
    // get keyboard input
    private Vector2 input;

    // layers
    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;

    private void Update()
    {
        if (!isMoving)
        {
            // handles movement and towards target postion
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            //remove diagonal movement
            if (input.x != 0) input.y = 0;
            
            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
            
        }
    }
    
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        
        
        // check to find target postion and if not there then move
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }
    
    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            //chance of encounter 1 in 10
            if (Random.Range(1, 101) <= 10)
            {
                //Debug.Log("encountered a monster");
            }
        }
    }
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

}
