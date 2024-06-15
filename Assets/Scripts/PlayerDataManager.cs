using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager THIS;

    public struct PlayerData
    {
        private float power;
        private int coins;

        public void SetPower(float p)
        {
            power = p;
        }

        public void ChangePower(float p)
        {
            power += p;
        }

        public void SetCoins(int c)
        {
            coins = c;
        }

        public void ChangeCoins(int c)
        {
            coins += c;
        }

        public float GetPower()
        {
            return power;
        }

        public int GetCoins()
        {
            return coins;
        }
    }

    [SerializeField] PlayerData playerData1;
    [SerializeField] PlayerData playerData2;

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

    public PlayerData Player1()
    {
        return playerData1;
    }

    public PlayerData Player2()
    {
        return playerData2;
    }

}
