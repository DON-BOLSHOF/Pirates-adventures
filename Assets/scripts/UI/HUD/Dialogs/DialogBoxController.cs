﻿using System.Collections;
using PixelCrew.Model.Data;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.HUD.Dialogs
{
    class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space] [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")] [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private readonly static int IsOpen = Animator.StringToHash("IsOpen");

        private DialogData _data;
        private int _currentSentence;
        private AudioSource _sfxSource;
        private Coroutine _typingRoutine;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSFXSource();
        }

        public void ShowDialog(DialogData data)
        {
            _data = data;
            _currentSentence = 0;
            _text.text = string.Empty;

            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool(IsOpen, true);
        }

        private void OnStartDialogAnimation()
        {
            _typingRoutine = StartCoroutine(TypeDialogText());
        }

        public void OnSkip()
        {
            if (_typingRoutine == null)
                return;

            StopTypeAnimation();
            _text.text = _data.Sentences[_currentSentence];
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSentence++;

            var isDialogCompleted = _currentSentence > _data.Sentences.Length - 1;
            if (isDialogCompleted)
            {
                HideDialogBox();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        public void OnClose()
        {
            StopTypeAnimation();
            HideDialogBox();
        }

        private void HideDialogBox()
        {
            _animator.SetBool(IsOpen, false);
            _sfxSource.PlayOneShot(_close);
        }

        private void StopTypeAnimation()
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);

            //_typing = null;
        }

        private IEnumerator TypeDialogText()
        {
            _text.text = string.Empty;
            var sentences = _data.Sentences[_currentSentence];
            foreach (var letter in sentences)
            {
                _text.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingRoutine = null;
        }

        private  void OnCloseDialogAnimation()
        {
            _container.SetActive(false);
        }
    }
}
