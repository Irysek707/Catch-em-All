using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatchemAll
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font, font2;
        private Song backgroundMusic;

        Player player;
        List<Enemy1> enemies1;
        List<Enemy2> enemies2;
        List<Fruit1> melons;
        List<Fruit2> kiwis;
        List<Life> lifes;

        Texture2D melontex, kiwitex, enemy1Tex, enemy2Tex, background, start;

        Random rand = new Random();

        TimeSpan lastEnemySpawn = new TimeSpan();
        TimeSpan lastEnemy2Spawn = new TimeSpan();
        TimeSpan lastMelonSpawn = new TimeSpan();
        TimeSpan lastKiwiSpawn = new TimeSpan();
        TimeSpan one = new TimeSpan(0, 0, 3);
        TimeSpan two = new TimeSpan(0, 0, 5);
        TimeSpan three = new TimeSpan(0, 0, 1);

        int score = 0;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 450;

        }

        protected override void Initialize()
        {
            player = new Player();
            player.Initialise();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");
            font2 = Content.Load<SpriteFont>("Font2");

            background = Content.Load<Texture2D>("Background");
            start = Content.Load<Texture2D>("Start");

            backgroundMusic = Content.Load<Song>("music");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;

            player.Load(Content);

            lifes = new List<Life>();
            lifes.Add(new Life(new Vector2(760, 15)));
            lifes.Add(new Life(new Vector2(720, 15)));
            lifes.Add(new Life(new Vector2(680, 15)));

            enemies1 = new List<Enemy1>();
            for (int i = 0; i < 2; i++)
            {
                CreateEnemy();
            }
            enemies2 = new List<Enemy2>();
            for (int i = 0; i < 1; i++)
            {
                CreateEnemy2();
            }

            melons = new List<Fruit1>();
            for (int i = 0; i < 2; i++)
            {
                CreateMelons();
            }

            kiwis = new List<Fruit2>();
            for (int i = 0; i < 4; i++)
            {
                CreateKiwis();
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            ///Enemy collision///

            List<Enemy1> collidingEnemies = new List<Enemy1>();

            foreach (Enemy1 enemy1 in enemies1)
            {
                enemy1.Update(gameTime);
                if (player.IsPlayerColliding(enemy1))
                {
                    if (lifes.Count > 0)
                    {
                        Life lastLife = lifes.Last();
                        lifes.Remove(lastLife);
                    }

                    collidingEnemies.Add(enemy1);
                }
            }

            foreach (Enemy1 enemy1 in collidingEnemies)
            {
                enemies1.Remove(enemy1);
            }

            if (gameTime.TotalGameTime - lastEnemySpawn >= one)
            {
                CreateEnemy();
                lastEnemySpawn = gameTime.TotalGameTime;
            }

            List<Enemy2> collidingEnemies2 = new List<Enemy2>();

            foreach (Enemy2 enemy2 in enemies2)
            {
                enemy2.Update(gameTime);
                if (player.IsPlayerCollidingE2(enemy2))
                {
                    collidingEnemies2.Add(enemy2);
                    score -= 50;
                }
            }

            foreach (Enemy2 enemy2 in collidingEnemies2)
            {
                enemies2.Remove(enemy2);
            }

            if (gameTime.TotalGameTime - lastEnemy2Spawn >= two)
            {
                CreateEnemy2();
                lastEnemy2Spawn = gameTime.TotalGameTime;
            }

            ///Fruit collision///

            List<Fruit1> collidingMelons = new List<Fruit1>();

            foreach (Fruit1 melon in melons)
            {
                melon.Update(gameTime);
                if (player.IsPlayerCollidingwithMelon(melon) && lifes.Count > 0)
                {
                    score += 50;
                    collidingMelons.Add(melon);
                }
            }

            foreach (Fruit1 melon in collidingMelons)
            {
                melons.Remove(melon);
            }

            if (gameTime.TotalGameTime - lastMelonSpawn >= one)
            {
                CreateMelons();
                lastMelonSpawn = gameTime.TotalGameTime;
            }

            List<Fruit2> collidingKiwis = new List<Fruit2>();

            foreach (Fruit2 kiwi in kiwis)
            {
                kiwi.Update(gameTime);
                if (player.IsPlayerCollidingwithKiwi(kiwi) && lifes.Count > 0)
                {
                    score += 10;
                    collidingKiwis.Add(kiwi);
                }
            }

            foreach (Fruit2 kiwi in collidingKiwis)
            {
                kiwis.Remove(kiwi);
            }

            if (gameTime.TotalGameTime - lastKiwiSpawn >= three)
            {
                CreateKiwis();
                lastKiwiSpawn = gameTime.TotalGameTime;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (lifes.Count > 0)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 450), Color.White);

                _spriteBatch.DrawString(font, $"Score: {score}", new Vector2(10, 15), Color.White);

                player.Draw(_spriteBatch);

                foreach (Life life in lifes)
                {
                    life.Load(Content);
                    life.Draw(_spriteBatch);
                }

                foreach (Enemy1 enemy1 in enemies1)
                {
                    enemy1.Draw(_spriteBatch);
                }

                foreach (Enemy2 enemy2 in enemies2)
                {
                    enemy2.Draw(_spriteBatch);
                }

                foreach (Fruit1 melon in melons)
                {
                    melon.Draw(_spriteBatch);
                }

                foreach (Fruit2 kiwi in kiwis)
                {
                    kiwi.Draw(_spriteBatch);
                }
            }

            else
            {
                MediaPlayer.Stop();

                _spriteBatch.Draw(start, new Rectangle(0, 0, 800, 450), Color.White);
                _spriteBatch.DrawString(font2, "Game over", new Vector2(320, 200), Color.White);
                _spriteBatch.DrawString(font, $"Score: {score}", new Vector2(350, 250), Color.White);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CreateEnemy()
        {
            Enemy1 e = new Enemy1(new Vector2(rand.Next(0, 750), 0), rand.Next(1, 3), enemy1Tex);
            e.Load(Content);
            enemies1.Add(e);
        }

        private void CreateEnemy2()
        {
            Enemy2 e2 = new Enemy2(new Vector2(rand.Next(0, 750), 0), rand.Next(1, 2), enemy2Tex);
            e2.Load(Content);
            enemies2.Add(e2);
        }

        private void CreateMelons()
        {
            Fruit1 m = new Fruit1(new Vector2(rand.Next(0, 750), 0), rand.Next(1, 3), melontex);
            m.Load(Content);
            melons.Add(m);
        }

        private void CreateKiwis()
        {
            Fruit2 k = new Fruit2(new Vector2(rand.Next(0, 750), 0), rand.Next(1, 3), kiwitex);
            k.Load(Content);
            kiwis.Add(k);
        }
    }
}
