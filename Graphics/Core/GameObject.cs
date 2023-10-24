
namespace ZaephusEngine {

    public class GameObject {

        public Transform transform = new Transform();

        private List<Component> components = new List<Component>();

        public GameObject() : this(new Component[0]) {}
        public GameObject(params Component[] _components) {
            components.Add(transform);
            components.AddRange(_components);

            Game.InitCall += Initialize;

            Game.UpdateCall += ComponentUpdate;
            Game.UpdateCall += Update;

            Game.RenderCall += Render;

            Game.ExitCall += ComponentExit;
            Game.ExitCall += Exit;
        }

        private void Initialize() {
            foreach(Component c in components) {
                c.Initialize(this);
                c.Start();
            }
            Start();
            Game.InitCall -= Initialize;
        }
        protected virtual void Start() {}

        private void ComponentUpdate(float _dt) {
            foreach(Component c in components) {
                c.Update(_dt);
            }
        }
        protected virtual void Update(float _dt) {}

        protected virtual void Render() {}

        private void ComponentExit() {
            foreach(Component c in components) {
                c.Exit();
            }
        }
        protected virtual void Exit() {}

        public Component AddComponent(Component _component) {
            components.Add(_component);
            _component.Initialize(this);
            _component.Start();
            return _component;
        }

        public void RemoveComponent(Component _component) {
            components.Remove(_component);
        }

        public void RemoveComponentOfType<T>() where T : Component {
            foreach(Component c in components) {
                if(c.GetType() == typeof(T)) {
                    components.Remove(c);
                    return;
                }
            }

        }

        public void RemoveAllComponentsOfType<T>() where T : Component {
            foreach(Component c in components) {
                if(c.GetType() == typeof(T)) {
                    components.Remove(c);
                }
            }
        }

        public T GetComponent<T>() where T : Component {
            foreach(Component c in components) {
                if(c.GetType() == typeof(T)) {
                    return (T)c;
                }
            }
            return null;
        }

        public List<Component> GetComponents<T>() where T : Component {
            List<Component> foundComponents = new List<Component>();
            foreach(Component c in components) {
                if(c.GetType() == typeof(T)) {
                    foundComponents.Add(c);
                }
            }
            return foundComponents;
        }

    }

}