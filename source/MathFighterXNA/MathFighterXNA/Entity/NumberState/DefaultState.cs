﻿using ClownSchool.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClownSchool.Entity.NumberState {

    public class DefaultState : INumberState {
        public DragableNumber Owner;

        private Tweener defaultMoveTweener;

        double maxHoverTime = .7;
        double hoverTime = 0;

        public DefaultState(DragableNumber owner) {
            Owner = owner;

            defaultMoveTweener = new Tweener(owner.Y, owner.Y + 5, 1f, ClownSchool.Tweening.Quadratic.EaseInOut);
            defaultMoveTweener.Ended += delegate() { defaultMoveTweener.Reverse(); };
        }

        void INumberState.OnHandCollide(PlayerHand hand) {
        }

        void INumberState.OnSlotCollide(NumberSlot slot) {
        }

        void INumberState.Update(Microsoft.Xna.Framework.GameTime gameTime) {           
            defaultMoveTweener.Update(gameTime);
            Owner.Y = (int)defaultMoveTweener.Position;

            //TODO: Maybe dirty, should use OnHandCollide somehow, because I query the colliding hand two times, once in number and then here
            var hand = (PlayerHand)Owner.GetFirstCollidingEntity("hand");

            if (hand != null && hand.Player == Owner.Owner && hand.DraggingBalloon == null) {
                if (Configuration.GRABBING_ENABLED) {
                    if (hand.IsGrabbing) {
                        var balloon = new Balloon(hand.X, hand.Y, Owner.Number);
                        hand.Screen.AddEntity(balloon);

                        hand.Grab(balloon);
                    }
                } else {
                    hoverTime += gameTime.ElapsedGameTime.TotalSeconds;
                }
            } else {
                hoverTime = 0;
            }

            if (hoverTime > maxHoverTime) {
                var balloon = new Balloon(hand.X, hand.Y, Owner.Number);
                hand.Screen.AddEntity(balloon);

                hand.Grab(balloon);
            }
        }

        void INumberState.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) {
            if (hoverTime > 0 && hoverTime <= maxHoverTime) {
                for (int i = 0; i <= 360; i += 5) {
                    var destRect = new Rectangle(Owner.BoundingBox.Center.X - 2, Owner.BoundingBox.Center.Y + 48, 1, 14);

                    var asset = Assets.CirclePartFilled;
                    if ((360 / maxHoverTime) * hoverTime <= i) {
                        asset = Assets.CirclePartEmpty;
                    } 
                    
                    spriteBatch.Draw(asset, destRect, null, Color.White, MathHelper.ToRadians(i), new Vector2(0, 13), SpriteEffects.None, 0);                    
                }
            }
        }
    }
}
