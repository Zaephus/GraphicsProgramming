

namespace ZaephusEngine {

    public class GameObject {

        public required Transform transform;

        private List<Component> components = new List<Component>();

        public GameObject() {}
        public GameObject(params Component[] _components) {
            components.AddRange(_components);
        }

        public virtual void Start() {
            foreach(Component c in components) {
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

        public Component GetComponent(Type _type) {
            foreach(Component c in components) {
                if(c.GetType() == _type) {
                    return c;
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