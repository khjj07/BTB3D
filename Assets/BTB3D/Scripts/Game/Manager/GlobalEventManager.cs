using System;
using BTB3D.Scripts.Util;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BTB3D.Scripts.Game.Manager
{
    public class GlobalEventManager : Singleton<GlobalEventManager>
    {
        [SerializeField]
        private Image fadeImage;
        [SerializeField]
        private float fadeDuration;
        private bool _isFade=true;
        [SerializeField]
        private Canvas interactionBillboard;
        [SerializeField]
        private Transform interactionContents;


        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public Tween FadeIn()
        {
            return fadeImage.DOColor(new Color(0,0,0,0), fadeDuration);
        }

        public Tween FadeOut()
        {
            return fadeImage.DOColor(Color.black, fadeDuration);
        }

        public void ShowInteractionUI(Camera camera, Vector3 position)
        {
            interactionContents.transform.position = camera.WorldToScreenPoint(position);
            interactionBillboard.gameObject.SetActive(true);
        }
        public void HideInteractionUI()
        {
            interactionBillboard.gameObject.SetActive(false);
        }
    }
}
