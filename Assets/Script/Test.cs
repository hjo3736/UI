using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{

    [SerializeField]
    TMP_Text text;
    private void Start()
    {
        text.text = SendingInfo.name;
    }


}
