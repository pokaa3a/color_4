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

}

public partial class CharacterManager
{
    private CharacterManager()
    {

    }

    public void SummonCharacters()
    {
        Character square = new Character(new Vector2Int(0, 0), CharacterType.Square);
        Character circle = new Character(new Vector2Int(0, 1), CharacterType.Circle);
        Character triangle = new Character(new Vector2Int(0, 2), CharacterType.Triangle);
        characters.Add(square);
        characters.Add(circle);
        characters.Add(triangle);
    }
}