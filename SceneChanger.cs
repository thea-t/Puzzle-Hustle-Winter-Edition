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
        public ScenesContent scenesContent;

        //storing public variables that are being used by other classes
        public Scenes currentScene;
        public int currentLevel;
        
       

        //function that changes the scene
        public void ChangeScene(Scenes newScene)
        {
            currentScene = newScene;
        }
    }
}
