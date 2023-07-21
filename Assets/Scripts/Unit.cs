using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Info")]
    [Tooltip("Unit Name")]
    public string Name;
    [SerializeField]
    [Tooltip("Unit Look(Sprite)")]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    [Tooltip("Unit Stat")]
    Stat stat;
    [SerializeField]
    [Tooltip("Unit Health")]
    Health health;
    [SerializeField]
    [Tooltip("Unit Abillity")]
    Ability abillity;
    [SerializeField]
    [Tooltip("Unit Battle Stat")]
    BattleStat battleStat;
    [SerializeField]
    [Tooltip("Unit Explore Stat")]
    ExploreStat exploreStat;
    [Header("Unit Component Division")]
    [SerializeField]
    [Tooltip("Owned Component")]
    bool[] ownFlag;
    [SerializeField]
    [Tooltip("Is Unit Miner?")]
    bool IsMinerFlag;

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
