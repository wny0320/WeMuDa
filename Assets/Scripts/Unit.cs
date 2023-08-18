using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string Name;
    private SpriteRenderer spriteRenderer;
    private Stat stat;
    private Health health;
    private Ability ability;
    private BattleStat battleStat;
    private ExploreStat exploreStat;
    private bool[] ownFlag;
    private bool IsMinerFlag;

    public void Spawn(string name, Sprite sprite)
    {
        Name = name;
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        if(stat == null) stat = GetComponent<Stat>();
        stat.RandomStat();
        if(ability == null) ability = GetComponent<Ability>();
        ability.RandomAbillity();
        if(health == null) health = GetComponent<Health>();
        health.HealthReset();
        if(battleStat == null) battleStat = GetComponent<BattleStat>();
        battleStat.BattleStatSet(stat);
    }

    public void CallOneDayAfter()
    {
        health.OneDayAfter();
    }
}
