using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EkmekToplama : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private string gidilecekSahneAdi = "Level2";
    [SerializeField] private float beklemeSuresi = 2f;
    [SerializeField] private float donusHizi = -50f;
    [SerializeField] private AudioClip gecisSesi;
    private AudioSource gecisSesCalar;

    private void Start()
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        gecisSesCalar = gameObject.AddComponent<AudioSource>();
        gecisSesCalar.playOnAwake = false;
    }
    private void Update()
    {
        transform.Rotate(0f,0f,donusHizi *  Time.deltaTime) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(BolumeGecisYap());
        }
    }

    private IEnumerator BolumeGecisYap()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if(loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        if (gecisSesi != null && gecisSesCalar != null)
        {
            gecisSesCalar.PlayOneShot(gecisSesi); 
        }

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(beklemeSuresi);

        Time.timeScale = 1f;

        SceneManager.LoadScene(gidilecekSahneAdi);
    }
}
