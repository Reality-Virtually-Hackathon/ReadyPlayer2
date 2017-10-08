// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class DictationRecordButton : MonoBehaviour, IInputClickHandler, IDictationHandler
    {
        [SerializeField]
        [Range(0.1f, 5f)]
        [Tooltip("The time length in seconds before dictation recognizer session ends due to lack of audio input in case there was no audio heard in the current session.")]
        private float initialSilenceTimeout = 2f;

        [SerializeField]
        [Range(5f, 60f)]
        [Tooltip("The time length in seconds before dictation recognizer session ends due to lack of audio input.")]
        private float autoSilenceTimeout = 10f;

        [SerializeField]
        [Range(1, 60)]
        [Tooltip("Length in seconds for the manager to listen.")]
        private int recordingTime = 5;

        [SerializeField]
        private TextMesh speechToTextOutput;

        private bool isRecording;

        private MeshRenderer buttonRenderer;

        private void Awake()
        {
            buttonRenderer = this.GetComponent<MeshRenderer>();
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            ToggleRecording();
        }

        private void ToggleRecording()
        {
                StartCoroutine(DictationInputManager.StopRecording());
                Debug.Log("stop recording");
                speechToTextOutput.color = Color.white;
                MovieScript.main.scoreText.text ="%"+ (MovieScript.main.scoreQuote(speechToTextOutput.text)*100).ToString();
                StartCoroutine(DictationInputManager.StartRecording(initialSilenceTimeout, autoSilenceTimeout, recordingTime));
                MovieScript.main.nextQuote();
                speechToTextOutput.text = "...";
                speechToTextOutput.color = Color.green;
                Debug.LogWarning("start recording");
        }

        public void OnDictationHypothesis(DictationEventData eventData)
        {
            Debug.LogWarning("OnDictationHypothesis");
            speechToTextOutput.color = Color.green;
            speechToTextOutput.text = eventData.DictationResult;
            buttonRenderer.material.color = Color.green;
        }

        public void OnDictationResult(DictationEventData eventData)
        {   
            Debug.LogWarning("OndictationResult");
            speechToTextOutput.text = eventData.DictationResult;
            ToggleRecording();
        }

        public void OnDictationComplete(DictationEventData eventData)
        {
            Debug.LogWarning("onComplete");
            buttonRenderer.material.color = Color.white;
            speechToTextOutput.text = eventData.DictationResult;
            ToggleRecording();
        }

        public void OnDictationError(DictationEventData eventData)
        {
            Debug.LogWarning("onDictationError");
            isRecording = false;
            buttonRenderer.material.color = Color.red;
            speechToTextOutput.color = Color.red;
            speechToTextOutput.text = eventData.DictationResult;
            Debug.LogError(eventData.DictationResult);
            StartCoroutine(DictationInputManager.StopRecording());
        }
        
    }
}