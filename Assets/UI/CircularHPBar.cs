using UnityEngine;
using UnityEngine.UI;

public class CircularHPBar : MonoBehaviour
{
    [SerializeField] private Image healtBar;
    [SerializeField] private EnemyController hpData;
    private float maxHp;
    private float lerpSpeed;

    private void Start()
    {
        maxHp = hpData.Health;
    }

    void Update()
    {
        lerpSpeed = 5f * Time.deltaTime;
        HealtBarFiller();
        ColorChanger();
    }

    void HealtBarFiller()
    {
        healtBar.fillAmount = Mathf.Lerp(healtBar.fillAmount, hpData.Health / maxHp, lerpSpeed);
    }
    void ColorChanger()
    {
        Color healtColor = Color.Lerp(Color.red, Color.green, (hpData.Health / maxHp));
        healtBar.color = healtColor;
    }
}
