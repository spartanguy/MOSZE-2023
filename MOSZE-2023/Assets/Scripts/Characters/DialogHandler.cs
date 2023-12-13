using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//dialogusok megjelenéséért felelős class.
public class DialogHandler : MonoBehaviour
{
    //egy textMeshből áll amit kiírunk a képernyőre.
    public TextMeshProUGUI textMeshPro;

    // A mess értéket kiirjuk a képernyőre
    public void Setup(string mess)
    {
        textMeshPro.SetText(mess); 
    }
}
