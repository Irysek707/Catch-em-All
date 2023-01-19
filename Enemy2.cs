using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatchemAll
{
    public class Enemy2
    {
        Texture2D texture;
        Vector2 position;
        float speed;
        Rectangle collisionRect;
        public Rectangle CollisionRect { get { return collisionRect; } }

        public Enemy2(Vector2 pos, float s, Texture2D tex)
        {
            position = pos;
            speed = s;
            texture = tex;
        }

        public void Initialise()
        {

        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Bad02");
            collisionRect = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void Update(GameTime gt)
        {
            position.Y += speed;

            collisionRect.X = (int)position.X;
            collisionRect.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
