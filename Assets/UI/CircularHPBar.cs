using UnityEngine;
using UnityEngine.UI;

public class CircularHPBar : MonoBehaviour
{
    [SerializeField] private Image healtBar;
    [SerializeField] private TestActiveOrbs hpData;
    private float lerpSpeed;

    void Update()
    {
        lerpSpeed = 5f * Time.deltaTime;
        HealtBarFiller();
        ColorChanger();
    }

    void HealtBarFiller()
    {
        healtBar.fillAmount = Mathf.Lerp(healtBar.fillAmount, hpData.Hp / hpData.MaxHp, lerpSpeed);
    }
    void ColorChanger()
    {
        Color healtColor = Color.Lerp(Color.red, Color.green, (hpData.Hp / hpData.MaxHp));
        healtBar.color = healtColor;
    }
}
