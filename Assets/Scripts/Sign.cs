using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public bool isActive = false;
    public GameObject PanelSpanish;
    public GameObject PanelEnglish;
    private StartGameManager startGameManager;

    private void Start()
    {
        startGameManager = GameObject.Find("StartGameManager").GetComponent<StartGameManager>();
    }

    public IEnumerator SignAction()
    {
        if (startGameManager.IsSpanish)
        {
            if (!isActive)
            {
                isActive = true;
                PanelSpanish.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(2f);
                isActive = false;
                PanelSpanish.SetActive(false);
            }
        }
        else
        {
            if (!isActive)
            {
                isActive = true;
                PanelEnglish.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(2f);
                isActive = false;
                PanelEnglish.SetActive(false);
            }
        }


    }

    public void TurnOff()
    {
        PanelSpanish.SetActive(false);
        PanelEnglish.SetActive(false);
    }
}
