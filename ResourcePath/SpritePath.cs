using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePath
{
    public class Tile
    {
        public const string empty = "Sprites/tile/tile_empty";
        public const string red = "Sprites/tile/tile_red";
        public const string yellow = "Sprites/tile/tile_yellow";
        public const string blue = "Sprites/tile/tile_blue";
        public const string green = "Sprites/tile/tile_green";
    }
    public class Object
    {
        public class Character
        {
            public const string square = "Sprites/mapObject/character/character_square";
            public const string squareSelected = "Sprites/mapObject/character/character_square_selected";
            public const string circle = "Sprites/mapObject/character/character_circle";
            public const string circleSelected = "Sprites/mapObject/character/character_circle_selected";
            public const string triangle = "Sprites/mapObject/character/character_triangle";
            public const string triangleSelected = "Sprites/mapObject/character/character_triangle_selected";
        }
        public class Enemy
        {
            public const string minion = "Sprites/mapObject/enemy/minion";
        }
        public class Effect
        {
            public const string attack = "Sprites/mapObject/effect/attack";
            public const string slashesRed = "Sprites/mapObject/effect/slashes_red";
            public const string slashesOrange = "Sprites/mapObject/effect/slashes_orange";
            public const string slashesGreen = "Sprites/mapObject/effect/slashes_green";
        }
        public const string tower = "Sprites/mapObject/tower";
        public const string towerClickable = "Sprites/mapObject/tower_clickable";
    }
    public class UI
    {
        public class Button
        {
            public const string endTurnButtonEnemy = "Sprites/ui/button/endTurnButton_enemy";
            public const string endTurnButtonPressed = "Sprites/ui/button/endTurnButton_pressed";
            public const string endTurnButtonUnpressed = "Sprites/ui/button/endTurnButton_unpressed";
        }
    }
    public class Skill
    {
        public class Icon
        {
            public const string empty = "Sprites/skill/icon/empty";
            public const string attack = "Sprites/skill/icon/attack";
            public const string attackConnected = "Sprites/skill/icon/attack_connected";
            public const string attackSameColor = "Sprites/skill/icon/attack_same_color";
            public const string selected = "Sprites/skill/icon/skill_selected";
        }

        public class Description
        {
            public const string attack = "Sprites/skill/description/attack";
            public const string attackConnected = "Sprites/skill/description/attack_connected";
            public const string attackSameColor = "Sprites/skill/description/attack_same_color";
        }
    }
}
