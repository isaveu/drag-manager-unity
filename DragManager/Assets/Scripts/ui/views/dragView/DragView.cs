using managers.dragManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ui.views.dragView {

    public class DragView : MonoBehaviour {

        public DragManager DragManager;

        public Button BtnStartDrag;
        public Button BtnStopDrag;
        public Button BtnReset;

        /**************************************************/

        #region Awake

        private void Awake() {
            AddEvents();
        }

        #endregion

        #region Start

        private void Start() {
            BtnStartDrag.gameObject.SetActive(true);
            BtnStopDrag.gameObject.SetActive(false);
        }

        #endregion


        #region Event: OnBtnStartDragClick

        private void OnBtnStartDragClick() {
            DragManager.SetState(DragManagerState.Ready);

            RefreshButtons(DragManagerState.Ready);
        }

        #endregion

        #region Event: OnBtnStopDragClick

        private void OnBtnStopDragClick() {
            DragManager.SetState(DragManagerState.Idle);

            RefreshButtons(DragManagerState.Idle);
        }

        #endregion

        #region Event: OnBtnResetClick

        private void OnBtnResetClick() {
            DragManager.SetState(DragManagerState.Idle);
            DragManager.ResetAnchors();

            RefreshButtons(DragManagerState.Idle);
        }

        #endregion


        #region RefreshButtons

        private void RefreshButtons(DragManagerState state) {
            switch (state) {
                case DragManagerState.Idle:
                    BtnStartDrag.gameObject.SetActive(true);
                    BtnStopDrag.gameObject.SetActive(false);
                    break;
                case DragManagerState.Ready:
                    BtnStartDrag.gameObject.SetActive(false);
                    BtnStopDrag.gameObject.SetActive(true);
                    break;
            }
        }

        #endregion


        #region Events: Add, Remove

        private void AddEvents() {
            BtnStartDrag.onClick.AddListener(new UnityAction(OnBtnStartDragClick));
            BtnStopDrag.onClick.AddListener(new UnityAction(OnBtnStopDragClick));
            BtnReset.onClick.AddListener(new UnityAction(OnBtnResetClick));
        }

        private void RemoveEvents() {
            BtnStartDrag.onClick.AddListener(OnBtnStartDragClick);
            BtnStopDrag.onClick.AddListener(OnBtnStopDragClick);
            BtnReset.onClick.AddListener(OnBtnResetClick);
        }

        #endregion

    }

}