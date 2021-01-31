using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostOffice1ChangeScene : MonoBehaviour
{
    AudioSource audio;
    [SerializeField]
    public Object nextScene;

    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitForTransition());
    }

    IEnumerator WaitForTransition()
    {
        while (audio.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextScene.name);
        yield return null;
    }
}
