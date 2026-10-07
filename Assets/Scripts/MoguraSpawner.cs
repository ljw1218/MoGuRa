using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoguraSpawner : MonoBehaviour
{
    [SerializeField] private MoguraType[] types;

    [SerializeField] GameObject easyHole;
    [SerializeField] GameObject hardHole;

    private MoguraSlot[] slots;
    float interval = 0.6f;
    private Coroutine loop;
    private float time = 0;
    private float timeMargin = 1f;

    private void Update()
    {
        time += Time.deltaTime;
    }
    public void Init(Action<int> onHit)
    {
        easyHole.SetActive(!GameData.isHard);
        hardHole.SetActive(GameData.isHard);
        GameObject active = GameData.isHard ? hardHole : easyHole;
        slots = active.GetComponentsInChildren<MoguraSlot>();

        foreach (MoguraSlot s in slots)
        {
            s.onHit = onHit;
        }
    }
    public void StartSpawning()
    {
        time = 0;
        loop = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (loop != null)
            StopCoroutine(loop);
    }

    private IEnumerator SpawnLoop()
    {
        while(time+timeMargin < GameData.gameMaxTime)
        {
            yield return new WaitForSeconds(interval);

            MoguraSlot slot = PickFreeSlot();
            if (slot != null)
                slot.Pop(PickType());
        }
    }
    private MoguraSlot PickFreeSlot()
    {
        List<MoguraSlot> freeSlot = new List<MoguraSlot>();
        foreach (MoguraSlot s in slots)
        {
            if (!s.IsBusy)
                freeSlot.Add(s);
        }
        if (freeSlot.Count == 0)
            return null;
        return freeSlot[Random.Range(0, freeSlot.Count)];
    }

    private MoguraType PickType()
    {
        int total = 0;
        foreach (MoguraType t in types)
            total += t.weight;

        int rand = Random.Range(0, total);
        foreach(MoguraType t in types)
        {
            if (rand < t.weight)
                return t;
            rand -= t.weight;
        }
        return types[0];
    }
}
