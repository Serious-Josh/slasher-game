using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float runSpeed;
    public Animator currAnimator;
    public CharacterController controller;
    public PlayerMovementTank player;

    public LayerMask playerMask;
    public LayerMask environmentMask;

    public Transform visibleTarget = null;

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {

        visibleTarget = null;
        //normally the Physics.blahblahblah returns an array, but since there's only ever one player, this is just using the first entry by default
        Transform target = Physics.OverlapSphere(transform.position, viewRadius, playerMask)[0].transform;

        Vector3 direction = (target.position - transform.position).normalized;

        if(Vector3.Angle(transform.forward, direction) < viewAngle / 2)
        {
            Vector3 movement;
            float distance = Vector3.Distance(transform.position, target.position);

            if (!Physics.Raycast(transform.position, direction, distance, environmentMask))
            {

                if (distance < 0.65 && currAnimator.GetBool("Attack") == false)
                {
                    currAnimator.ResetTrigger("Idle");
                    currAnimator.ResetTrigger("Walk");
                    currAnimator.ResetTrigger("Sprint");
                    currAnimator.SetTrigger("Attack");
                }
                else if (distance < 0.65)
                {
                    //blank space lol
                }
                else
                {
                    currAnimator.ResetTrigger("Idle");
                    currAnimator.ResetTrigger("Walk");
                    currAnimator.SetTrigger("Sprint");

                    if (currAnimator.GetBool("Attack") == false)
                    {
                        transform.LookAt(target);
                        movement = transform.forward * runSpeed;
                        controller.Move(movement * Time.deltaTime - Vector3.up * 1);
                    }
                }
            }
            else
            {
                currAnimator.ResetTrigger("Sprint");
                currAnimator.ResetTrigger("Walk");
                currAnimator.SetTrigger("Idle");
            }

        }

    }

    private void Stab()
    {
        player.playerDamage();
    }

    private void Reset()
    {
        currAnimator.ResetTrigger("Attack");
        currAnimator.ResetTrigger("Idle");
        currAnimator.ResetTrigger("Walk");
        currAnimator.SetTrigger("Sprint");
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {

        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }

}
