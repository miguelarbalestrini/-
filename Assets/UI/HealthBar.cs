using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField]public Slider slider;
	[SerializeField] public Gradient gradient;
	[SerializeField] public Image fill;
	private float lerpSpeed;

    private void Update()
    {
		lerpSpeed = 3f * Time.deltaTime;
    }
    public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(float health)
	{
		slider.value = health;
		Mathf.Lerp(slider.value, slider.value / slider.maxValue, lerpSpeed);
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
