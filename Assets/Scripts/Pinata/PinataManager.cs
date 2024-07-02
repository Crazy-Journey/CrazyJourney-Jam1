using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PinataManager : MonoBehaviour
{
    public float powerDrop;
    public int coinDrop;
    [SerializeField] GameObject particleEffect;

    [SerializeField] GameObject scoreVisual;
    [SerializeField] Sprite spriteCoin;
    [SerializeField] Sprite spritePower;

    private EnemySpawner spawner;

    public void setSpawner(EnemySpawner _spawner)
    { 
        spawner = _spawner;
    }


    private void OnDestroy()
    {
        spawner.PinataDied();
        GameObject effect = Instantiate(particleEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);


        GameObject _scoreVisual = Instantiate(scoreVisual, transform.position, Quaternion.identity);

        if(coinDrop > 0)
        {

            _scoreVisual.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = spriteCoin;
            _scoreVisual.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "+" + coinDrop.ToString();
        }
        else
        {
            _scoreVisual.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = spritePower;
            _scoreVisual.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "+" + powerDrop.ToString();
        }

        Destroy(_scoreVisual, 1f);

    }
}
