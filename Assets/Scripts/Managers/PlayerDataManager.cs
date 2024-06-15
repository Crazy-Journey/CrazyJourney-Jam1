using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
