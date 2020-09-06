﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hero
{
    public ScriptableHero scriptable_hero { get; } = null;

    public int strength     { get; } = 0;
    public int dexterity    { get; } = 0;
    public int intelligence { get; } = 0;

    public int cur_health   { get; set; } = 0;
    
    public event Action<Hero> onHeroDie = delegate( Hero hero ) {};
    
    private const int ADD_HP_PER_LVL = 10;
    private const int ADD_DAMAGE_PER_LVL = 2;

    private const float CHANCE_HIT_PER_LVL = 0.05f;
    private const float CHANCE_HIT_MIN = 0.4f;
    private const float CHANCE_HIT_MAX = 0.95f;

    private const float ADD_CHANCE_CRIT_PER_LVL = 0.02f;
    private const float CHANCE_CRIT_MIN = 0.0f;
    private const float CHANCE_CRIT_MAX = 1.0f;

    private const float MINUS_COOLDOWN_PER_LVL = 0.25f;
    
    
    public int health => strength * ADD_HP_PER_LVL;

    public int addDamage => strength * ADD_DAMAGE_PER_LVL;

    public float chanceHit => Mathf.Clamp( CHANCE_HIT_MIN + (dexterity * CHANCE_HIT_PER_LVL), CHANCE_HIT_MIN, CHANCE_HIT_MAX );
    
    public float addChanceCrit => Mathf.Clamp( dexterity * ADD_CHANCE_CRIT_PER_LVL, CHANCE_CRIT_MIN, CHANCE_CRIT_MAX );

    public int minusCooldown => Mathf.FloorToInt( intelligence * MINUS_COOLDOWN_PER_LVL );

    public int logic => intelligence;

    
    public Hero( ScriptableHero scriptable_hero )
    {
        this.scriptable_hero = scriptable_hero;

        strength     = scriptable_hero.strength;
        dexterity    = scriptable_hero.dexterity;
        intelligence = scriptable_hero.intelligence;

        cur_health = health;
    }


    public bool tryToHit( Hero attacker_hero, ScriptableHeroSkill skill )
    {
        bool was_hit = false;
        
        if ( skill.use_weapon )
            was_hit = Random.value <= attacker_hero.chanceHit;
        else
            was_hit = Random.value <= skill.chance_hit;

        if ( was_hit )
            setDamage( getDamage( attacker_hero, skill ) );
        
        return was_hit;
    }

    public void setDamage( int damage )
    {
        if ( cur_health <= 0 )
        {
            Debug.LogError( $"{scriptable_hero.hero_name} already die!" );
            return;
        }
        
        cur_health -= damage;

        if ( cur_health <= 0 )
            onHeroDie( this );
    }
    
    private int getDamage( Hero hero, ScriptableHeroSkill skill )
    {
        int damage = 0;

        if ( skill.use_weapon )
            damage = hero.scriptable_hero.weapon.damage;
        else
            damage = skill.damage;

        return damage;
    }
}
