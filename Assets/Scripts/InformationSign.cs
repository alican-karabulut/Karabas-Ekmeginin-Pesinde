using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InformationSign : MonoBehaviour
{
    [SerializeField] GameObject informationPanel;
    [SerializeField] GameObject eTusuUyarisi;
    [SerializeField] Button exitBtn;
    [SerializeField] string hedefEtiket = "Player";
    private bool oyuncuIcerideMi = false;

    private void Start()
    {
        if(informationPanel != null)
        {
            informationPanel.SetActive(false);
        }
        if(eTusuUyarisi != null) eTusuUyarisi.SetActive(false);

        if(exitBtn != null)
        {
            exitBtn.onClick.AddListener(PaneliKapat);
        }
    }
    private void Update()
    {
        if(oyuncuIcerideMi && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if(informationPanel != null && !informationPanel.activeSelf)
            {
                PaneliAc();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(hedefEtiket))
        {
            oyuncuIcerideMi = true;

            if(eTusuUyarisi != null) eTusuUyarisi.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(hedefEtiket))
        {
            oyuncuIcerideMi = false;

            if (eTusuUyarisi != null) eTusuUyarisi.SetActive(false);
        }
    }
    public void PaneliAc()
    {
        if(informationPanel != null)
        {
            informationPanel.SetActive(true);

            if (eTusuUyarisi != null) eTusuUyarisi.SetActive(false);

            Time.timeScale = 0f;
        }
    }
    public void PaneliKapat()
    {
        if(informationPanel != null)
        {
            Time.timeScale = 1f;
            informationPanel.SetActive(false);
        }

        if(oyuncuIcerideMi && eTusuUyarisi != null)
        {
            eTusuUyarisi.SetActive(true);
        }
    }
}
