using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TextMeshPro))]
public class CharacterDamagePopup : MonoBehaviour
{
    
    public TextMeshPro TxtStatValue;

    //TODO: Parameterised colour
    private Color32 PlayerDamageTakenColour = new Color32(255,255,255,220);
    private Color32 EnemyDamageTakenColour = new Color32(255,105,105,220);

    private StatChangeType statChangeType = StatChangeType.unknown;
    private float value;
    private bool started = false;

    public void Display(Vector2 location, StatChangeType statChanged, float amount, float amountFontsizeMultiplier)
    {
        SetPosition(location);
        value = amount;
        statChangeType = statChanged;
        started = true;

        TxtStatValue.fontSize = amount * amountFontsizeMultiplier;

        string textValue = StringHelper.ConvertIntToHumanReadableString((int)amount);

        switch (statChanged)
        {
            case StatChangeType.PlayerDamageTaken:
                TxtStatValue.color = PlayerDamageTakenColour;
                textValue = $"-{textValue}";
                break;            
            case StatChangeType.EnemyDamageTaken:
                TxtStatValue.color = EnemyDamageTakenColour;
                textValue = $"-{textValue}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(statChanged), statChanged, null);
        }

        TxtStatValue.text = textValue;
    }

    //TODO: Parameterised spread
    private const float maxSpawnSpread = 0.4f;
    private const float minSpawnSpread = -0.4f;
    
    private void SetPosition(Vector2 location)
    {
        Vector2 newPosition;
        float xSpread = Random.Range(minSpawnSpread, maxSpawnSpread);
        float xPosition = location.x + xSpread;
        float ySpread = Random.Range(minSpawnSpread, maxSpawnSpread);
        float yPosition = location.y + ySpread;
        newPosition = new Vector2(xPosition, yPosition);
        transform.position = newPosition;
    }

    private void OnEnable()
    {
        StartCoroutine("SelfDestruct");
    }
    
    private void Update()
    {
            //LeanTween.moveLocal(TxtStatValue.gameObject, Vector3.left, 1);
            if (TxtStatValue.fontSize > 0.05f)
            {
                TxtStatValue.fontSize = TxtStatValue.fontSize - 0.01f;
            }
            else
            {
                TxtStatValue.fontSize = 0;
            }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

public enum StatChangeType
{
    unknown,
    PlayerDamageTaken,
    EnemyDamageTaken,
}
