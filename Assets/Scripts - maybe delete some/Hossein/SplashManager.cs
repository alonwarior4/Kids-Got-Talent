using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    [SerializeField] VideoClip kharazmiClip;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => player.time > player.clip.length * 0.98f);
        player.clip = kharazmiClip;
        player.Play();
        yield return new WaitUntil(() => player.time > player.clip.length * 0.98f);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);       
    }

}
