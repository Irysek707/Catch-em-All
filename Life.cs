using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatchemAll
{
    public class Life
    {
        Texture2D texture;
        Vector2 position;

        public Life(Vector2 pos)
        {
            position = pos;
        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Life");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

    }
}
