using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BTB3D.Scripts.Game.Manager
{
    public class SplashManager : MonoBehaviour
    {
        [SerializeField] private Image splashImage;
        [SerializeField] private float fadeDuration;

        public Tween ShowSplash()
        {
            return splashImage.DOColor(new Color(1, 0, 0, 1), fadeDuration);
        }

        public Tween HideSplash()
        {
           return splashImage.DOColor(new Color(0, 0, 0, 0), fadeDuration);
        }

        public void LoadTitle()
        {
            SceneManager.LoadSceneAsync("Title");
        }

        public Sequence SplashSequence()
        {
            return DOTween.Sequence()
                .Append(ShowSplash())
                .Append(HideSplash())
                .AppendCallback(() =>
                {
                    LoadTitle();
                });
        }

        public void Start()
        {
            SplashSequence();
        }
    }
}
