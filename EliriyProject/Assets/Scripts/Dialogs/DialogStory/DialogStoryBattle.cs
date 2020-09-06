using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class DialogStory
{
  private List<Hero> my_squad = null;
  private List<Hero> enemy_squad = null;
  private Action<bool> on_finish_battle = null;
  
  
  public void initAsBattle( List<Hero> my_squad, List<Hero> enemy_squad, Action<bool> on_finish_battle )
  {
    this.my_squad = my_squad;
    this.enemy_squad = enemy_squad;
    this.on_finish_battle = on_finish_battle;

    foreach ( Hero my in my_squad )
      my.onHeroDie += onMyHeroDie;

    foreach ( Hero enemy in enemy_squad )
      enemy.onHeroDie += onEnemyHeroDie;

    startRound( true );
  }

  private void onMyHeroDie( Hero hero )
  {
    hero.onHeroDie -= onMyHeroDie;
    my_squad.Remove( hero );

    if ( my_squad.Count == 0 )
    {
      Debug.Log( $"BATTLE FAIL" );
      on_finish_battle?.Invoke( false );
    }
  }

  private void onEnemyHeroDie( Hero hero )
  {
    hero.onHeroDie -= onEnemyHeroDie;
    enemy_squad.Remove( hero );
    
    if ( enemy_squad.Count == 0 )
    {
      Debug.Log( $"BATTLE WIN" );
      on_finish_battle?.Invoke( true );
    }
  }

  private void startRound( bool first_round = false)
  {
    destroyAllButtons();
    
    if ( first_round )
    {
      txt_story_desc.text = $"Вы встретили врага: { getListEnemy() }\nВыберите персонажа:";
    }
    else
    {
      txt_story_desc.text = $"Выберите персонажа:"; // TODO refa this bullshit
    }
    
    foreach ( Hero hero in my_squad )
    {
      UIAnswerButton answer_button = spawnButtonAnswer();
      answer_button.init( getHeroInfo( hero ), () => selectSkill( hero ) );
    }
  }

  private void selectSkill( Hero hero )
  {
    destroyAllButtons();
    
    txt_story_desc.text = $"Вы выбрали персонажа по имени: <b>{hero.scriptable_hero.hero_name}</b>\nВыберите способность:"; // TODO refa this bullshit

    foreach ( ScriptableHeroSkill skill in hero.scriptable_hero.skills )
    {
      UIAnswerButton answer_button = spawnButtonAnswer( ButtonSize.MEDIUM );
      answer_button.init( getSkillInfo( hero, skill ), () => selectEnemy( hero, skill ) );
    }
  }

  private void selectEnemy( Hero attacker_hero, ScriptableHeroSkill skill )
  {
    destroyAllButtons();
    
    txt_story_desc.text = $"Выберите цель для способности <b>{skill.skill_name}</b>";

    foreach ( Hero hero in enemy_squad )
    {
      UIAnswerButton answer_button = spawnButtonAnswer();
      answer_button.init( getEnemyInfo( hero ), () => makeAction( skill, attacker_hero, hero ) );
    }
  }

  private void makeAction( ScriptableHeroSkill skill, Hero attacker, Hero target )
  {
    destroyAllButtons();
    
    string result = $"Вы бьете существо {target.scriptable_hero.hero_name}, ";

    if ( target.tryToHit( attacker, skill ) )
    {
      result += $"и наносите {getDamage( skill, attacker )}";
    }
    else
    {
      result += $"но оно уворачиваеться от атаки.";
    }
    
    txt_story_desc.text = result;
    
    UIAnswerButton answer_button = spawnButtonAnswer();
    answer_button.init( $"Дальше", () => enemyRound() );
    
    checkAtEndBattle();
  }

  private void enemyRound()
  {
    IEnumerable<Hero> available_heroes = enemy_squad;
    Hero random_enemy = available_heroes.randomElement();
    ScriptableHeroSkill random_skill = random_enemy.scriptable_hero.skills.randomElement();
    Hero random_attack_target = my_squad.randomElement();

    txt_story_desc.text = $"Существо {random_enemy.scriptable_hero.hero_name} пытаеться использовать способность {random_skill.skill_name}. И оно ";

    if ( random_attack_target.tryToHit( random_enemy, random_skill ) )
      txt_story_desc.text += $"наносит {getDamage( random_skill, random_enemy )} персонажу {random_attack_target.scriptable_hero.hero_name}.";
    else
      txt_story_desc.text += $"промахиваеться.";
    
    destroyAllButtons();
    UIAnswerButton answer_button = spawnButtonAnswer();
    answer_button.init( $"Дальше", () => startRound() );
    
    checkAtEndBattle();
  }

  private void checkAtEndBattle()
  {
    bool is_end = enemy_squad.Count == 0 || my_squad.Count == 0;

    if ( is_end )
    {
      destroyAllButtons();
      UIAnswerButton answer_button = spawnButtonAnswer();
      answer_button.init( $"Закончить", closeDialog );
    }
  }
  
  private string getDamage( ScriptableHeroSkill skill, Hero hero )
  {
    int damage = 0;

    if ( skill.use_weapon )
      damage = hero.scriptable_hero.weapon.damage;
    else
      damage = skill.damage;

    return $"{damage} урона";
  }

  private string getSkillInfo( Hero hero, ScriptableHeroSkill skill )
  {
    string bonus_info = null;
    
    if ( skill.use_weapon )
    {
      bonus_info = $"Использовать <u>{hero.scriptable_hero.weapon.item_name}</u>. Урон:{hero.scriptable_hero.weapon.damage}, шанс попадания: {hero.chanceHit * 100.0f}%, перезарядка: {skill.cooldown} хода";
    }
    else
    {
      bonus_info = $"Урон:{skill.damage}, шанс попадания: {skill.chance_hit * 100.0f}%, перезарядка: {skill.cooldown} хода";
    }

    return $"<b>{skill.skill_name}</b>\n{bonus_info}";
  }

  private string getHeroInfo( Hero hero )
  {
    return $"{hero.scriptable_hero.hero_name} ({hero.cur_health}/{hero.health})";
  }
  
  private string getEnemyInfo( Hero enemy )
  {
    return $"{enemy.scriptable_hero.hero_name} ({enemy.cur_health}/{enemy.health})";
  }

  private string getListEnemy()
  {
    string enemies = string.Join( ", ", enemy_squad.Select( x => x.scriptable_hero.hero_name ) );
    txt_story_desc.text = $"Вы встретили врага: {enemies}.\nВыберите персонажа."; // TODO refa this bullshit

    return enemies;
  }
}
