using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStats : MonoBehaviour
{
    [SerializeField] TMP_Text player1CoinsText, player1PowerText, player2CoinsText, player2PowerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player1CoinsText.text = Mathf.FloorToInt(PlayerDataManager.THIS.GetPlayer(0).GetCoins()).ToString();
        player1PowerText.text = Mathf.FloorToInt((int)PlayerDataManager.THIS.GetPlayer(0).GetPower()).ToString();
        player2CoinsText.text = Mathf.FloorToInt(PlayerDataManager.THIS.GetPlayer(1).GetCoins()).ToString();
        player2PowerText.text = Mathf.FloorToInt(PlayerDataManager.THIS.GetPlayer(1).GetPower()).ToString();
    }
}
