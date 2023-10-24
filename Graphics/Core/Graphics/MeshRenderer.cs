

namespace ZaephusEngine {

    public class MeshRenderer : Component {

        public Material[] materials;
        public Material material {
            get {
                if(materials != null) {
                    return materials[0];
                }
                else {
                    return null;
                }
            }
            set {
                if(materials == null) {
                    materials = new Material[] { value };
                }
                else {
                    materials[0] = value;
                }
            }
        }

        public Mesh[] meshes;
        public Mesh mesh {
            get {
                if(meshes != null) {
                    return meshes[0];
                }
                else {
                    return null;
                }
            }
            set {
                if(meshes == null) {
                    meshes = new Mesh[] { value };
                }
                else {
                    meshes[0] = value;
                }
            }
        }

        public RenderOrder renderOrder = RenderOrder.Normal;

        private bool isInitialized = false;

        private static readonly LitMaterial standardMaterial = new();

        public MeshRenderer() : this(null, null) {} 
        public MeshRenderer(Mesh _mesh) : this(new Mesh[] {_mesh}, null) {}
        public MeshRenderer(Model _model) : this(_model.meshes, _model.materials) {}
        public MeshRenderer(Mesh[] _meshes, Material[] _materials) {

            meshes = _meshes;
            materials = _materials;

            // TODO: Change render order to int value instead of enum.
            // TODO: Move render call to material class.
            switch(renderOrder) {
                case RenderOrder.Early:
                    Game.EarlyRenderCall += Render;
                    break;
                case RenderOrder.Normal:
                    Game.RenderCall += Render;
                    break;
                case RenderOrder.Late:
                    Game.LateRenderCall += Render;
                    break;
            }

        }

        public override unsafe void Start() {
            isInitialized = true;
        }

        protected void Render() {

            if(!isInitialized) {
                return;
            }

            if(materials == null || materials[0] == null) {
                RenderMaterial(standardMaterial);
            }

            for(int i = 0; i < meshes.Length; i++) {
                if(materials != null && materials.Length > i && materials[i] != null) {
                    RenderMaterial(materials[i]);
                }

                meshes[i]?.Render();
            }

        }

        public override void Exit() {
            material?.Dispose();
        }

        private void RenderMaterial(Material _material) {
            Matrix4x4 model = gameObject.transform.objectMatrix;
            _material.SetMatrix4x4("modelMatrix", ref model);

            Matrix4x4 view = Camera.activeCamera.ViewMatrix;
            _material.SetMatrix4x4("viewMatrix", ref view);

            Matrix4x4 projection = Camera.activeCamera.ProjectionMatrix;
            _material.SetMatrix4x4("projectionMatrix", ref projection);

            Matrix4x4 normalMatrix = model.inverse.transposed;
            _material.SetMatrix4x4("normalMatrix", ref normalMatrix);

            Vector3 camPos = Camera.activeCamera.transform.position;
            _material.SetVector3("viewPosition", camPos);

            _material.Render();
        }

    }

}