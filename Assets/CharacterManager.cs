using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts;
using System;

public class CharacterManager : MonoBehaviour
{
    private List<Character> _characters;

    [SerializeField]
    private GameObject _characterPrefab;

    [SerializeField]
    private CharacterMoods _AcolitoMoods;

    private void Start()
    {
        _characters = new List<Character>();
    }
    public void CreateCharacter(string name , string position , string mood)
    {
        if(!Enum.TryParse(name,out CharacterName nameEnum))
        {
            Debug.LogWarning($"Fallo al parsear el nombre de personaje a enum: {name}");
            return;
        }
        if (!Enum.TryParse(position, out CharacterPosition positionEnum))
        {
            Debug.LogWarning($"Fallo al parsear el position de personaje a enum: {position}");
            return;
        }
        if (!Enum.TryParse(mood, out CharacterMood moodEnum))
        {
            Debug.LogWarning($"Fallo al parsear el mood de personaje a enum: {mood}");
            return;
        }

        CreateCharacter (nameEnum, positionEnum, moodEnum);
    }
    public void CreateCharacter(CharacterName name, CharacterPosition position, CharacterMood mood) 
    {
        var character = _characters.FirstOrDefault(x => x.Name == name);

        if (character == null)
        {
            var CharacterObject = Instantiate(_characterPrefab, gameObject.transform, false);

            character = CharacterObject.GetComponent<Character>();

            _characters.Add(character);
        }
        else if (character.IsShowing)
        {
            Debug.LogWarning($"error al mostrar el personaje {name}, ya esta en pantalla");
            return;
        }

        character.Init(name, position, mood, GetMoodSetForCharacter(name));
    }
    public void HideCharacter(string name)
    {
        if(!Enum.TryParse(name,out CharacterName nameEnum))
        {
            Debug.LogWarning($"Fallo al parsear el nombre del personaje a enum {name}");
            return ;
        }
        HideCharacter (nameEnum);
    }
    public void HideCharacter(CharacterName name)
    {
        var character = _characters.FirstOrDefault(_x => _x.Name == name);

        if(character?.IsShowing != true)
        {
            Debug.LogWarning($"Character{name} no está siendo mostrado, no se puede esconder");
        }
        else
        {
            character?.Hide();
        }
    }
    public void ChangeMood(string name, string mood)
    {
        if (!Enum.TryParse(name, out CharacterName nameEnum))
        {
            Debug.LogWarning($"Fallo al parsear el nombre de personaje a enum: {name}");
            return;
        }
        if (!Enum.TryParse(mood, out CharacterMood moodEnum))
        {
            Debug.LogWarning($"Fallo al parsear el mood de personaje a enum: {mood}");
            return;
        }
        ChangeMood(nameEnum, moodEnum);
    }
    public void ChangeMood(CharacterName name,CharacterMood mood)
    {
        var character = _characters.FirstOrDefault(_x => _x.Name == name);

        if (character?.IsShowing != true)
        {
            Debug.LogWarning($"Character{name} no está siendo mostrado, no se puede cambiar el mood");
        }
        else
        {
            character.ChangeMood(mood);
        }
    }
    private CharacterMoods GetMoodSetForCharacter (CharacterName name)
    {
        switch (name)
        {
            case CharacterName.Acolito:
                return _AcolitoMoods;
            default:
                Debug.LogError($"No se pudo encontrar el moodset de {name}");
                return null;
        }
    } 
}
