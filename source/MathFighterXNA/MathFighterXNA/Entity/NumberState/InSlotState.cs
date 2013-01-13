﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathFighterXNA.Entity.NumberState
{
    public class InSlotState : INumberState  
    {
        public DragableNumber Owner;
        public NumberSlot Slot;

        public InSlotState(DragableNumber owner)
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
                Owner.Position = Slot.Position;
            }
        }
    }
}