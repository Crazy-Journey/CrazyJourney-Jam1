using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerDataManager;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager THIS;

    [SerializeField] float initialPower;
    [SerializeField] int initialCoins;
    [SerializeField] int initialPiso;


    public struct PlayerData
    {
        private float _power;
        private int _coins;
        private int _piso;

        public void SetPower(float power)
        {
            _power = power;
        }

        public void ChangePower(float power)
        {
            _power += power;
        }

        public void SetCoins(int coins)
        {
            _coins = coins;
        }

        public void ChangeCoins(int coins)
        {
            _coins += coins;
        }

        public void SetPiso(int piso)
        {
            _piso = piso;
        }

        public void ChangePiso(int piso)
        {
            _piso += piso;
        }

        public float GetPower()
        {
            return _power;
        }

        public int GetCoins()
        {
            return _coins;
        }

        public int GetPiso()
        {
            return _piso;
        }
    }

    private List<PlayerData> playerData = new List<PlayerData>();

    private void Awake()
    {
        // Uso de singleton
        if (THIS == null)
        {
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
            return;
        }
            
    }

    private void Start()
    {
        playerData.Add(new PlayerData());
        playerData.Add(new PlayerData());

        for (int i = 0; i < 2; i++)
        {
            PlayerData player = new PlayerData();
            player.SetPower(initialPower);
            player.SetCoins(initialCoins);
            player.SetPiso(initialPiso);
            SetPlayer(i, player);
        };
    }
    private void Update()
    {
        //Debug.Log("Player 1 Power: " + playerData[0].GetPower());
        //Debug.Log("Player 1 Coins: " + playerData[0].GetCoins());
        //Debug.Log("Player 2 Power: " + playerData[1].GetPower());
        //Debug.Log("Player 2 Coins: " + playerData[1].GetCoins());
    }

    public PlayerData GetPlayer(int p)
    {
        return playerData[p];
    }

    public void SetPlayer(int p, PlayerData pData)
    {
        playerData[p] = pData;
    }

}
