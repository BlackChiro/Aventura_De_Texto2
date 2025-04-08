using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CharacterMoods : MonoBehaviour
{
    public CharacterName Name;
    public Sprite Sentado;
    public Sprite Maldecir;
    public Sprite PensandoPensamientos;

    public Sprite GetMoodSprite(CharacterMood mood)
    {
        switch (mood)
        {
            case CharacterMood.Sentado:
                return Sentado;
            case CharacterMood.Maldecir:
                return Maldecir ?? Sentado;
            case CharacterMood.PensandoPensamientos:
                return PensandoPensamientos ?? Sentado;
            default:
                Debug.Log($"No se encontró el Sprite para el personje: {Name}, mood: {mood}");
                return Sentado;
        }
    }
}
