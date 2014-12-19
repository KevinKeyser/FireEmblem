using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public enum Team
    {
        Red = 0,
        Blue = 1,
        Green = 2
    }
    public class Character : GamePiece
    {
        private Team team;

        public Team Team
        {
            get { return team; }
            set { team = value; }
        }
        
        private Dictionary<AnimationName, Animation> animations;

        private Weapon weapon;

        public Weapon Weapon
        {
            get { return weapon; }
            set
            {
                if (value != null && characterClass.UsableWeapons.Contains(value.WeaponType))
                {
                    weapon = value;
                }
            }
        }

        private Item[] items = new Item[5];

        public Item[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
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
            set
            {
                currentHealth = MathHelper.Clamp(value, 0, TotalStats.HealthPoints);
                if (currentHealth == 0)
                {
                    IsDead = true;
                }
            }
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

        private AnimationName currentAnimation;
        public AnimationName CurrentAnimation
        {
            get
            {
                return currentAnimation;
            }
            set
            {
                if(currentAnimation != value)
                {
                    animations[currentAnimation].Reset();
                    currentAnimation = value;
                }
            }
        }
        private ClassName className;
        public ClassName ClassName
        {
            get
            {
                return className;
            }
            set
            {
                if (Info.Images[characterName].ContainsKey(value))
                {
                    className = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Character {0} cannot have ClassName {1}", characterName, value));
                }
            }
        }
        private CharacterName characterName;
        public Character(CharacterName characterName, Rectangle rectangle, CharacterStatistics characterStats, ClassName className, Team team)
            : base(Info.Images[characterName][className], rectangle, GamePieceType.Character)
        {
            this.characterName = characterName;
            this.team = team;
            weapon = null;
            this.className = className;
            this.characterClass = Info.Classes[className];
            this.characterStats = characterStats;
            currentHealth = TotalStats.HealthPoints;
            animations = Info.Animations[className][team];
            currentAnimation = AnimationName.Walking;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsDead)
            {
                animations[currentAnimation].Update(gameTime);
                sourceRectangle = animations[currentAnimation].SourceRectangle;
                origin = animations[currentAnimation].Origin;
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
            return string.Format("Class : {2}\nWeapon : {3}\nCurrent Hp : {0}\n{1}", currentHealth, TotalStats.ToString(), characterClass.Name, weapon);
        }
    }
}