

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

        public virtual void Update() {
            foreach(Component c in components) {
                c.Update();
            }
        }

        public virtual void Exit() {
            foreach(Component c in components) {
                c.Exit();
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

        public List<Component> GetComponents(Type _type) {
            List<Component> foundComponents = new List<Component>();
            foreach(Component c in components) {
                if(c.GetType() == _type) {
                    foundComponents.Add(c);
                }
            }
            return foundComponents;
        }

    }

}