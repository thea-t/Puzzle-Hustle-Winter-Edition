using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Hustle_Winter_Edition
{
    class SceneChanger
    {
        //storing the scenes in an enum in order to switch between them easily
        public enum Scenes
        {
            Menu,
            Map,
            Game,
            Result,
            Paused
        }

        //storing the scenes content here so that the MapLevel and the ScoreCalculator class can access it 
        public ScenesContent scenesContent;

        //storing the current level so that the MapLevel and the ScenesContent class can access it
        public int currentLevel;

        public Scenes currentScene;

        //function that changes the scene by assigning the current scene to the new scene
        public void ChangeScene(Scenes newScene)
        {
            currentScene = newScene;
        }
    }
}
