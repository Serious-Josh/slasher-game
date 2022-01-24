using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
using TMPro;

public class PlayerMovementTank : MonoBehaviour
{
    public CharacterController controller;
    public float playerSpeed = 6f;
    public float playerRotationSpeed = 90f;
    Animator p_Animator;
    PopUpSystem pSys;
    public AudioSource damage;

    //nonsense for damage vignette
    public PostProcessVolume pp;
    Vignette vig;

    //base hp is 3
    public static int health = 3;

    void Start()
    {

        p_Animator = gameObject.GetComponent<Animator>();

        //disable canvas component of canvas
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 movement;

        //actual movement calcs and moving the player
        transform.Rotate(0, Input.GetAxis("Horizontal") * playerRotationSpeed * Time.deltaTime, 0);
        movement = transform.forward * Input.GetAxis("Vertical") * playerSpeed;
        controller.Move(movement * Time.deltaTime - Vector3.up * 1);

        //if the player is moving
        if ((Input.GetAxis("Vertical") * playerSpeed) != 0)
        {
            //walk animation stuff
            p_Animator.ResetTrigger("Idle");
            p_Animator.SetTrigger("Walk");

        }
        else
        {
            //if not moving, idle anim
            p_Animator.ResetTrigger("Walk");
            p_Animator.SetTrigger("Idle");
        }

        //if user presses enter
        if (Input.GetKeyDown(KeyCode.Return))
            {

            if (GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().enabled == false)
            {

                //have no idea, was in the unity page
                int layerMask = 1 << 8;
                layerMask = ~layerMask;

                RaycastHit hit;

                //if ray casted from player's front hits an object
                if (Physics.Raycast(transform.position, transform.forward, out hit, 0.8f, layerMask))
                {
                    //if the object the ray hit is an interactable
                    if (hit.collider.GetComponent<Interactable>() != null)
                    {
                        //kick to the popSystem master
                        pSys = GameObject.FindGameObjectWithTag("UI Master").GetComponent<PopUpSystem>();
                        pSys.popBox(hit.collider.GetComponent<Interactable>());
                    }
                    else if (hit.collider.GetComponent<DialogueObject>() != null)
                    {
                        pSys = GameObject.FindGameObjectWithTag("UI Master").GetComponent<PopUpSystem>();
                        pSys.diogBox(hit.collider.GetComponent<DialogueObject>(), 0);
                    }
                    else if (hit.collider.GetComponent<Door>() != null)
                    {
                        hit.collider.GetComponent<Door>().loadConnection();
                    }
                    else
                    {
                        Debug.Log(hit.collider.gameObject.name);
                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.right * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
            }

        }

        
    }

    public void playerDamage()
    {
        health = health - 1;
        damage.Play();
        testShit();
    }

    public void testShit()
    {
        var vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(0.5f);
        vignette.color.Override(new ColorParameter().value = Color.red);
        var volume = PostProcessManager.instance.QuickVolume(7, 100f, vignette);
        volume.weight = 0f;
        DOTween.Sequence()
           .Append(DOTween.To(() => volume.weight, x => volume.weight = x, 1f, 1f))
           .AppendInterval(0.4f)
           .Append(DOTween.To(() => volume.weight, x => volume.weight = x, 0f, 1f))
           .OnComplete(() =>
           {
               RuntimeUtilities.DestroyVolume(volume, true, true);
               Destroy(this);
           });
    }

}