using UnityEngine;
using UnityEngine.UI;

public class CircularHPBar : MonoBehaviour
{
    [SerializeField] private Image healtBar;
    private EnemyController CurrenthpData = null;
    private float maxHp;
    private float lerpSpeed;

    private void Start()
    {
        EventManager.StartListening("onLock", setHealthBar);
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

    void setHealthBar(EventParam eventParam)
    {
        if (eventParam.enemyControllerParam)
        {
            CurrenthpData = eventParam.enemyControllerParam;
            maxHp = CurrenthpData.Health;
            HealtBarFiller();
            ColorChanger();
        }
    }
}
