using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutScenePlayer : MonoBehaviour
{
    private PlayableDirector custscene;
    public AudioSource source;

    void Start()
    {
        custscene = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            
        custscene.Play();
        source.Play();
        }
    }
}
