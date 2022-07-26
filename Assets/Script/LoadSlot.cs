using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    TMP_Text text;

    private string _file;
    public string file
    {
        get { return _file; }
        set
        {
            _file = value;
            if (_file != null)
            {
                text.text = _file;
            }
            else
            {
                text.text = null;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SendingInfo.name = text.text;
        SendingInfo.is_loaded = true;
        Debug.Log(SendingInfo.name);
        Debug.Log(SendingInfo.is_loaded);
        SceneManager.LoadScene(1);  
    }

}
