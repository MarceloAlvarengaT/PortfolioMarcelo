using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlink : MonoBehaviour
{
    public void LinkTOR()
    {
        Application.OpenURL("https://drive.google.com/file/d/1-VdRJx_CBQf2QJchhC31QUrUX5HtngwM/view?usp=sharing");
    }

    public void LinkNL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Clovit.Match_Game&hl=en_US&gl=US");
    }
}
