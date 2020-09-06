using System;
using UnityEngine;
using UnityEngine.UI;


public class UIBlockBanner : UIBlockBase
{
  [SerializeField] private Image img_banner = null;


  public void init( Sprite banner_sprite, Action action_after_init )
  {
    this.action_after_init = action_after_init;
    
    img_banner.sprite = banner_sprite;
    
    show();
  }
}
