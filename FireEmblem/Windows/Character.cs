using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    class Character : GamePiece
    {
        private CharacterClass characterClass;

        public CharacterClass CharacterClass
        {
            get { return characterClass; }
            set { characterClass = value; }
        }

        private CharacterStatistics characterStats;

        public CharacterStatistics TotalStats
        {
            get { return characterStats + characterClass.Stats; }
        }

        private bool hasMoved;

        public bool HasMoved
        {
            get { return hasMoved; }
            set { hasMoved = value; }
        }
        
        
        public Character(Texture2D texture, Rectangle rectangle, CharacterStatistics characterStats, CharacterClass characterClass)
            : base(texture, rectangle)
        {
            this.characterClass = characterClass;
            this.characterStats = characterStats;
        }
    }
}
