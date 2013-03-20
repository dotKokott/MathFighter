﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClownSchool.Entity;
using ClownSchool.Entity.NumberState;

namespace ClownSchool {

    public class DragableNumber : BaseEntity {        

        public Player Owner { get; set; }
        public int Number { get; private set; }

        //States
        public INumberState State;
        public DefaultState DefaultState;

        private bool selected { get; set; }

        public DragableNumber(Player owner, int posX, int posY, int number) {
            Owner = owner;
            Position = new Point(posX, posY);
            Size = new Point(52, 56);
            Offset = new Point(5, 5);

            Number = number;

            CollisionType = "number";

            DefaultState = new DefaultState(this);

            State = DefaultState;
        }

        public override void Init() {

        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            var hand = (PlayerHand)GetFirstCollidingEntity(X, Y, "hand");
            if (hand != null) {
                State.OnHandCollide(hand);               
            }

            selected = hand != null;

            State.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Assets.BalloonSpritesheet, new Rectangle(X, Y, selected ? (int)(62 * 1.2f) : 62, selected ? (int)(89 * 1.2f) : 89), new Rectangle(62 * (Number - 1), 0, 62, 89), new Color(255, 255, 255, selected ? 255 : 100));

            State.Draw(spriteBatch);            
        }

        public override void Delete() {
        }
    }
}