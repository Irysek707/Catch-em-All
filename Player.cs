using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatchemAll
{
    public class Player
    {
        Texture2D texture;
        Vector2 position;
        float speed;
        Rectangle collisionRect;


        public Player()
        {

        }

        public void Initialise()
        {
            position = new Vector2(400, 300);
            speed = 5;
        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Character");
            collisionRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }

            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.X > 800 - texture.Width)
            {
                position.X = 800 - texture.Width;
            }

            collisionRect.X = (int)position.X;
            collisionRect.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

        public bool IsPlayerColliding(Enemy1 enemy1)
        {
            return collisionRect.Intersects(enemy1.CollisionRect);
        }
        public bool IsPlayerCollidingE2(Enemy2 enemy2)
        {
            return collisionRect.Intersects(enemy2.CollisionRect);
        }
        public bool IsPlayerCollidingwithMelon(Fruit1 melon)
        {
            return collisionRect.Intersects(melon.CollisionRect);
        }
        public bool IsPlayerCollidingwithKiwi(Fruit2 kiwi)
        {
            return collisionRect.Intersects(kiwi.CollisionRect);
        }
    }
}
