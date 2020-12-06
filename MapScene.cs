using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Hustle_Winter_Edition
{
    //how to draw textures from other classes https://community.monogame.net/t/drawing-from-a-new-class-is-not-working/11878/5
    class MapScene : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        //create the texture variables
        Texture2D backgroundImage;
        Texture2D playButtonImage;

        public MapScene(Game game) : base(game) 
        {
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load the textures
            backgroundImage = Game.Content.Load<Texture2D>("art/Background2");
        }

        public override void Draw(GameTime gameTime)
        {
            //draw the textures
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, new Vector2(0, 0));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
