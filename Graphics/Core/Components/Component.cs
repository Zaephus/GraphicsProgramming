

namespace ZaephusEngine {

    public abstract class Component {

        public GameObject gameObject;

        public void Initialize(GameObject _gameObject) {
            gameObject = _gameObject;
        }

        public virtual void Start() {}
        public virtual void Update() {}
        public virtual void Exit() {}
        
    }

}