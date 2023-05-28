

namespace ZaephusEngine {

    public abstract class Component {

        public GameObject gameObject;

        public void Initialize(GameObject _gameObject) {
            gameObject = _gameObject;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Exit();
        
    }

}