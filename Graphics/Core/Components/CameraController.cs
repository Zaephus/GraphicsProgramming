
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class CameraController : Component {

        public float rotateSpeed = 30.0f;
        public float moveSpeed = 7.0f;

        private Vector2 mouseDelta;
        private Vector2 keyDelta;

        private Vector2 camRot;
        
        public override void Start() {
            Window.HandleInput += HandleInput;
        }

        public override void Update(float _dt) {
            
            gameObject.transform.position -= gameObject.transform.forward * keyDelta.y * _dt * moveSpeed;
            gameObject.transform.position += gameObject.transform.right * keyDelta.x * _dt * moveSpeed;

            camRot += new Vector2(-mouseDelta.y * _dt * rotateSpeed, -mouseDelta.x * _dt * rotateSpeed);

            gameObject.transform.rotation = Quaternion.FromEuler(camRot.x, camRot.y, 0.0f);

        }

        private void HandleInput(KeyboardState _keyState, MouseState _mouseState) {
            
            Vector2 mouse = _mouseState.Position - _mouseState.PreviousPosition;
            if(mouse.magnitude >= Math.epsilon) {
                mouseDelta = mouse;
            }
            else {
                mouseDelta = Vector2.zero;
            }

            if(_keyState.IsKeyDown(Keys.D)) {
                keyDelta.x = 1;
            }
            else if(_keyState.IsKeyDown(Keys.A)) {
                keyDelta.x = -1;
            }
            else {
                keyDelta.x = 0;
            }

            if(_keyState.IsKeyDown(Keys.W)) {
                keyDelta.y = 1;
            }
            else if(_keyState.IsKeyDown(Keys.S)) {
                keyDelta.y = -1;
            }
            else {
                keyDelta.y = 0;
            }

        }

    }

}