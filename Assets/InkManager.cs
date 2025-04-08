using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts;

public class InkManager : MonoBehaviour
{

    [SerializeField]
    private TextAsset _inkJsonAsset;
    private Story _story;

    [SerializeField]
    private TMP_Text _textField;

    [SerializeField]
    private VerticalLayoutGroup _choiceButtonsContainer;

    [SerializeField]
    private Button _choiceButtonPrefab;

    [SerializeField]
    private Color _normalTextColor;

    [SerializeField]
    private Color _pensamientoTextColor;

    private CharacterManager _characterManager;

    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _characterManager = FindObjectOfType<CharacterManager>();
        _audioManager = FindObjectOfType<AudioManager>();
       StartStory();
    }

   

    void StartStory()
    {
        _story = new Story(_inkJsonAsset.text);

        _story.BindExternalFunction("ShowCharacter",(string name, string position,string mood)=> _characterManager.CreateCharacter(name, position, mood));
   
        _story.BindExternalFunction("HideCharacter",(string name) => Debug.Log($"Esconde el personaje llamado: {name}"));

        _story.BindExternalFunction("ChangeMood", (string name, string mood) => _characterManager.ChangeMood(name, mood));

        _story.BindExternalFunction("SwitchSong",()=> _audioManager.SwitchSong());

        DisplayNextLine();
    }

    public void DisplayNextLine() //muestra la siguiente linea
    {
        if (_story.canContinue)
        {
            string text = _story.Continue(); //Recoge la siguiente linea
            text = text?.Trim(); //recortar el espacio blanco del texto
            ApplyStyling();
            _textField.text = text; //muestra en la cajita de texto el nuevo texto
        } 

        else if (_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        
    }

    private void DisplayChoices()
    {
        if (_choiceButtonsContainer.GetComponentsInChildren<Button>().Length > 0) return;
        for (int i = 0; i < _story.currentChoices.Count; i++)
        {
            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text);

            button.onClick.AddListener(()=> OnClickButtonChoice(choice));
        }
    }

    Button CreateChoiceButton(string text)
    {
        var choiceButton = Instantiate(_choiceButtonPrefab);
        choiceButton.transform.SetParent(_choiceButtonsContainer.transform, false);

        var buttonText = choiceButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = text; 

        return choiceButton;
    }

    void OnClickButtonChoice(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        ClearChoiceView();
        _story.Continue();
        DisplayNextLine();
        
    }

    void ClearChoiceView()
    {
        if (_choiceButtonsContainer != null)
        {
            foreach (var button in _choiceButtonsContainer.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
    }

    private void ApplyStyling()
    {
        if (_story.currentTags.Contains("pensamiento"))
        {
            _textField.color = _pensamientoTextColor;
            //se pone cursiva
        }
        else
        {
            _textField.color = _normalTextColor;
            //estilo normal
        }
    }
}
