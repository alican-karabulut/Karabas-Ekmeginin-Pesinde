using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; 

public class GameManager : MonoBehaviour
{
    [Header("Oyun Bilgileri (UI)")]
    [SerializeField] TextMeshProUGUI seviyeYazisi;

    [Header("Duraklatma (Pause) Menüsü")]
    [SerializeField] GameObject duraklatmaPaneli;
    [SerializeField] Button duraklatButonu;
    [SerializeField] Button devamEtButonu;
    [SerializeField] Button anaMenuyeDonButtonu;

    [Header("Sahne Ayarları")]
    [SerializeField] string anaMenuSahneAdi = "MainMenu";

    private void Start()
    {
        
        if (seviyeYazisi != null)
        {
            seviyeYazisi.text = "BÖLÜM: " + SceneManager.GetActiveScene().name;
        }

        if (duraklatButonu != null)
        {
            duraklatButonu.onClick.AddListener(OyunuDurdur);
        }

        if (devamEtButonu != null)
        {
            devamEtButonu.onClick.AddListener(OyunuDevamEttir);
        }

        if (anaMenuyeDonButtonu != null)
        {
            anaMenuyeDonButtonu.onClick.AddListener(AnaMenuyeDon);
        }

        Time.timeScale = 1f;
        AudioListener.pause = false;

        
        if (duraklatmaPaneli != null)
        {
            duraklatmaPaneli.SetActive(false);
        }
        
    }

    
    public void OyunuDurdur()
    {
        if (duraklatmaPaneli != null)
        {
            duraklatmaPaneli.SetActive(true);
        }

        Time.timeScale = 0f;          
        AudioListener.pause = true;   
    }

    
    public void OyunuDevamEttir()
    {
        if (duraklatmaPaneli != null)
        {
            duraklatmaPaneli.SetActive(false);
        }

        Time.timeScale = 1f;          
        AudioListener.pause = false;  
    }

    
    public void AnaMenuyeDon()
    {
        Time.timeScale = 1f;          
        AudioListener.pause = false;  

       
        SceneManager.LoadScene(anaMenuSahneAdi);
    }
}