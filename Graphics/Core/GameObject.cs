
namespace ZaephusEngine {

    public class GameObject {

        public Transform transform = new Transform();

        private List<Component> components = new List<Component>();

        public GameObject() {
            components.Add(transform);
            Start();
        }
        public GameObject(params Component[] _components) {
            components.Add(transform);
            components.AddRange(_components);
            Start();
        }

        private void Start() {
            foreach(Component c in components) {
                c.Initialize(this);
                c.Start();
            }
        }

        public virtual void Update(float _dt) {
            foreach(Component c in components) {
                c.Update(_dt);
            }
        }

        public virtual void Exit() {
            foreach(Component c in components) {
                c.Exit();
            }
        }

        public Component AddComponent(Component _component) {
            components.Add(_component);
            _component.Initialize(this);
            _component.Start();
            return _component;
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