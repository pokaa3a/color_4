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
    }
    public class Object
    {
        public class Character
        {
            public const string square = "Sprites/object/character/character_square";
            public const string squareSelected = "Sprites/object/character/character_square_selected";
            public const string circle = "Sprites/object/character/character_circle";
            public const string circleSelected = "Sprites/object/character/character_circle_selected";
            public const string triangle = "Sprites/object/character/character_triangle";
            public const string triangleSelected = "Sprites/object/character/character_triangle_selected";
        }
        public class Enemy
        {
            public const string minion = "Sprites/object/enemy/minion";
        }
        public class Effect
        {
            public const string attack = "Sprites/object/effect/attack";
            public const string attackAttempt = "Sprites/object/effect/attack_attempt";
            public const string move = "Sprites/object/effect/move";
            public const string reachable = "Sprites/object/effect/reachable";
        }
        public const string tower = "Sprites/object/tower";
        public const string towerClickable = "Sprites/object/tower_clickable";
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
        }

        public class Description
        {
            public const string attack = "Sprites/skill/description/attack";
            public const string attackConnected = "Sprites/skill/description/attack_connected";
            public const string attackSameColor = "Sprites/skill/description/attack_same_color";
        }
    }
}
