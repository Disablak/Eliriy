using System.Collections.Generic;
using System.Linq;

public partial class DialogStory
{
  private List<Hero> enemy_squad = null;
  
  public void initAsBattle( List<Hero> my_squad, List<Hero> enemy_squad )
  {
    this.enemy_squad = enemy_squad;
    
    string enemies = string.Join( ", ", enemy_squad.Select( x => x.scriptable_hero.hero_name ) );
    txt_story_desc.text = $"Вы встретили врага: {enemies}.\nВыберите персонажа.";// TODO refa this bullshit
    
    foreach ( Hero hero in my_squad )
    {
      UIAnswerButton answer_button = spawnButtonAnswer();
      answer_button.init( hero.scriptable_hero.hero_name, () => selectSkill( hero ) );
    }
  }

  private void selectSkill( Hero hero )
  {
    destroyAllButtons();
    
    txt_story_desc.text = $"Вы выбрали персонажа по имени: <b>{hero.scriptable_hero.hero_name}</b>\nВыберите способность:"; // TODO refa this bullshit

    foreach ( ScriptableHeroSkill skill in hero.scriptable_hero.skills )
    {
      UIAnswerButton answer_button = spawnButtonAnswer( ButtonSize.MEDIUM );
      answer_button.init( getSkillInfo( hero, skill ), () => selectEnemy( skill ) );
    }
  }

  private void selectEnemy( ScriptableHeroSkill skill )
  {
    destroyAllButtons();
    
    txt_story_desc.text = $"Выберите цель для способности <b>{skill.skill_name}</b>";

    foreach ( Hero hero in enemy_squad )
    {
      UIAnswerButton answer_button = spawnButtonAnswer();
      answer_button.init( hero.scriptable_hero.hero_name, () => calcAction( skill, hero ) );
    }
  }

  private void calcAction( ScriptableHeroSkill skill, Hero target )
  {
    destroyAllButtons();
    
    txt_story_desc.text = $"Нихуя себе, вы убили {target.scriptable_hero.hero_name}. Мои поздравления)";
  }

  private string getSkillInfo( Hero hero, ScriptableHeroSkill skill )
  {
    string bonus_info = null;
    
    if ( skill.use_weapon )
    {
      bonus_info = $"Использовать <u>{hero.scriptable_hero.weapon.item_name}</u>. Урон:{hero.scriptable_hero.weapon.damage}, <nobr>шанс попадания: {hero.chanceHit * 100.0f}%,</nobr> перезарядка: {skill.cooldown} хода";
    }
    else
    {
      bonus_info = $"Урон:{skill.damage}, <nobr>шанс попадания: {skill.chance_hit * 100.0f}%,</nobr> перезарядка: {skill.cooldown} хода";
    }

    return $"<b>{skill.skill_name}</b>\n{bonus_info}";
  }
}
