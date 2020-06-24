using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Button btn_cancel_traveling = null;
    [SerializeField] private Button btn_player_info = null;
    
    private GameManager gameManager => GameManager.instance;
    private DialogsManager dialogsManager => DialogsManager.instance;
    
    void Awake()
    {
        GameEventManager.onPlayerStartTraveling += () => btn_cancel_traveling.gameObject.SetActive(true);
        GameEventManager.onPlayerFinishTraveling += () => btn_cancel_traveling.gameObject.SetActive(false);
        GameEventManager.onPlayerCancelTraveling += () => btn_cancel_traveling.gameObject.SetActive(false);

        btn_cancel_traveling.onClick.AddListener( () => gameManager.cancelTravel() );
        btn_player_info.onClick.AddListener( () => dialogsManager.initDialogPlayer() );
    }
}
