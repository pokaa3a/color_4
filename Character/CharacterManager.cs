using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterManager
{
    // [public]

    // [private]
    private List<Character> characters = new List<Character>();
}

public partial class CharacterManager
{
    private Character _selectedCharacter = null;
    public Character selectedCharacter
    {
        get => _selectedCharacter;
        set
        {
            if (_selectedCharacter != null && value != null)
            {
                if (_selectedCharacter != value)
                {
                    _selectedCharacter.selected = false;
                    value.selected = true;
                }
            }
            else if (_selectedCharacter != null && value == null)
            {
                _selectedCharacter.selected = false;
            }
            else if (_selectedCharacter == null && value != null)
            {
                value.selected = true;
            }
            _selectedCharacter = value;
        }
    }

    // Singleton
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CharacterManager();
            }
            return _instance;
        }
    }
}

public partial class CharacterManager
{
    public void SummonCharacters()
    {
        Character square = new Character(new Vector2Int(0, 0), CharacterType.Square);
        Character circle = new Character(new Vector2Int(0, 1), CharacterType.Circle);
        Character triangle = new Character(new Vector2Int(0, 2), CharacterType.Triangle);
        characters.Add(square);
        characters.Add(circle);
        characters.Add(triangle);
    }

    // Called when player's turn starts
    public void ResetCharacters()
    {
        foreach (Character character in characters)
        {
            character.hasMoved = false;
            character.hasUsedSkill = false;
        }
    }

    private CharacterManager() { }
}