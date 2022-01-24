using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{

    Canvas canvas;
    PlayerMovementTank player;
    float temp;
    float tempRot;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementTank>();
        temp = player.playerSpeed;
        tempRot = player.playerRotationSpeed;
    }

    public void popBox(Interactable inter)
    {
        canvas.enabled = true;
        TextMeshProUGUI mText = canvas.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        canvas.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        mText.text = inter.text;

        StartCoroutine(waitDialogue(canvas, mText, player));
        player.playerSpeed = 0f;
        player.playerRotationSpeed = 0f;
    }

    public void diogBox(DialogueObject obj, int count)
    {
        canvas.enabled = true;
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        TextMeshProUGUI mText = canvas.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI name = canvas.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

        if (count != (obj.dialogue.Length - 1))
        {
            Debug.Log(obj.nameOrder[count]);
            name.text = obj.nameOrder[count];
            mText.text = obj.dialogue[count];

            obj.playVoice(obj.nameOrder[count]);

            player.playerSpeed = 0f;
            player.playerRotationSpeed = 0f;
            obj.mainCam.enabled = false;
            obj.diagCam.enabled = true;

            StartCoroutine(conDialogue(canvas, mText, player, obj, count));
        }
        else
        {
            name.text = obj.nameOrder[count];
            mText.text = obj.dialogue[count];
            obj.playVoice(obj.nameOrder[count]);

            player.playerSpeed = 0f;
            player.playerRotationSpeed = 0f;

            StartCoroutine(endDialogue(canvas, mText, player, obj));
        }
    }

    IEnumerator waitDialogue(Canvas cnv, TextMeshProUGUI text, PlayerMovementTank p)
    {

        yield return new WaitForSeconds(0.5f);

        bool done = false;
        while (!done)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Wait Done");
                done = true;
                cnv.enabled = false;
                cnv.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                cnv.transform.GetChild(3).gameObject.SetActive(true);
                text.text = "SAMPLE TEXT";
                p.playerSpeed = temp;
                p.playerRotationSpeed = tempRot;
            }
            yield return null;
        }
    }

    IEnumerator endDialogue(Canvas cnv, TextMeshProUGUI text, PlayerMovementTank p, DialogueObject obj)
    {

        yield return new WaitForSeconds(0.5f);

        bool done = false;
        while (!done)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Done");
                done = true;
                cnv.transform.GetChild(3).gameObject.SetActive(true);
                cnv.enabled = false;
                text.text = "SAMPLE TEXT";
                p.playerSpeed = temp;
                p.playerRotationSpeed = tempRot;
                obj.stopVoice(obj.nameOrder[obj.nameOrder.Length - 1]);
                obj.playDefault(obj.nameOrder[obj.nameOrder.Length -1]);
                obj.mainCam.enabled = true;
                obj.diagCam.enabled = false;
            }
            yield return null;
        }
    }

    IEnumerator conDialogue(Canvas cnv, TextMeshProUGUI text, PlayerMovementTank p, DialogueObject obj, int count)
        {

            yield return new WaitForSeconds(0.5f);
            bool done = false;

            while (!done)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    done = true;
                    obj.stopVoice(obj.nameOrder[count]);
                    count++;
                    diogBox(obj, count);
                }
                yield return null;
            }
        }

}
