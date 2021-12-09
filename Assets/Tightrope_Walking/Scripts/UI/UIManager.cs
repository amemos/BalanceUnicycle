using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using amemo.balanceUnicycle.structurals.events;

namespace amemo.balanceUnicycle.ui
{
    public class UIManager : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        GameObject TapToPlayPanel;
        [SerializeField]
        GameObject LevelCompletedPanel;
        [SerializeField]
        GameObject LevelFailedPanel;
        [SerializeField]
        RectTransform scaler;
        [SerializeField]
        RectTransform indicator;


        private void OnEnable()
        {
            EventManager.onLevelCompleted += OpenLevelCompletedPanel;
            EventManager.onLevelFailed += OpenLevelFailedPanel;
            EventManager.onIndicatorUpdate += OnIndicatorUpdate;
        }

        private void OnDisable()
        {
            EventManager.onLevelCompleted -= OpenLevelCompletedPanel;
            EventManager.onLevelFailed -= OpenLevelFailedPanel;
            EventManager.onIndicatorUpdate -= OnIndicatorUpdate;
        }

        private void OnIndicatorUpdate(float degree)
        {
            indicator.eulerAngles = Vector3.Lerp(indicator.eulerAngles, Vector3.forward * degree, Time.deltaTime * 4);
        }

        private void OpenLevelFailedPanel()
        {
            LevelFailedPanel.SetActive(true);
        }

        private void OpenLevelCompletedPanel()
        {
            LevelCompletedPanel.SetActive(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TapToPlayPanel.SetActive(false);
            scaler.gameObject.SetActive(true);
            EventManager.LevelStarted(0);
        }

    }
}

   
