using UnityEngine;

public class DialogueObject : MonoBehaviour
{

    public float radius = 3f;
    public string[] dialogue;
    public string[] nameOrder;
    public AudioClip defaultClip;
    public Camera mainCam;
    public Camera diagCam;

    private void Awake()
    {

        defaultClip = GetComponent<AudioSource>().clip;
    }

    public void playVoice(string tag)
    {

        if ((tag == "Player") || (tag == ""))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<AudioSource>().clip = player.GetComponent<VoiceClip>().voice;
            player.GetComponent<AudioSource>().loop = false;
            player.GetComponent<AudioSource>().volume = 0.5f;
            player.GetComponent<AudioSource>().Play();
        }
        else if ((tag == "Child") || (tag == "Steven"))
        {

            GameObject child = GameObject.FindGameObjectWithTag("Child");

            //if there is no clip, register default clip as null
            if (child.GetComponent<AudioSource>().clip != null)
            {
                child.GetComponent<AudioSource>().clip = child.GetComponent<VoiceClip>().voice;
                child.GetComponent<AudioSource>().loop = false;
                child.GetComponent<AudioSource>().Play();
            }

            else
            {
                child.GetComponent<AudioSource>().clip = child.GetComponent<VoiceClip>().voice;
                child.GetComponent<AudioSource>().loop = false;
                child.GetComponent<AudioSource>().Play();
            }
        }
        else if (tag == "Father")
        {

            GameObject father = GameObject.FindGameObjectWithTag("Father");

            //if there is no clip, register default clip as null
            if (father.GetComponent<AudioSource>().clip != null)
            {
                father.GetComponent<AudioSource>().clip = father.GetComponent<VoiceClip>().voice;
                father.GetComponent<AudioSource>().loop = false;
                father.GetComponent<AudioSource>().Play();
            }

            else
            {
                father.GetComponent<AudioSource>().clip = father.GetComponent<VoiceClip>().voice;
                father.GetComponent<AudioSource>().loop = false;
                father.GetComponent<AudioSource>().Play();
            }
        }
        else
        {

            GameObject wife = GameObject.FindGameObjectWithTag("Wife");

            //if there is no clip, register default clip as null
            if (wife.GetComponent<AudioSource>().clip != null)
            {
                wife.GetComponent<AudioSource>().clip = wife.GetComponent<VoiceClip>().voice;
                wife.GetComponent<AudioSource>().loop = false;
                wife.GetComponent<AudioSource>().Play();
            }

            else
            {
                wife.GetComponent<AudioSource>().clip = wife.GetComponent<VoiceClip>().voice;
                wife.GetComponent<AudioSource>().loop = false;
                wife.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void playDefault(string tag)
    {
        if ((tag == "Child") || (tag == "Steven"))
        {
            GameObject child = GameObject.FindGameObjectWithTag("Child");
            child.GetComponent<AudioSource>().clip = defaultClip;
            child.GetComponent<AudioSource>().loop = true;
            child.GetComponent<AudioSource>().Play();

        }
        else if (tag == "Father")
        {
            GameObject father = GameObject.FindGameObjectWithTag("Father");
            father.GetComponent<AudioSource>().clip = defaultClip;
            father.GetComponent<AudioSource>().loop = true;
            father.GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject wife = GameObject.FindGameObjectWithTag("Wife");
            wife.GetComponent<AudioSource>().clip = defaultClip;
            wife.GetComponent<AudioSource>().loop = true;
            wife.GetComponent<AudioSource>().Play();
        }
    }

    //stop playing voice as dialogue advances. this will also swap back audiosource.clip to default
    public void stopVoice(string tag)
    {
        if ((tag == "Player") || (tag == ""))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<AudioSource>().volume = 1f;
            player.GetComponent<AudioSource>().Stop();
        }
        else if ((tag == "Child") || (tag == "Steven"))
        {
            GameObject child = GameObject.FindGameObjectWithTag("Child");
            child.GetComponent<AudioSource>().Stop();
            child.GetComponent<AudioSource>().clip = defaultClip;

        }
        else if (tag == "Father")
        {
            GameObject father = GameObject.FindGameObjectWithTag("Father");
            father.GetComponent<AudioSource>().Stop();
            father.GetComponent<AudioSource>().clip = defaultClip;
        }
        else
        {
            GameObject wife = GameObject.FindGameObjectWithTag("Wife");
            wife.GetComponent<AudioSource>().Stop();
            wife.GetComponent<AudioSource>().clip = defaultClip;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}