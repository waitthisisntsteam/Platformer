using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Practice_Scroller
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// add swords and enemies
    public class Game1 : Game
    {
        List<Rectangle> hitBoxes = new List<Rectangle>();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite Character;
        Sprite Platform1;
        Sprite Platform2;
        Sprite Floor;
        Sprite Portal;
        Sprite Spikes;
        Sprite Wall;
        Sprite end;
        Sprite Floor_R;
        Sprite Lava;
        Sprite Fade;
        Sprite SidePlat;
        Sprite Background;
        Sprite Warning;
        Sprite Fireball1;
        Sprite Fireball2;
        Sprite Fireball3;
        Texture2D SpriteSheet;
        Texture2D BossSprites;

        SpriteEffects direction = SpriteEffects.FlipHorizontally;
        SpriteEffects e_d = SpriteEffects.FlipHorizontally;

        TimeSpan updateIdleTime = TimeSpan.FromMilliseconds(400);
        TimeSpan elapsedIdleTime = TimeSpan.Zero;
        int idle = 0;

        TimeSpan updateRunningTime = TimeSpan.FromMilliseconds(150);
        TimeSpan elapsedRunningTime = TimeSpan.Zero;
        int running = 0;

        TimeSpan elapsedBossTime = TimeSpan.Zero;

        TimeSpan updateLavaTime = TimeSpan.FromMilliseconds(450);
        TimeSpan elapsedLavaTime = TimeSpan.Zero;

        TimeSpan updateWarningTime = TimeSpan.FromMilliseconds(800);
        TimeSpan elapsedWarningTime = TimeSpan.Zero;

        Vector2 Scale;
        Vector2 InitialScale = Vector2.One;

        List<Frame> idleFrames = new List<Frame>();
        List<Frame> runningFrames = new List<Frame>();
        List<Frame> e_run = new List<Frame>();
        List<Frame> e_attack = new List<Frame>();
        Frame jumpFrame;
        Frame crouchFrame;
        Frame e_entranceFrame;
        Frame e_idle;
        Frame hurt;

        int jumpHeight = 0;
        int platform1Speed = 3;
        int platform2Speed = -3;
        int currentIdleFrame = 0;
        int currentRunningFrame = 0;
        bool goUp = false;
        bool crouch = false;
        bool onPlatform = false;
        bool jump = false;
        bool iAttack = false;

        bool enter = false;
        bool run = false;
        bool attack = false;
        bool eIdle = false;
        bool eHurt = false;

        Rectangle CharacterHB;
        Rectangle Platform1HB;
        Rectangle Platform2HB;
        Rectangle FloorHB;
        Rectangle PortalHB;
        Rectangle SpikesHB;
        Rectangle WallHB;
        Rectangle Floor_RHB;
        Rectangle LavaHB;
        Rectangle SidePlatHB;
        Rectangle Fireball1HB;
        Rectangle Fireball2HB;
        Rectangle Fireball3HB;

        bool l1 = true;
        bool l2 = false;
        bool l3 = false;
        bool l4 = false;

        bool GameOver = false;

        int j = 1;
        int h = 0;

        bool lavaRise = false;
        bool warning = false;

        int lives = 3;

        public void Jump()
        {
            if (goUp == true)
            {
                jump = true;
                Character.Position.Y -= jumpHeight;
                if (jumpHeight < 15)
                {
                    jumpHeight += 1;
                }
                else if (jumpHeight == 15)
                {
                    jumpHeight = 0;
                    goUp = false;
                }
            }
        }

        bool gameEnd = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here            
            
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteSheet = Content.Load<Texture2D>("sprite sheet");
            BossSprites = Content.Load<Texture2D>("boss sprites");

            Platform1 = new Sprite(new Vector2(5, GraphicsDevice.Viewport.Height - 92), Color.White, Content.Load<Texture2D>("platfoorm"));
            Platform2 = new Sprite(new Vector2(GraphicsDevice.Viewport.Width - Platform1.Image.Width - 5, GraphicsDevice.Viewport.Height - 178), Color.White, Content.Load<Texture2D>("platfoorm"));
            Character = new Sprite(new Vector2(0, GraphicsDevice.Viewport.Height - 50), Color.White, Content.Load<Texture2D>("characteer"));
            Floor = new Sprite(new Vector2(0, GraphicsDevice.Viewport.Height - 280), Color.White, Content.Load<Texture2D>("flooor"));
            Portal = new Sprite(new Vector2(0, Floor.Position.Y - 55), Color.White, Content.Load<Texture2D>("portaal"));
            Spikes = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("spikees"));
            Wall = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("wall"));
            Floor_R = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("floor_reversed"));
            Lava = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("lava"));
            Fade = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("Fade"));
            SidePlat = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("side_platform"));
            Background = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("background"));
            Warning = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("warning"));
            Fireball1 = new Sprite(new Vector2(1600, 0), Color.White, Content.Load<Texture2D>("Fireball"));
            Fireball2 = new Sprite(new Vector2(1600, 0), Color.White, Content.Load<Texture2D>("Fireball"));
            Fireball3 = new Sprite(new Vector2(1600, 0), Color.White, Content.Load<Texture2D>("Fireball"));
            end = new Sprite(new Vector2(0, 0), Color.White, Content.Load<Texture2D>("hpw"));

            int x = 0;
            for (int i = 0; i < 7; i++)
            {
                if (i == 0)
                {
                    x = 0;
                }
                else if (i == 1)
                {
                    x = 61;
                }
                else if (i == 2)
                {
                    x = 133;
                }
                else if (i == 3)
                {
                    x = 196;
                }
                else if (i == 4)
                {
                    x = 259;
                }
                else if (i == 5)
                {
                    x = 311;
                }
                else if (i == 6)
                {
                    x = 372;
                }
                Frame frame = new Frame(Vector2.Zero, new Rectangle(x, 107, 48, 69));
                idleFrames.Add(frame);
            }

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    x = 0;
                }
                if (i == 1)
                {
                    x = 65;
                }
                if (i == 2)
                {
                    x = 130;
                }
                if (i == 3)
                {
                    x = 195;
                }
                Frame frame = new Frame(Vector2.Zero, new Rectangle(x, 28, 48, 69));
                runningFrames.Add(frame);
            }

            for (int i = 0; i < 9; i++)
            {
                if (i == 0)
                {
                    e_entranceFrame = new Frame(Vector2.Zero, new Rectangle(6, 359, 50, 61));
                }
                if (i == 1)
                {
                    Frame frame = new Frame(Vector2.Zero, new Rectangle(8, 146, 50, 61));
                    e_run.Add(frame);
                }
                if (i == 2)
                {
                    Frame frame = new Frame(Vector2.Zero, new Rectangle(64, 146, 50, 61));
                    e_run.Add(frame);
                }
                if (i == 3)
                {
                    Frame frame = new Frame(Vector2.Zero, new Rectangle(16, 432, 50, 61));
                    e_attack.Add(frame);
                }
                if (i == 4)
                {
                    Frame frame = new Frame(Vector2.Zero, new Rectangle(84, 433, 50, 61));
                    e_attack.Add(frame);
                }
                if (i == 5)
                {
                    Frame frame = new Frame(Vector2.Zero, new Rectangle(157, 433, 50, 61));
                    e_attack.Add(frame);
                }
                if (i == 6)
                {
                    Frame frame = new Frame(Vector2.Zero, new Rectangle(220, 435, 50, 61)); 
                    e_attack.Add(frame);
                }
                if (i == 7)
                {
                    e_idle = e_attack[0];
                }
                if (i == 8)
                {
                    
                }
            }

            jumpFrame = new Frame(Vector2.Zero, new Rectangle(0, 188, 48, 69));

            crouchFrame = new Frame(Vector2.Zero, new Rectangle(0, 262, 48, 69));

            hurt = new Frame(Vector2.Zero, new Rectangle(13, 215, 50, 61));

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || GameOver == true)
                Exit();

            // TODO: Add your update logic here

            CharacterHB = new Rectangle((int)Character.Position.X, (int)Character.Position.Y, Character.Image.Width, Character.Image.Height);
            PortalHB = new Rectangle((int)Portal.Position.X, (int)Portal.Position.Y, Portal.Image.Width, Portal.Image.Height);

            if (iAttack == true)
            {
                elapsedBossTime += gameTime.ElapsedGameTime;
                if (elapsedBossTime < TimeSpan.FromMilliseconds(32000))
                {
                    eHurt = true;
                    attack = false;
                    eIdle = false;
                    enter = false;
                }
                else if (elapsedBossTime < TimeSpan.FromMilliseconds(35000))
                {
                    int i = (int)Platform1.Position.Y;
                    i+=4;
                    Platform1.Position.Y = i;
                }
                else if (elapsedBossTime > TimeSpan.FromMilliseconds(35000))
                {
                    gameEnd = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        l1 = true;
                        l2 = false;
                        l3 = false;
                        l4 = false;

                        Platform1.Position = new Vector2(5, GraphicsDevice.Viewport.Height - 92);
                        Platform2.Position = new Vector2(GraphicsDevice.Viewport.Width - Platform1.Image.Width - 5, GraphicsDevice.Viewport.Height - 178);
                        Character.Position = new Vector2(0, GraphicsDevice.Viewport.Height - 50);
                        Floor.Position = new Vector2(0, GraphicsDevice.Viewport.Height - 280);
                        Portal.Position = new Vector2(0, Floor.Position.Y - 55);
                        Fireball1.Position = new Vector2(1600, 0);
                        Fireball2.Position = new Vector2(1600, 0);
                        Fireball3.Position = new Vector2(1600, 0);

                        j = 1;
                        h = 0;
                        platform1Speed = 3;
                        platform2Speed = 3;

                        warning = false;
                        lavaRise = false;

                        elapsedBossTime = TimeSpan.Zero;
                        elapsedIdleTime = TimeSpan.Zero;
                        elapsedLavaTime = TimeSpan.Zero;
                        elapsedRunningTime = TimeSpan.Zero;
                        elapsedWarningTime = TimeSpan.Zero;
                        lives = 3;
                        iAttack = false;
                        gameEnd = false;
                    }
                }
            }
            else
            {
                //Game Functions
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        crouch = true;
                        Character.Position.X += 2;
                    }
                    else
                    {
                        Character.Position.X += 4;
                    }
                    idle = 0;
                    direction = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    idle++;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        crouch = true;
                        Character.Position.X -= 2;
                    }
                    else
                    {
                        Character.Position.X -= 4;
                    }
                    idle = 0;
                    direction = SpriteEffects.None;
                }
                else
                {
                    idle++;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (Character.Position.Y + Character.Image.Height == GraphicsDevice.Viewport.Height)
                    {
                        goUp = true;
                    }
                    else if (onPlatform == true)
                    {
                        goUp = true;
                        onPlatform = false;
                    }
                    idle = 0;
                    running = 0;
                }
                else
                {
                    idle++;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    crouch = true;
                    running = 0;
                    idle = 0;
                    Scale = new Vector2(1f, 0.9f);
                }
                else
                {
                    idle++;
                    crouch = false;
                    Scale = InitialScale;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    running = 1;
                }
                else
                {

                    running = 0;
                }

                Jump();

                if (idle > 3)
                {
                    elapsedIdleTime += gameTime.ElapsedGameTime;
                    if (elapsedIdleTime > updateIdleTime)
                    {
                        elapsedIdleTime = TimeSpan.Zero;
                        currentIdleFrame++;
                        if (currentIdleFrame == 6)
                        {
                            currentIdleFrame = 0;
                        }
                    }

                }
                else
                {
                    currentIdleFrame = 0;
                }
                if (running > 0)
                {
                    elapsedRunningTime += gameTime.ElapsedGameTime;
                    if (elapsedRunningTime > updateRunningTime)
                    {
                        elapsedRunningTime = TimeSpan.Zero;
                        currentRunningFrame++;
                        if (currentRunningFrame == 3)
                        {
                            currentRunningFrame = 0;
                        }
                    }
                }
                else
                {
                    currentRunningFrame = 0;
                }

                if (crouch == false)
                {
                    Character.Image = Content.Load<Texture2D>("characteer");
                }

                if (goUp == false && onPlatform == false && Character.Position.Y < GraphicsDevice.Viewport.Height - CharacterHB.Height)
                {
                    Character.Position.Y += 6;
                    jump = true;
                }
                if (onPlatform == true || Character.Position.Y > GraphicsDevice.Viewport.Height - CharacterHB.Height)
                {
                    jump = false;
                }

                if (Character.Position.Y + Character.Image.Height > GraphicsDevice.Viewport.Height)
                {
                    Character.Position.Y = GraphicsDevice.Viewport.Height - Character.Image.Height;
                }
                if (Character.Position.X < 0)
                {
                    Character.Position.X = 0;
                }
                if (Character.Position.X + Character.Image.Width > GraphicsDevice.Viewport.Width)
                {
                    Character.Position.X = GraphicsDevice.Viewport.Width - Character.Image.Width;
                }

                if (CharacterHB.Intersects(PortalHB))
                {
                    if (l1 == true)
                    {
                        l2 = true;
                        l1 = false;

                        Platform1.Position = new Vector2(0, GraphicsDevice.Viewport.Height - Platform1.Image.Height);
                        Character.Position = new Vector2(0, GraphicsDevice.Viewport.Height - Character.Image.Height - 90);
                        Spikes.Position = new Vector2(Platform1.Position.X + Platform1.Image.Width + 70, GraphicsDevice.Viewport.Height - Spikes.Image.Height);
                        Wall.Position = new Vector2(Spikes.Position.X + Spikes.Image.Width, GraphicsDevice.Viewport.Height - Wall.Image.Height);
                        Floor_R.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - 20, Wall.Position.Y - 20);
                        Portal.Position = new Vector2(GraphicsDevice.Viewport.Width - Portal.Image.Width, (int)Floor_R.Position.Y - Portal.Image.Height);
                        Lava.Position = new Vector2(Wall.Position.X + Wall.Image.Width, GraphicsDevice.Viewport.Height - Lava.Image.Height);
                    }
                    else if (l2 == true)
                    {
                        l2 = false;
                        l3 = true;

                        Character.Position = new Vector2(0, 0);
                        Floor.Position = new Vector2(0, 70);
                        Platform1.Position = new Vector2(GraphicsDevice.Viewport.Width + Platform1.Image.Width, 190);
                        Wall.Position = new Vector2(555, Platform1.Position.Y);
                        Lava.Position = new Vector2(0, Wall.Position.Y + Wall.Image.Height - 20);
                        Fade.Position = new Vector2(0, Wall.Position.Y + Wall.Image.Height);
                        Portal.Position = new Vector2(Wall.Position.X - Portal.Image.Width, Wall.Position.Y + 50);
                        SidePlat.Position = new Vector2(Portal.Position.X - 15, Portal.Position.Y + Portal.Image.Height);
                        platform1Speed = 2;
                    }
                    else if (l3 == true)
                    {
                        l3 = false;
                        l4 = true;

                        Character.Position = new Vector2(0, 0);
                        Platform1.Position = new Vector2(0, 100);
                        Lava.Position = new Vector2(0, GraphicsDevice.Viewport.Height + 10);
                        Platform2.Position = new Vector2(1600, 1600);
                        platform1Speed = 1;
                    }
                }

                hitBoxes.Clear();
                if (l1 == true)
                {
                    Platform1HB = new Rectangle((int)Platform1.Position.X - 20, (int)Platform1.Position.Y, Platform1.Image.Width, Platform1.Image.Height + 1);
                    Platform2HB = new Rectangle((int)Platform2.Position.X - 20, (int)Platform2.Position.Y, Platform2.Image.Width, Platform2.Image.Height + 1);
                    FloorHB = new Rectangle((int)Floor.Position.X, (int)Floor.Position.Y, Floor.Image.Width, Floor.Image.Height);

                    hitBoxes.Add(Platform1HB);
                    hitBoxes.Add(Platform2HB);
                    hitBoxes.Add(FloorHB);

                    Platform1.Position.X += platform1Speed;
                    if (Platform1.Position.X + Platform1.Image.Width > GraphicsDevice.Viewport.Width - 5)
                    {
                        platform1Speed = -Math.Abs(platform1Speed);
                    }
                    if (Platform1.Position.X < 5)
                    {
                        platform1Speed = Math.Abs(platform1Speed);
                    }
                    Platform2.Position.X += platform2Speed;
                    if (Platform2.Position.X + Platform2.Image.Width > GraphicsDevice.Viewport.Width - 5)
                    {
                        platform2Speed = -Math.Abs(platform2Speed);
                    }
                    if (Platform2.Position.X < 5)
                    {
                        platform2Speed = Math.Abs(platform2Speed);
                    }

                    if (idle > 3)
                    {
                        if (onPlatform == true && CharacterHB.Y > FloorHB.Y)
                        {
                            if (CharacterHB.X > Platform2HB.X && CharacterHB.X < Platform2HB.X + Platform2HB.Width && CharacterHB.Y < Platform2HB.Y)
                            {
                                Character.Position.X += platform2Speed;
                            }
                            else if (CharacterHB.X > Platform1HB.X && CharacterHB.X < Platform1HB.X + Platform1HB.Width)
                            {
                                Character.Position.X += platform1Speed;
                            }
                        }

                    }
                }
                if (l2 == true)
                {
                    Platform1HB = new Rectangle((int)Platform1.Position.X - 20, (int)Platform1.Position.Y, Platform1.Image.Width, Platform1.Image.Height);
                    SpikesHB = new Rectangle((int)Spikes.Position.X, (int)Spikes.Position.Y, Spikes.Image.Width, Spikes.Image.Height);
                    WallHB = new Rectangle((int)Wall.Position.X - 20, (int)Wall.Position.Y, Wall.Image.Width, Wall.Image.Height);
                    Floor_RHB = new Rectangle((int)Floor_R.Position.X - 20, (int)Floor_R.Position.Y, Floor_R.Image.Width, Floor_R.Image.Height);
                    LavaHB = new Rectangle((int)Lava.Position.X, (int)Lava.Position.Y, Lava.Image.Width, Lava.Image.Height);
                    hitBoxes.Add(Platform1HB);
                    hitBoxes.Add(SpikesHB);
                    hitBoxes.Add(WallHB);
                    hitBoxes.Add(Floor_RHB);
                    hitBoxes.Add(LavaHB);

                    Platform1.Position.Y += platform1Speed;
                    if (Platform1.Position.Y + Platform1.Image.Height > GraphicsDevice.Viewport.Height - 5)
                    {
                        platform1Speed = -Math.Abs(platform1Speed);
                    }
                    if (Platform1.Position.Y < 5)
                    {
                        platform1Speed = Math.Abs(platform1Speed);
                    }
                    if (CharacterHB.Intersects(Platform1HB))
                    {
                        if (onPlatform == true)
                        {
                            Character.Position.Y += platform1Speed;
                        }
                        else
                        {
                            Character.Position.Y += 6;
                        }
                    }
                }
                if (l3 == true)
                {
                    FloorHB = new Rectangle((int)Floor.Position.X - 40, (int)Floor.Position.Y, Floor.Image.Width + 40, Floor.Image.Height);
                    Platform1HB = new Rectangle((int)Platform1.Position.X - 20, (int)Platform1.Position.Y, Platform1.Image.Width, Platform1.Image.Height);
                    WallHB = new Rectangle((int)Wall.Position.X - 20, (int)Wall.Position.Y, Wall.Image.Width + 15, Wall.Image.Height);
                    LavaHB = new Rectangle((int)Lava.Position.X, (int)Lava.Position.Y, Lava.Image.Width, Lava.Image.Height);
                    SidePlatHB = new Rectangle((int)SidePlat.Position.X - 30, (int)SidePlat.Position.Y, SidePlat.Image.Width + 30, SidePlat.Image.Height);

                    hitBoxes.Add(FloorHB);
                    hitBoxes.Add(Platform1HB);
                    hitBoxes.Add(WallHB);
                    hitBoxes.Add(LavaHB);
                    hitBoxes.Add(SidePlatHB);

                    Platform1.Position.X += platform1Speed;
                    if (Platform1.Position.X + Platform1.Image.Width > GraphicsDevice.Viewport.Width + Platform1.Image.Width - 5)
                    {
                        platform1Speed = -Math.Abs(platform1Speed);
                    }
                    if (Platform1.Position.X < FloorHB.Right - 130)
                    {
                        platform1Speed = Math.Abs(platform1Speed);
                    }
                    if (idle > 3)
                    {
                        if (onPlatform == true && CharacterHB.Y > FloorHB.Y)
                        {
                            if (CharacterHB.X > Platform1HB.X && CharacterHB.X < Platform1HB.X + Platform1HB.Width)
                            {
                                Character.Position.X += platform1Speed;
                            }
                        }

                    }
                }
                if (l4 == true)
                {
                    LavaHB = new Rectangle((int)Lava.Position.X, (int)Lava.Position.Y + 2, Lava.Image.Width, Lava.Image.Height);
                    Platform2HB = new Rectangle((int)Platform2.Position.X - 20, (int)Platform2.Position.Y, Platform2.Image.Width, Platform2.Image.Height + 1);
                    Fireball1HB = new Rectangle((int)Fireball1.Position.X + 5, (int)Fireball1.Position.Y, Fireball1.Image.Width - 5, Fireball1.Image.Height);
                    Fireball2HB = new Rectangle((int)Fireball2.Position.X + 5, (int)Fireball2.Position.Y, Fireball2.Image.Width - 5, Fireball2.Image.Height);
                    Fireball3HB = new Rectangle((int)Fireball3.Position.X + 5, (int)Fireball3.Position.Y, Fireball3.Image.Width - 5, Fireball3.Image.Height);

                    hitBoxes.Add(LavaHB);
                    hitBoxes.Add(Platform2HB);
                    hitBoxes.Add(Fireball1HB);
                    hitBoxes.Add(Fireball2HB);
                    hitBoxes.Add(Fireball3HB);

                    elapsedBossTime += gameTime.ElapsedGameTime;

                    Platform1.Position.X += platform1Speed;
                    if (Platform1.Position.X + Platform1.Image.Width > GraphicsDevice.Viewport.Width - 5)
                    {
                        platform1Speed = -Math.Abs(platform1Speed);
                        e_d = SpriteEffects.None;
                    }
                    if (Platform1.Position.X < 5)
                    {
                        platform1Speed = Math.Abs(platform1Speed);
                        e_d = SpriteEffects.FlipHorizontally;
                    }
                    if (Platform2.Position.X < 1599)
                    {
                        Platform2.Position.X += platform2Speed;
                        if (Platform2.Position.X + Platform2.Image.Width > GraphicsDevice.Viewport.Width - 5)
                        {
                            platform2Speed = -Math.Abs(platform2Speed);
                        }
                        if (Platform2.Position.X < 5)
                        {
                            platform2Speed = Math.Abs(platform2Speed);
                        }

                        if (idle > 3)
                        {
                            if (onPlatform == true && CharacterHB.Y > Platform1.Position.Y)
                            {
                                if (CharacterHB.X > Platform2HB.X && CharacterHB.X < Platform2HB.X + Platform2HB.Width && CharacterHB.Y < Platform2HB.Y)
                                {
                                    Character.Position.X += platform2Speed;
                                }
                            }

                        }
                    }

                    if (elapsedBossTime < TimeSpan.FromMilliseconds(1000))
                    {
                        eIdle = true;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(2000))
                    {
                        eIdle = false;
                        enter = true;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(3500))
                    {
                        enter = false;
                        eIdle = true;
                        if (j == 1)
                        {
                            platform1Speed = 3;
                            j = 0;
                        }
                        h = 1;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(3600))
                    {
                        warning = true;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(6000))
                    {
                        elapsedWarningTime += gameTime.ElapsedGameTime;
                        if (elapsedWarningTime > updateWarningTime && h == 1)
                        {
                            warning = false;
                            h = 0;
                        }
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(6100))
                    {
                        elapsedWarningTime = TimeSpan.Zero;
                        lavaRise = true;
                        attack = true;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(15000))
                    {
                        if (Character.Position.X > Platform1.Position.X - 30 && Character.Position.X + Character.Image.Width < Platform1.Position.X + Platform1.Image.Width + 30)
                        {
                            warning = true;
                        }
                        else if (warning == true)
                        {
                            elapsedWarningTime += gameTime.ElapsedGameTime;
                            if (elapsedWarningTime > updateWarningTime)
                            {
                                warning = false;
                                lavaRise = true;
                                elapsedWarningTime = TimeSpan.Zero;
                            }
                        }

                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(15001))
                    {
                        Platform2.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - Platform2HB.Width / 2, GraphicsDevice.Viewport.Height - 110);
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(17500))
                    {
                        warning = true;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(18200))
                    {
                        Lava.Position.Y -= 1;
                        warning = false;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(18300))
                    {
                        Fireball1.Position = new Vector2(0 - Fireball1.Image.Width, Platform2.Position.Y - Fireball1HB.Height);
                        Fireball2.Position = new Vector2(GraphicsDevice.Viewport.Width, Fireball1.Position.Y - Fireball2HB.Height);
                        Fireball3.Position = new Vector2(0 - Fireball3.Image.Width, Fireball2.Position.Y - Fireball3HB.Height);
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(22000))
                    {
                        Fireball1.Position.X += 5;
                        Fireball2.Position.X -= 5;
                        Fireball3.Position.X += 5;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(25000))
                    {
                        Platform2.Position.Y -= 1;
                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(30000))
                    {

                    }
                    else if (elapsedBossTime < TimeSpan.FromMilliseconds(31000))
                    {
                        iAttack = true;
                        platform1Speed = 0;
                        platform2Speed = 0;
                    }

                    if (lavaRise == true)
                    {
                        if (elapsedLavaTime < updateLavaTime)
                        {
                            attack = true;
                            elapsedLavaTime += gameTime.ElapsedGameTime;
                            Lava.Position.Y -= 1;
                        }
                        else
                        {
                            lavaRise = false;
                            elapsedLavaTime = TimeSpan.Zero;
                            Lava.Position.Y = GraphicsDevice.Viewport.Height + 2;
                            attack = false;
                        }
                    }
                }

                for (int i = 0; i < hitBoxes.Count; i++)
                {
                    if (CharacterHB.Intersects(hitBoxes[i]))
                    {
                        if (hitBoxes[i] == SpikesHB || hitBoxes[i] == LavaHB || hitBoxes[i] == Fireball1HB || hitBoxes[i] == Fireball2HB || hitBoxes[i] == Fireball3HB)
                        {
                            lives -= 1;
                            if (l2 == true)
                            {
                                Character.Position = new Vector2(0, GraphicsDevice.Viewport.Height - Character.Image.Height - 90);
                            }
                            else if (l3 == true)
                            {
                                Character.Position = new Vector2(0, 0);
                            }
                            else if (l4 == true)
                            {
                                Character.Position = new Vector2(0, 0);
                            }
                        }
                        if (lives == 0 || Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            l1 = true;
                            l2 = false;
                            l3 = false;
                            l4 = false;

                            Platform1.Position = new Vector2(5, GraphicsDevice.Viewport.Height - 92);
                            Platform2.Position = new Vector2(GraphicsDevice.Viewport.Width - Platform1.Image.Width - 5, GraphicsDevice.Viewport.Height - 178);
                            Character.Position = new Vector2(0, GraphicsDevice.Viewport.Height - 50);
                            Floor.Position = new Vector2(0, GraphicsDevice.Viewport.Height - 280);
                            Portal.Position = new Vector2(0, Floor.Position.Y - 55);
                            Fireball1.Position = new Vector2(1600, 0);
                            Fireball2.Position = new Vector2(1600, 0);
                            Fireball3.Position = new Vector2(1600, 0);

                            j = 1;
                            h = 0;
                            platform1Speed = 3;
                            platform2Speed = 3;

                            warning = false;
                            lavaRise = false;

                            elapsedBossTime = TimeSpan.Zero;
                            elapsedIdleTime = TimeSpan.Zero;
                            elapsedLavaTime = TimeSpan.Zero;
                            elapsedRunningTime = TimeSpan.Zero;
                            elapsedWarningTime = TimeSpan.Zero;
                            lives = 3;

                            break;
                        }
                        if (Character.Position.X > hitBoxes[i].X && Character.Position.X < hitBoxes[i].X + hitBoxes[i].Width)
                        {
                            if (Character.Position.Y < hitBoxes[i].Y - 20)
                            {
                                if (goUp == false)
                                {
                                    Character.Position.Y = hitBoxes[i].Y - Character.Image.Height + 1;
                                }
                                onPlatform = true;
                                break;
                            }
                            if (Character.Position.Y > hitBoxes[i].Y)
                            {
                                Character.Position.Y += 20;
                                break;
                            }
                        }
                    }
                    else
                    {
                        onPlatform = false;
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(Warning.Image, Warning.Position, Warning.Tint);
            if (warning == false)
            {
                spriteBatch.Draw(Background.Image, Background.Position, Background.Tint);
            }


            if (l1 == true)
            {
                spriteBatch.Draw(Platform1.Image, Platform1.Position, Platform1.Tint);
                spriteBatch.Draw(Platform2.Image, Platform2.Position, Platform2.Tint);
                spriteBatch.Draw(Floor.Image, Floor.Position, Floor.Tint);
                spriteBatch.Draw(Portal.Image, Portal.Position, Portal.Tint);
            }
            if (l2 == true)
            {
                spriteBatch.Draw(Platform1.Image, Platform1.Position, Platform1.Tint);
                spriteBatch.Draw(Spikes.Image, Spikes.Position, Spikes.Tint);
                spriteBatch.Draw(Wall.Image, Wall.Position, Wall.Tint);
                spriteBatch.Draw(Floor_R.Image, Floor_R.Position, Floor_R.Tint);
                spriteBatch.Draw(Portal.Image, Portal.Position, Portal.Tint);
                spriteBatch.Draw(Lava.Image, Lava.Position, Lava.Tint);
                spriteBatch.Draw(Portal.Image, Portal.Position, null, Portal.Tint, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);
            }
            if (l3 == true)
            {
                spriteBatch.Draw(Portal.Image, Portal.Position, null, Portal.Tint, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);
                spriteBatch.Draw(Floor.Image, Floor.Position, Floor.Tint);
                spriteBatch.Draw(Platform1.Image, Platform1.Position, Platform1.Tint);
                spriteBatch.Draw(Wall.Image, Wall.Position, Wall.Tint);
                spriteBatch.Draw(Lava.Image, Lava.Position, Lava.Tint);
                spriteBatch.Draw(Fade.Image, Fade.Position, Fade.Tint);
                spriteBatch.Draw(SidePlat.Image, SidePlat.Position, SidePlat.Tint);
            }
            if (l4 == true)
            {
                spriteBatch.Draw(Platform1.Image, Platform1.Position, Platform1.Tint);                
                spriteBatch.Draw(Fireball1.Image, Fireball1.Position, Fireball1.Tint);
                spriteBatch.Draw(Fireball2.Image, Fireball2.Position, Fireball2.Tint);
                spriteBatch.Draw(Fireball3.Image, Fireball3.Position, Fireball3.Tint);
                if (attack == true)
                {
                    spriteBatch.Draw(BossSprites, new Vector2(Platform1.Position.X + 40, Platform1.Position.Y - 50), e_attack[1].SourceRectangle, Color.White, 0f, e_attack[1].Origin, 1, e_d, 0f);
                }
                else if (enter == true)
                {
                    spriteBatch.Draw(BossSprites, new Vector2(Platform1.Position.X + 40, Platform1.Position.Y - 50), e_entranceFrame.SourceRectangle, Color.White, 0f, e_entranceFrame.Origin, 1, e_d, 0f);
                }
                else if (eIdle == true)
                {
                    spriteBatch.Draw(BossSprites, new Vector2(Platform1.Position.X + 40, Platform1.Position.Y - 50), e_idle.SourceRectangle, Color.White, 0f, e_idle.Origin, 1, e_d, 0f);
                }
                else if (eHurt == true)
                {
                    spriteBatch.Draw(BossSprites, new Vector2(Platform1.Position.X + 40, Platform1.Position.Y - 50), hurt.SourceRectangle, Color.White, 0f, hurt.Origin, 1, e_d, 0f);
                }
                spriteBatch.Draw(Lava.Image, Lava.Position, Lava.Tint);
                if (gameEnd == true)
                {
                    spriteBatch.Draw(end.Image, end.Position, end.Tint);
                }
                spriteBatch.Draw(Platform2.Image, Platform2.Position, Platform2.Tint);
            }

            if (jump == true)
            {
                spriteBatch.Draw(SpriteSheet, Character.Position, jumpFrame.SourceRectangle, Color.White, 0f, jumpFrame.Origin, Scale, direction, 0f);
            }
            else if (idle > 3)
            {
                spriteBatch.Draw(SpriteSheet, Character.Position, idleFrames[currentIdleFrame].SourceRectangle, Color.White, 0f, idleFrames[currentIdleFrame].Origin, Scale, direction, 0f);
            }
            else if (running > 0 && jump == false)
            {
                spriteBatch.Draw(SpriteSheet, Character.Position, runningFrames[currentRunningFrame].SourceRectangle, Color.White, 0f, runningFrames[currentRunningFrame].Origin, Vector2.One, direction, 0f);
            }
            else if (crouch == true)
            {
                spriteBatch.Draw(SpriteSheet, Character.Position, crouchFrame.SourceRectangle, Color.White, 0f, crouchFrame.Origin, Scale, direction, 0f);
            }
            else if (iAttack == true)
            {

            }

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}