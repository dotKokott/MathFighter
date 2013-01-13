﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathFighterXNA.Tweening;

namespace MathFighterXNA.Entity.NumberState
{
    public class MoveToSlotState : INumberState
    {
        public DragableNumber Owner;
        
        private NumberSlot slot;
        public NumberSlot Slot
        {
            get
            {
                return slot;
            }
            set
            {
                slot = value;

                snapToSlotTweenerX = new Tweener(Owner.X, value.X, 1f, Elastic.EaseOut);
                snapToSlotTweenerY = new Tweener(Owner.Y, value.Y, 1f, Elastic.EaseOut);

                snapToSlotTweenerX.Ended += delegate() { tweenerXFinished = true; };
                snapToSlotTweenerY.Ended += delegate() { tweenerYFinished = true; };
            }
        }

        private Tweener snapToSlotTweenerX;
        private Tweener snapToSlotTweenerY;

        private bool tweenerXFinished = false;
        private bool tweenerYFinished = false;

        public MoveToSlotState(DragableNumber owner)
        {
            Owner = owner;
        }        

        void INumberState.onHandCollide(PlayerHand hand)
        {            
        }

        void INumberState.onSlotCollide(NumberSlot slot)
        {            
        }

        void INumberState.Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Slot != null)
            {
                snapToSlotTweenerX.Update(gameTime);
                snapToSlotTweenerY.Update(gameTime);

                Owner.X = (int)snapToSlotTweenerX.Position;
                Owner.Y = (int)snapToSlotTweenerY.Position;

                if (tweenerXFinished && tweenerYFinished)
                {
                    Owner.State = Owner.InSlotState;
                    Owner.InSlotState.Slot = this.Slot;
                }
            }
            
        }
    }
}