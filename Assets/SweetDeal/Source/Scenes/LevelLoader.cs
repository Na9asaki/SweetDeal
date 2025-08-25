using UnityEngine.SceneManagement;

namespace SweetDeal.Source.Scenes
{
    public static class LevelLoader
    {
        public static void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }

        public static void LoadGameplay()
        {
            SceneManager.LoadScene(1);
        }
    }
}