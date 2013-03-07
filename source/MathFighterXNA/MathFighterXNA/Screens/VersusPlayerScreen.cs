﻿using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathFighterXNA.Entity;
using MathFighterXNA.Bang;
using MathFighterXNA.Bang.Actions;

namespace MathFighterXNA.Screens {

    public class VersusPlayerScreen : GameScreen {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public EquationInput Input { get; set; }

        public VersusPlayerScreen(KinectContext context) : base(context) {
        }

        public override void Init() {
            PlayerOne = new Player(Context, SkeletonPlayerAssignment.LeftSkeleton);
            PlayerTwo = new Player(Context, SkeletonPlayerAssignment.RightSkeleton);

            AddEntity(PlayerOne);
            AddEntity(PlayerTwo);

            for (int i = 1; i <= 10; i++) {
                double dy = System.Math.Pow((60 * i - 30) - 300, 2) * 0.002 + 15;
                AddEntity(new DragableNumber(PlayerOne, System.Convert.ToInt32((60 * i) - 30), System.Convert.ToInt32(dy), i));
            }

            AddInput();
        }

        private void AddInput() {
            Input = new EquationInput(300, 400);

            Input.Actions.AddAction(new TweenPositionTo(Input, new Vector2(100, 300), 1f, Tweening.Back.EaseOut), true);
            Input.Actions.AddAction(new WaitForEquationInput(Input, EquationInputType.Equation), true);

            Input.Actions.AddAction(new TweenPositionTo(Input, new Vector2(300, 200), 1f, Tweening.Back.EaseOut), true);
            Input.Actions.AddAction(new WaitForEquationInput(Input, EquationInputType.Product), true);

            Input.Actions.AddAction(new EndEquationInput(Input), true);

            AddEntity(Input);
        }

        public override void Update(GameTime gameTime) {
            //Dirty? Calling ToArray to make a copy of the entity collection preventing crashing when entities create other entities through an update call
            foreach (var ent in Entities.ToArray<BaseEntity>()) {
                ent.Update(gameTime);
            }

            if (Input.Actions.IsComplete()) {
                RemoveEntity(Input);
                AddInput();
            }
        }        

        public override void Draw(SpriteBatch spriteBatch) {
            foreach (var ent in Entities) {
                ent.Draw(spriteBatch);
            }            
        }
    }
}