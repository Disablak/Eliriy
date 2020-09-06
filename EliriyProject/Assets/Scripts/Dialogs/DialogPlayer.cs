using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogPlayer : MonoBehaviour
{
    [SerializeField] private Button btn_exit = null;

    [SerializeField] private TextMeshProUGUI txt_player_characteristic = null;
    [SerializeField] private TextMeshProUGUI txt_inventory = null;
    [SerializeField] private TextMeshProUGUI txt_i_know = null;

    private void Awake()
    {
        btn_exit.onClick.AddListener( closeDialog );
    }

    public void init()
    {
        PlayerInfo player_info = GameManager.instance.PlayerInfo;
        
        setCharacteristic( player_info.getPlayer );
        setInventory( player_info.money, player_info.my_items );
        setIKnow( player_info.i_know );
    }
    
    public void setInventory( int money, List<ScriptableItem> items )
    {
        string text = $"Inventory:\nMoney - {money}\n";
        string all_items = string.Join( "\n", items.Select( x => x.name ) );
        
        txt_inventory.text = $"{text}{all_items}";
    }

    public void setIKnow( List<IKnow> knows )
    {
        txt_i_know.text = string.Join( "\n", knows );
    }

    private void setCharacteristic( Hero characteristic )
    {
        txt_player_characteristic.text = $"Health: {characteristic.health}\n" +
                                         $"Additional damage: {characteristic.addDamage}\n" +
                                         $"Chance to hit: {characteristic.chanceHit}\n" +
                                         $"Chance to crit: {characteristic.addChanceCrit}\n" +
                                         $"Minus cooldown: {characteristic.minusCooldown}\n" +
                                         $"Logic: {characteristic.logic}";
    }

    private void closeDialog()
    {
        gameObject.SetActive( false );
    }
}
