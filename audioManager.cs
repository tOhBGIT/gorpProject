using UnityEngine;

public class audioManager : MonoBehaviour
{

    public AudioSource source;
    public AudioClip[] sounds = new AudioClip[0];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSfx(string soundName, float volume)
    {
        source.loop = false;
        int toPlay = 0;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                toPlay = i;
            }
            else
            {
                Debug.Log("No sound found with name "+ soundName);
            }
        }
        source.PlayOneShot(sounds[toPlay], volume);
    }
}
