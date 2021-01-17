using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Hustle_Winter_Edition
{
    class ScoreCalculator
    {
        public int movesCount;
        public float passedTime;
        public int score;

        public void Update(GameTime gameTime)
        {
            // timer: https://stackoverflow.com/questions/13394892/how-to-create-a-timer-counter-in-c-sharp-xna

            passedTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

        }

        public void DisplayScore(SceneChanger sceneChanger)
        {
            sceneChanger.ChangeScene(SceneChanger.Scenes.Result);

            score = 1000000000 / ((int)passedTime * movesCount);

            sceneChanger.scenesContent.UnlockNextLevel();

        }



    }

}
