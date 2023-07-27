
using System.Diagnostics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    // TODO: Add Render Initialization stack.
    // Once a renderer is created, it should be added to a initialization stack,
    // that get's resolved before every update cycle.
    // This way it is possible to create renderers at any time.

    public class Game {

        public static Action InitCall;
        public static Action RenderInitCall;
        public static Action LateInitCall;

        public static Action<float> UpdateCall;
        public static Action LateUpdateCall;

        public static Action EarlyRenderCall;
        public static Action RenderCall;
        public static Action LateRenderCall;

        public static Action ExitCall;

        protected Window window = new Window("ZaephusEngine");

        protected virtual void Start() {}
        private void Initialize() {
            Start();
            window.Initialize();
            InitCall?.Invoke();
            RenderInitCall?.Invoke();
            LateInitCall?.Invoke();
            PostInitialize();
        }
        protected virtual void PostInitialize() {}

        protected virtual void Update(float _dt) {}
        private void OnUpdate(float _dt) {
            Update(_dt);
            window.Update(_dt);
            UpdateCall?.Invoke(_dt);
            LateUpdateCall?.Invoke();
        }

        protected virtual void Render() {}
        private void OnRender() {
            Render();
            window.Render();
            EarlyRenderCall?.Invoke();
            RenderCall?.Invoke();
            LateRenderCall?.Invoke();
            window.SwapBuffers();
        }

        protected virtual void Exit() {}
        private void OnExit() {
            Exit();
            window.Exit();
            ExitCall?.Invoke();
        }
        
        // TODO: Add fixed time.
        // TODO: Add frame interpolation.
        public unsafe void Run() {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double lastTime = stopwatch.ElapsedMilliseconds * 0.001f;

            Initialize();
            while(GLFW.WindowShouldClose(window.WindowPtr) == false) {

                double currentTime = stopwatch.ElapsedMilliseconds * 0.001f;
                double dt = currentTime - lastTime;
                
                OnUpdate((float)dt);
                OnRender();

                lastTime = currentTime;

            }

            OnExit();

            stopwatch.Stop();

        }
        
    }

}