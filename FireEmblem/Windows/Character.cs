using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class Character : GamePiece
    {
        private Weapon weapon;

        public Weapon Weapon
        {
            get { return weapon; }
            set { 
                if(characterClass.UsableWeapons.Contains(value.WeaponType))
                {
                    weapon = value; 
                }
            }
        }

        public bool IsDead = false;
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
        private int currentHealth;

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }
        
        private bool hasMoved;

        public bool HasMoved
        {
            get { return hasMoved; }
            set { hasMoved = value; }
        }

        bool hasAttacked = false;

        public bool HasAttacked
        {
            get { return hasAttacked; }
            set { hasAttacked = value; }
        }
        
        public Character(Texture2D texture, Rectangle rectangle, CharacterStatistics characterStats, CharacterClass characterClass)
            : base(texture, rectangle, GamePieceType.Character)
        {
            weapon = null;
            this.characterClass = characterClass;
            this.characterStats = characterStats;
            currentHealth = TotalStats.HealthPoints;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsDead)
            {
                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
            {
                base.Draw(spriteBatch);
            }
        }

        public override string ToString()
        {
            return string.Format("Current Hp : {0}\n{1}", currentHealth, TotalStats.ToString());
        }
    }
}
