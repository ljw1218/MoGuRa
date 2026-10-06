using System;
using UnityEngine;
using UnityEngine.UI;

public class MoguraSlot : MonoBehaviour
{
    private Transform mogura;
    private Image img;
    public Action<int> onHit;
    public bool IsBusy { get; private set; }
    
    void Awake()
    {
        mogura = transform.GetChild(0);
        img = mogura.GetComponent<Image>();
    }

}
