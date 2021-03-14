﻿using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Project1.StateManagement;
using tainicom.Aether.Physics2D.Dynamics;

namespace Project1.Screens
{
    // This screen implements the actual game logic. It is just a
    // placeholder to get the idea across: you'll probably want to
    // put some more interesting gameplay in here!
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private Vector2 _playerPosition = new Vector2(100, 100);
        private Vector2 _enemyPosition = new Vector2(100, 100);

        private readonly Random _random = new Random();

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        private World world;
        private List<BallSprite> balls;
        private FumikoSprite actor;
        private Song backgroundMusic;


        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back }, true);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");


            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void Initialize()
        {
            world = new World();
            world.Gravity = new Vector2(0f, 20f);
            var top = 0;
            var bottom = Constants.GAME_HEIGHT;
            var left = 0;
            var right = Constants.GAME_WIDTH;
            var edges = new Body[] {
               world.CreateEdge(new Vector2(left, top), new Vector2(right, top)),
               world.CreateEdge(new Vector2(left, top), new Vector2(left, bottom)),
               world.CreateEdge(new Vector2(left, bottom), new Vector2(right, bottom)),
               world.CreateEdge(new Vector2(right, top), new Vector2(right, bottom))
            };
            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1.0f);
            }

            int temp = 0;
            int temp1 = 0;
            System.Random random = new System.Random();
            balls = new List<BallSprite>();
            for (int i = 0; i < 10; i++)
            {
                var radius = 16;
                var position = new Vector2(50 + temp, 50 + temp1);
                temp += 75;
                temp1 += 25;
                var body = world.CreateCircle(radius, 1f, position, BodyType.Dynamic);
                body.LinearVelocity = new Vector2(
                    random.Next(-75, 75),
                    random.Next(-75, 75));
                body.IgnoreGravity = true;
                body.SetRestitution(1);
                body.AngularVelocity = (float)random.NextDouble() * MathHelper.Pi - MathHelper.PiOver2;
                balls.Add(new BallSprite(radius, body));
            }

            var positionf = new Vector2(10, 225);
            var bodyf = world.CreateRectangle(24, 32, 1f, positionf, 0, BodyType.Dynamic);
           
            bodyf.SetRestitution(0f);
            actor = new FumikoSprite(bodyf);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            foreach (var ball in balls) ball.LoadContent(content);
            actor.LoadContent(content);
            backgroundMusic = content.Load<Song>("little town - orchestral");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

      
            

        }

      

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                actor.Update(gameTime);

                if (actor.Colliding == true)
                {
                    //sound of colliding 
                    System.Windows.Forms.MessageBox.Show("You have Lost :(");
                    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
                }


                //Update balls
                foreach (var ball in balls) ball.Update(gameTime);

                //check for victory condition
                if (actor.body.Position.X > Constants.GAME_WIDTH - 24)
                {
                    System.Windows.Forms.MessageBox.Show("You have Won :)");
                    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
                    
                }

                world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            actor.Draw(gameTime, spriteBatch);
            foreach (var ball in balls) ball.Draw(gameTime, spriteBatch);
         

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
