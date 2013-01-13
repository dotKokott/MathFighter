﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathFighterXNA.Entity;

namespace MathFighterXNA.Screens
{
    public class Playground : GameScreen //Single Player Screen for playing / testing
    {
        public Player Player { get; set; }
        
        public Playground(KinectContext context) : base(context)
        {
        }

        public override void Init()
        {
            Player = new Player(Context, SkeletonPlayerAssignment.FirstSkeleton, this);
            AddEntity(Player);

            AddEntity(new NumberSlot(100, 100));
            AddEntity(new DragableNumber(Player, 20, 20, 5));            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var ent in Entities)
                ent.Update(gameTime);
        }        

        public override void Draw(SpriteBatch spriteBatch)
        {            
            foreach (var ent in Entities)
                ent.Draw(spriteBatch);
        }
    }
}