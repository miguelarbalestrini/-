using UnityEngine;
using UnityEngine.UI;

public class CircularHPBar : MonoBehaviour
{
    [SerializeField] private Image healtBar;
    private EnemyController CurrenthpData = null;
    private float maxHp;
    private float initialHp;
    private float currentHp;
    bool resetedHp = false;
    bool filledOnce = false;
    private float lerpSpeed;

    EnemyController currentEnemy;
    EnemyController enemyController;
    private void Start()
    {
     /*
        maxHp = hpData.Health;
        currentEnemy = hpData;
        maxHp = currentEnemy.Health;
        initialHp = hpData.Health;
      */
        EventManager.StartListening("onLock", setHealthBar);
        //EventManager.RaiseEvent("onLock");
    }

    void Update()
    {
       
        if (CurrenthpData != null )
        {
            lerpSpeed = 5f * Time.deltaTime;
            HealtBarFiller();
            ColorChanger();
        }
      
    }

    void HealtBarFiller()
    {
        healtBar.fillAmount = Mathf.Lerp(healtBar.fillAmount, CurrenthpData.Health / maxHp, lerpSpeed);
        Debug.Log(healtBar.fillAmount);
    }

    void ColorChanger()
    {
        Color healtColor = Color.Lerp(Color.red, Color.green, (CurrenthpData.Health / maxHp));
        Debug.Log(healtColor);
        healtBar.color = healtColor;
    }

    void setHealthBar(EventParam eventParam )
    {
        if (eventParam.enemyControllerParam)
        {
            CurrenthpData = eventParam.enemyControllerParam;
            maxHp = CurrenthpData.Health;
            HealtBarFiller();
            ColorChanger();
            Debug.Log($"{CurrenthpData.gameObject.name}: {CurrenthpData.Health}");
            Debug.Log($"MAXHP: {maxHp}");
        }
    }
}
