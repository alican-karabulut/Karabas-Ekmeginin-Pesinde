using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//game manager
public class AnaMenuKontrol : MonoBehaviour
{
    [SerializeField] private string ilkBolumAdi = "Level1";
    [SerializeField] private Button baslaButton;
    [SerializeField] private Button cikisButton;
    [SerializeField] private AudioClip baslaBtn;
    [SerializeField] private AudioClip cikisBtn;
    private AudioSource sesOynat;

    private void Start()
    {
        sesOynat = GetComponent<AudioSource>();

        if(sesOynat == null)
        {
            sesOynat = gameObject.AddComponent<AudioSource>();
        }

        if(baslaButton != null)
        {
            baslaButton.onClick.AddListener(OyunaBasla);
        }

        if(cikisButton != null)
        {
            cikisButton.onClick.AddListener(OyundanCik);
        }
    }
    public void OyunaBasla()
    {

        StartCoroutine(SesliSahneGecisi());
    }

    public void OyundanCik()
    {
        if(cikisBtn != null)
        {
            sesOynat.PlayOneShot(cikisBtn);
        }
        Application.Quit();
        Debug.Log("Oyundan cikis yapildi");
    }

    private IEnumerator SesliSahneGecisi()
    {
        if(baslaBtn != null)
        {
            sesOynat.PlayOneShot(baslaBtn);

            yield return new WaitForSeconds(baslaBtn.length);
        }
        Time.timeScale = 1f;

        SceneManager.LoadScene(ilkBolumAdi);
    }
}
