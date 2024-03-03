using System;
using BTB3D.Scripts.UI;
using Microsoft.Unity.VisualStudio.Editor;
using UniRx;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game.Manger
{
    public class TitleUIManager : MonoBehaviour
    {
        public enum State
        {
            Default,
            PlayMenu,
            LoadGame,
            Setting,
            Credit
        }
        [Header("Buttons")]
        public CustomTitleButton playButton;
        public CustomTitleButton settingButton;
        public CustomTitleButton creditButton;
        public CustomTitleButton exitButton;
        public CustomTitleButton continueButton;
        public CustomTitleButton newGameButton;
        public CustomTitleButton loadGameButton;
        public CustomTitleButton backGround;

        public Subject<State> stateSubject=new Subject<State>();

        [Header("Groups")]
        public GameObject titleMenuGroup;
        public GameObject playMenuGroup;

        [Header("Panels")]
        public GameObject loadGamePanel;
        public GameObject settingPanel;
        public GameObject creditPanel;

        public void Awake()
        {
            playButton.onClickDelegate += OnPlayButtonClick;
            settingButton.onClickDelegate += OnSettingButtonClick;
            creditButton.onClickDelegate += OnCreditButtonClick;
            exitButton.onClickDelegate += OnExitButtonClick;
            continueButton.onClickDelegate += OnContinueButtonClick;
            newGameButton.onClickDelegate += OnNewGameButtonClick;
            loadGameButton.onClickDelegate += OnLoadGameButtonClick;
            backGround.onClickDelegate += OnBackgroundClick;
        }

        public void Start()
        {
            stateSubject.Where(x => x == State.Default)
                .Subscribe(_ => titleMenuGroup.SetActive(true)).AddTo(gameObject);
            stateSubject.Where(x => x != State.Default && x != State.PlayMenu)
                .Subscribe(_ => titleMenuGroup.SetActive(false)).AddTo(gameObject);
            stateSubject.Where(x => x == State.PlayMenu)
                .Subscribe(_ => playMenuGroup.SetActive(true)).AddTo(gameObject);
            stateSubject.Where(x => x != State.PlayMenu)
                .Subscribe(_ => playMenuGroup.SetActive(false)).AddTo(gameObject);
            stateSubject.Where(x => x == State.LoadGame)
                .Subscribe(_ => loadGamePanel.SetActive(true));
            stateSubject.Where(x => x != State.LoadGame)
                .Subscribe(_ => loadGamePanel.SetActive(false));
            stateSubject.Where(x => x == State.Setting)
                .Subscribe(_ => settingPanel.SetActive(true)).AddTo(gameObject);
            stateSubject.Where(x => x != State.Setting)
                .Subscribe(_ => settingPanel.SetActive(false)).AddTo(gameObject);
            stateSubject.Where(x => x == State.Credit)
                .Subscribe(_ => creditPanel.SetActive(true)).AddTo(gameObject);
            stateSubject.Where(x => x != State.Credit)
                .Subscribe(_ => creditPanel.SetActive(false)).AddTo(gameObject);

        }

        #region Button

        public void OnBackgroundClick()
        {
            stateSubject.OnNext(State.Default);
        }

        public void OnPlayButtonClick()
        {
            stateSubject.OnNext(State.PlayMenu);
        }
        public void OnSettingButtonClick()
        {
            stateSubject.OnNext(State.Setting);
        }
        public void OnCreditButtonClick()
        {
            stateSubject.OnNext(State.Credit);
        }
        public void OnExitButtonClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }
        public void OnContinueButtonClick()
        {

        }
        public void OnNewGameButtonClick()
        {
            stateSubject.OnNext(State.LoadGame);
        }
        public void OnLoadGameButtonClick()
        {
            stateSubject.OnNext(State.LoadGame);
        }

        #endregion

    }
}
