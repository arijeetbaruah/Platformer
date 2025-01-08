using System;
using FMODUnity;
using PG.Audio;
using PG.Service;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PG.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        [SerializeField] private EventReference onClickAudio;
        
        [SerializeField] private TextMeshProUGUI text;
        
        [SerializeField] private Color onHoverColor;
        [SerializeField] private Color onClickColor;

        private Color baseColor;
        
        private Button button;
        

        private void Awake()
        {
            if (!TryGetComponent<Button>(out button))
            {
                Debug.LogError("Button has not been attached to this game object.");
            }
            
            baseColor = text.color;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            ServiceManager.Get<AudioService>().PlayOnce(onClickAudio);
        }

        public void OnHoverStart()
        {
            text.color = onHoverColor;
        }

        public void ToNormal()
        {
            text.color = baseColor;
        }

        public void OnSelect()
        {
            text.color = onClickColor;
        }
    }
}
