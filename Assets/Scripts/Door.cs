using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public AudioClip opening;
    public string sceneToLoad;

    public void loadConnection()
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().enabled = true;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.GetChild(1).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.GetChild(2).gameObject.SetActive(false);
        GetComponent<AudioSource>().clip = opening;
        GetComponent<AudioSource>().Play();

        StartCoroutine(waitLoad());
    }

    IEnumerator waitLoad()
    {

        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.GetChild(1).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.GetChild(2).gameObject.SetActive(true);
        SceneManager.LoadScene(sceneToLoad);
        /*GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().enabled = false;

        */
        yield return null;
        }
    }