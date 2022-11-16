using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle : MonoBehaviour
{
    public GameObject sentence;
    public GameObject solution;

    public AudioClip magicSound;
    public AudioClip rightSound;
    public AudioClip wrongSound;

    private AudioSource _audioSource;
    private BoxCollider _collider;

    private const string url = "https://www.youtube.com/watch?v=eebRBB-MBXs";

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Check the bullet text matches the solution
            string solutionText = solution.GetComponent<TextMeshPro>().text;
            if (other.GetComponent<Bullet>().GetText().Equals(solutionText))
            {
                solution.SetActive(true);
                PlaySound(rightSound);

                StartCoroutine(OpenBrowser(url));
            } else
            {
                PlaySound(wrongSound);
            }
        }
    }

    public void ShowSentence()
    {
        sentence.SetActive(true);
        _collider.enabled = true;
        PlaySound(magicSound);
    }

    private void PlaySound(AudioClip sound)
    {
        _audioSource.clip = sound;
        _audioSource.Play();
    }

    private IEnumerator OpenBrowser(string URL)
    {
        yield return new WaitForSeconds(2f);
        Application.OpenURL(URL);
    }
}
