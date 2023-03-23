using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    public bool IsSpanish;

    public GameObject panel;

    public Animator PanelAnim;

    public TextMeshProUGUI RightkeyText;
    public TextMeshProUGUI LeftkeyText;
    public TextMeshProUGUI SpacekeyText;
    public Image PhoneRotate;

    public Sprite SignSpanish;
    public Sprite SignEnglish;

    public GameObject firstsign;

    public AudioSource music;
    public Button musicButton;

    public Sprite muteSprite;
    public Sprite soundSprite;

    private MobileCheck mc;

    public GameObject PCControls;
    public GameObject PhoneControls;

    private void Start()
    {
        musicButton.image.sprite = soundSprite;
        mc = GameObject.Find("MobileCheck").GetComponent<MobileCheck>();
        PhoneRotate.gameObject.SetActive(false);
        if (mc.isMobile())
        {
            PhoneControls.SetActive(true);
            PCControls.SetActive(false);
            PhoneRotate.gameObject.SetActive(true);
        }
        else
        {
            PhoneControls.SetActive(false);
            PCControls.SetActive(true);
        }
    }

    private void Update()
    {

    }
    public void SetSpanish()
    {
        IsSpanish = true;
        RightkeyText.text = "Camina hacia la derecha";
        LeftkeyText.text = "Camina hacia la izquierda";
        SpacekeyText.text = "Abrir/Cerrar letreros";

        firstsign.GetComponent<SpriteRenderer>().sprite = SignSpanish;
        PanelAnim.SetBool("Start", true);
        music.Play();
        StartCoroutine(Wait());
    }

    public void SetEnglish()
    {
        IsSpanish = false;
        RightkeyText.text = "Walk Right";
        LeftkeyText.text = "Walk Left";
        SpacekeyText.text = "Open/Close sign";
        firstsign.GetComponent<SpriteRenderer>().sprite = SignEnglish;
        PanelAnim.SetBool("Start", true);
        music.Play();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
    }

    public void VolumeControl()
    {
        if(music.volume == 0.5)
        {
            music.volume = 0;
            musicButton.image.sprite = muteSprite;
        }
        else if(music.volume == 0)
        {
            music.volume = 0.5f;
            musicButton.image.sprite = soundSprite;
        }
    }
}
