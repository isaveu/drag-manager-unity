using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace managers.dragManager {

    public class DragItem : MonoBehaviour {

        #region Components

        [Header("Components")]
        public HandDraggable HandDraggable;

        #endregion

        #region States

        [Header("Drag State Model")]
        public GameObject DragStateModel;
        public Material Mat_DragState_Ready;
        public Material Mat_DragState_Dragging;
        private MeshRenderer _dragStateMeshRenderer;

        #endregion

        #region Initial Values

        public Vector3 InitialPosition;

        #endregion

        private DragItemState _state;

        /**************************************************/

        #region Awake

        private void Awake() {
            _dragStateMeshRenderer = DragStateModel.gameObject.GetComponent<MeshRenderer>();
        }

        #endregion

        #region Start

        private void Start() {
            AddEvents();

            SetState(DragItemState.Idle);
        }

        #endregion


        #region Set: State

        public void SetState(DragItemState state) {
            this._state = state;

            switch (_state) {
                case DragItemState.Idle:
                    HandDraggable.IsDraggingEnabled = false;
                    DragStateModel.gameObject.SetActive(false);
                    break;

                case DragItemState.Ready:
                    HandDraggable.IsDraggingEnabled = true;
                    DragStateModel.gameObject.SetActive(true);
                    _dragStateMeshRenderer.material = Mat_DragState_Ready;
                    break;

                case DragItemState.Dragging:
                    DragStateModel.gameObject.SetActive(true);
                    _dragStateMeshRenderer.material = Mat_DragState_Dragging;
                    break;
            }
        }

        #endregion


        #region Event: OnStartedDragging

        private void OnStartedDragging() {
            SetState(DragItemState.Dragging);
            DragManager.Instance.RemoveAnchor(this);
        }

        #endregion

        #region Event: OnStoppedDragging

        private void OnStoppedDragging() {
            SetState(DragItemState.Ready);
            DragManager.Instance.SaveAnchor(this);
        }

        #endregion


        #region Events: Add, Remove

        private void AddEvents() {
            HandDraggable.StartedDragging += OnStartedDragging;
            HandDraggable.StoppedDragging += OnStoppedDragging;
        }

        private void RemoveEvents() {
            HandDraggable.StartedDragging -= OnStartedDragging;
            HandDraggable.StoppedDragging -= OnStoppedDragging;
        }

        #endregion

        #region Destroy

        private void OnDestroy() {
            RemoveEvents();
        }

        #endregion

    }

}