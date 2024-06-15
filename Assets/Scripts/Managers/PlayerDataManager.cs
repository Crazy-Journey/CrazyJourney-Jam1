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

    private PlayerData playerData1;
    private PlayerData playerData2;

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

    public ref PlayerData Player1()
    {
        return ref playerData1;
    }

    public ref PlayerData Player2()
    {
        return ref playerData2;
    }

}
