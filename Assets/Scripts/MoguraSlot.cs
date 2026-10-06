using UnityEngine;
using UnityEngine.UI;

public class MoguraSlot : MonoBehaviour
{
    private Transform mogura;
    private Image img;
    
    void Awake()
    {
        mogura = transform.GetChild(0);
        img = mogura.GetComponent<Image>();
    }

}
