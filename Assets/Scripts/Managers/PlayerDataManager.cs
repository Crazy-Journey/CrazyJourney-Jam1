using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerDataManager;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager THIS;

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
        else Destroy(gameObject);
    }

    private void Start()
    {
        playerData.Add(new PlayerData());
        playerData.Add(new PlayerData());

        foreach (PlayerData playerData in playerData)
        {
            playerData.SetPower(0);
            playerData.SetCoins(0);
            playerData.SetPiso(0);
        };
    }

    public PlayerData Player(int p)
    {
        return playerData[p];
    }

}
