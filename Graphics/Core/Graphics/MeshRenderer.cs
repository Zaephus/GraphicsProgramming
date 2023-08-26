
using System.Buffers;
using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class MeshRenderer : Component {

        // public Material material = null;

        // private Mesh m_mesh = null;
        // public Mesh mesh {
        //     get {
        //         return m_mesh;
        //     }
        //     set {
        //         m_mesh = value;
        //         if(isInitialized) {
        //             Start();
        //         }
        //     }
        // }

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

        protected int vertexArrayObject;
        protected int vertexBufferObject;
        protected int elementBufferObject;

        private bool isInitialized = false;

        public MeshRenderer() : this(null, null) {} 
        public MeshRenderer(Mesh _mesh) : this(new Mesh[] {_mesh}, null) {}
        public MeshRenderer((Mesh[], Material[]) _model) : this(_model.Item1, _model.Item2) {}
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

            for(int i = 0; i < meshes.Length; i++) {

                if(materials.Length > i && materials[i] != null) {

                    Matrix4x4 model = gameObject.transform.objectMatrix;
                    materials[i].SetMatrix4x4("modelMatrix", ref model);

                    Matrix4x4 view = Camera.activeCamera.ViewMatrix;
                    materials[i].SetMatrix4x4("viewMatrix", ref view);

                    Matrix4x4 projection = Camera.activeCamera.ProjectionMatrix;
                    materials[i].SetMatrix4x4("projectionMatrix", ref projection);

                    Matrix4x4 normalMatrix = model.inverse.transposed;
                    materials[i].SetMatrix4x4("normalMatrix", ref normalMatrix);

                    Vector3 camPos = Camera.activeCamera.transform.position;
                    materials[i].SetVector3("viewPosition", camPos);

                    materials[i].Render();

                }

                meshes[i]?.Render();
                
            }

        }

        public override void Exit() {
            material?.Dispose();
        }

    }

}