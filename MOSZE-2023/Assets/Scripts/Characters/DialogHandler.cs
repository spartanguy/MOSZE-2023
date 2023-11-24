using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogHandler : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void Setup(string mess)
    {
        textMeshPro.SetText(mess); 
    }
}
