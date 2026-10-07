using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoguraSlot : MonoBehaviour
{
    [SerializeField] private Image mogura;

    public Action<int> onHit;
    public bool IsBusy { get; private set; }

    [Header("두더지 위치")]
    private float shownY = -40f;
    private float hideY = -285f;

    [Header("두더지 시간")]
    private float moveTime = 0.2f;
    private float stayTime = 1.5f;
    private float hitshowTime = 0.3f;

    RectTransform rt;
    MoguraType currenType;
    bool bcanHit;
    Coroutine routine;

    void Awake()
    {
        rt = mogura.rectTransform;
        SetY(hideY);
    }

    private IEnumerator PopRoutine()
    {
        IsBusy = true;
        bcanHit = true;
        yield return Move(hideY, shownY);
        yield return new WaitForSeconds(stayTime);
        bcanHit = false;
        if(currenType.score > 0)
            GameManager.instance.combo = 0;
        yield return Move(shownY,hideY);
        IsBusy = false;
    }
    public void Pop(MoguraType type)
    {
        currenType = type;
        mogura.sprite = type.imgIdle;
        routine = StartCoroutine(PopRoutine());
    }
    private void SetY(float y)
    {
        Vector2 pos = rt.anchoredPosition;
        pos.y = y;
        rt.anchoredPosition = pos;
    }

    private IEnumerator Move(float from,float to)
    {
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / moveTime;
            SetY(Mathf.Lerp(from,to,Mathf.SmoothStep(0f,1f,t)));
            yield return null;
        }
    }
    private IEnumerator HitRoutine()
    {
        yield return new WaitForSeconds(hitshowTime);
        yield return Move(rt.anchoredPosition.y, hideY);
        IsBusy = false;
    }
    public void Hit()
    {
        if (!bcanHit)
            return;
        bcanHit = false;
        StopCoroutine(routine);
        mogura.sprite = currenType.imgHit;
        if (currenType.score < 0)
            GameManager.instance.combo = 0;
        else
            GameManager.instance.combo++;
        onHit?.Invoke(currenType.score);
        routine = StartCoroutine(HitRoutine());
    }
}
