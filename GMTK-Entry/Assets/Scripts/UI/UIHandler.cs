using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] DamageNumber numberPrefab;
    [SerializeField] private RectTransform canvasPointsRectTransform;

    [SerializeField] private TMP_Text pointsText;

    [SerializeField] private Image healthBar;

    private int actualPoints = 0;
    private int actualHealth;
    [SerializeField] 
    private int maxHealth;

    private void Start()
    {
        actualHealth = maxHealth;
    }

    public void AddPointsToMarker(int pointsToAdd, Vector3 position)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform)canvasPointsRectTransform, // No GetComponent needed, just cast it.
        screenPosition,
        Camera.main,              // Use null if canvas is Screenspace Overlay.
        out Vector2 localPoint    // The position is given as an out parameter.
        ))
        {
            DamageNumber damageNumber = numberPrefab.Spawn(Vector2.zero, pointsToAdd);
            damageNumber.SetAnchoredPosition(canvasPointsRectTransform, localPoint);
        }

        actualPoints += pointsToAdd;
        pointsText.text = actualPoints.ToString();
    }

    public void ReduceHealth()
    {
        actualHealth--;
        StartCoroutine(SmoothReduceHealthBar((float)actualHealth / (float)maxHealth));
    }

    IEnumerator SmoothReduceHealthBar(float desiredFillAmount)
    {
        while(healthBar.fillAmount > desiredFillAmount)
        {
            healthBar.fillAmount -= 0.2f * Time.deltaTime;
            yield return null;
        }
    }
}
