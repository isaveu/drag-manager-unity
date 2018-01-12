using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;

namespace managers.dragManager {

    public class DragManager : MonoBehaviour {

        #region Singleton Instance

        private static DragManager _instance = null;
        public static DragManager Instance { get { return _instance; } }

        #endregion

        public List<DragItem> DragItemList;

        private DragManagerState _state;

        private bool _loaded = false;

        /**************************************************/

        #region Awake

        private void Awake() {
            _instance = this;
        }

        #endregion

        #region Start

        private void Start() {
            SetState(DragManagerState.Idle);
        }

        #endregion

        #region Update

        private void Update() {

            if (!_loaded && (WorldAnchorManager.Instance.AnchorStore != null)) {
                foreach (DragItem dragItem in DragItemList) {
                    SaveAnchor(dragItem);
                }
                _loaded = true;
            }

            #region Toggle State

            if (Input.GetKeyDown(KeyCode.A)) {
                if (_state == DragManagerState.Idle) {
                    SetState(DragManagerState.Ready);
                } else if (_state == DragManagerState.Ready) {
                    SetState(DragManagerState.Idle);
                }
            }

            #endregion
            
        }

        #endregion


        #region Set: State

        public void SetState(DragManagerState state) {
            this._state = state;

            foreach (DragItem dragItem in DragItemList) {
                if (state == DragManagerState.Idle) {
                    dragItem.SetState(DragItemState.Idle);

                } else if (state == DragManagerState.Ready) {
                    dragItem.SetState(DragItemState.Ready);
                }
            }
        }

        #endregion


        #region Anchor: Save

        public void SaveAnchor(DragItem dragItem) {
            WorldAnchorManager.Instance.AttachAnchor(dragItem.gameObject, dragItem.gameObject.name);
        }

        #endregion

        #region Anchor: Remove, RemoveAll

        public void RemoveAnchor(DragItem dragItem) {
            WorldAnchorManager.Instance.RemoveAnchor(dragItem.gameObject);
        }

        public void RemoveAllAnchors() {
            WorldAnchorManager.Instance.RemoveAllAnchors();
        }

        #endregion

        #region Anchor: Reset

        public void ResetAnchors() {
            RemoveAllAnchors();

            foreach (DragItem dragItem in DragItemList) {
                dragItem.transform.localPosition = dragItem.InitialPosition;
                dragItem.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
        }

        #endregion

    }

}