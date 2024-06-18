using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverStuff : MonoBehaviour
{
    [SerializeField] GameObject[] visuals;

    [SerializeField] Text continueText;
    void Start()
    {
        visuals[GameManager.THIS.winnerwinnerChickenDinner].SetActive(true);

        StartCoroutine(ShowBackButton());
    }

    private IEnumerator ShowBackButton()
    {
        yield return new WaitForSecondsRealtime(2f);

        int a = 0;

        while(continueText.color.a < 255)
        {
            a++;
            continueText.color = new Color(255, 255, 255, a);
            yield return new WaitForSeconds(0.1f);
        }

        
    }
}
