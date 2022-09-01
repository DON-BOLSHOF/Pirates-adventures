using UnityEngine;

namespace PixelCrew.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = GameObject.FindGameObjectWithTag("HUDCanvas");
            Object.Instantiate(window, canvas.transform);
        }
    }
}
