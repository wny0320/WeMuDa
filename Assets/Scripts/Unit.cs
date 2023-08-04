using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string Name;
    private SpriteRenderer spriteRenderer;
    private Stat stat;
    private Health health;
    private Ability abillity;
    private BattleStat battleStat;
    private ExploreStat exploreStat;
    private bool[] ownFlag;
    private bool IsMinerFlag;

    public void Spawn(string name, Sprite sprite)
    {
        Name = name;
        spriteRenderer.sprite = sprite;

        stat.RandomStat();
        abillity.RandomAbillity();
        health.HealthReset();
        battleStat.BattleStatSet(stat);
    }

    public void CallOneDayAfter()
    {
        health.OneDayAfter();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
