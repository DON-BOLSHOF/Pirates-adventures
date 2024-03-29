﻿using System;
using System.Collections;
using PixelCrew.Model.Data;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.HUD.Dialogs
{
    public class DialogBarController : MonoBehaviour
    {
        [SerializeField] private GameObject _container; 
        [SerializeField] private GameObject _bar;
        [SerializeField] private PortraitItemWidget _firstPortrait;
        [SerializeField] private PortraitItemWidget _secondPortrait;

        [Space] [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")] [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private DialogBarData[] _conversations;
        private DialogBarData _currentDialog => _conversations[_currentConversationIndex];

        private int _currentSentenceIndex;
        private int _currentConversationIndex;
        private string _currentSentence => _currentDialog.Sentences[_currentSentenceIndex];


        private PortraitItemWidget _currentPortrait;
        private PortraitItemWidget CurrentPortrait {
            get { return _currentPortrait; }
            set
            {
                if (value == null) { 
                    _currentPortrait = value;
                }

                if (CurrentPortrait == value)
                    return;

                if(CurrentPortrait != null)
                    PortraitConstriction(CurrentPortrait);

                _currentPortrait = value;
                CurrentPortrait.SetPortrait(_currentDialog.Portrait, _currentDialog.IsReversed);
            }
        }

        private Text _speakerName;
        private Text _text;

        private Animator _animator;
        private readonly static int IsOpen = Animator.StringToHash("Is-Open");

        private AudioSource _sfxSource;
        private Coroutine _typingRoutine;

        private Action onDialogEnd;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSFXSource();
            _animator = GetComponent<Animator>();

            var texts =_bar.GetComponentsInChildren(typeof(Text));
            _speakerName = (Text) texts[0];
            _text = (Text)texts[1];

            CurrentPortrait = new PortraitItemWidget();
        }

        public void ShowDialog(DialogBarData[] data, Action onEnd)
        {
            CurrentPortrait = null;
            _speakerName.text = string.Empty;
            _text.text = string.Empty;

            _sfxSource.PlayOneShot(_open);
            _container.SetActive(true);
            _animator.SetBool(IsOpen, true);
            InitDialog(data);

            onDialogEnd += onEnd;
        }

        public void InitDialog(DialogBarData[] data)
        {
            _conversations = data;
            _currentSentenceIndex = 0;
            _currentConversationIndex = 0;
        }

        private IEnumerator TypeDialogText()
        {
            _text.text = string.Empty;
            _speakerName.text = _currentDialog.SpeakerName;

            foreach (var letter in _currentSentence)
            {
                _text.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingRoutine = null;
        }

        public void OnStartTypingAnimation()
        {
            _typingRoutine = StartCoroutine(TypeDialogText());
        } 
        public void OnStartDialogAnimation()
        {
            CurrentPortrait = _currentDialog.IsReversed ? _secondPortrait : _firstPortrait;
            PortraitWiding(CurrentPortrait);
            _typingRoutine = StartCoroutine(TypeDialogText());
        }

        private void PortraitWiding(PortraitItemWidget portrait)
        {
            portrait.WidePortrait();
        }

        private void PortraitConstriction(PortraitItemWidget portrait)
        {
            portrait.ConstrictPortrait();
        }

        public void OnContinue()
        {
            _currentSentenceIndex++;
            var isSentenceCompleted = _currentSentenceIndex > _currentDialog.Sentences.Length-1;

            if (isSentenceCompleted)
            {
                _currentConversationIndex++;
                _currentSentenceIndex = 0;
                var isDialogCompleted = _currentConversationIndex > _conversations.Length - 1;

                if (isDialogCompleted)
                {
                    HideDialogBar();
                    return;
                }

                CurrentPortrait = _currentDialog.IsReversed ? _secondPortrait : _firstPortrait;
                PortraitWiding(CurrentPortrait);
            }

            OnStartTypingAnimation();
        }


        private void StopTypeAnimation()
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);

            _typingRoutine = null;
        }

        public void OnSkip()
        {
            if (_typingRoutine == null)
            {
                OnContinue();
                return;
            }

            StopTypeAnimation();
            _text.text = _currentSentence;
        }
        private void HideDialogBar()
        {
            _currentPortrait.ConstrictPortrait();
            Close(); 

            onDialogEnd?.Invoke();
            onDialogEnd = null;
        }
        public void CloseEarly()
        {
            if(_currentPortrait!= null)
                _currentPortrait.NormalizedScale();

            Close();
        }

        private void Close()
        {
            _animator.SetBool(IsOpen, false);
            _sfxSource.PlayOneShot(_close);
            SetBaseSprites();
        }

        public void SetBaseSprites()
        {
            if(_currentPortrait != null)
                _currentPortrait.SetBaseSprite();

            _firstPortrait.SetBaseSprite();
            _secondPortrait.SetBaseSprite();
        }
    }

    [Serializable]
    public class DialogBarData : DialogData
    {
        [SerializeField] private Sprite _portrait;
        [SerializeField] private string _speakerName;
        [SerializeField] private bool _isReversed;

        public Sprite Portrait => _portrait;
        public string SpeakerName => _speakerName;
        public bool IsReversed => _isReversed;
    }
}