using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;
    private Animator animator;

    [SerializeField]
    [Range(0,1)]
    private float maximumInjuredLayerWeight;

    private float maxHealth = 100;
    private float currentHealth;
    private int injuredLayerIndex;
    private float layerWeightVelocity;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        injuredLayerIndex = animator.GetLayerIndex("Injured Layer");

        print($"Injured Layer Index: {injuredLayerIndex}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= maxHealth / 10;

            if (currentHealth < 0)
            {
                currentHealth = maxHealth;
            }
        }

        float healthPercentage = currentHealth / maxHealth;
        healthText.text = $"HP: {healthPercentage * 100}%";

        float currentInjuredLayerWeight = animator.GetLayerWeight(injuredLayerIndex);
        float targetInjuredLayerWeight = (1 - healthPercentage) * maximumInjuredLayerWeight;
        animator.SetLayerWeight(injuredLayerIndex, Mathf.SmoothDamp(currentInjuredLayerWeight,
            targetInjuredLayerWeight,
            ref layerWeightVelocity,
            0.2f));


    }
}
