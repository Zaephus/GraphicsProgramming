
using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class MeshRenderer : Component {

        public Material material;

        public Mesh mesh;

        protected int vertexBufferObject;
        protected int vertexArrayObject;
        protected int elementBufferObject;

        public MeshRenderer() : this(new Mesh()) {} 
        public MeshRenderer(Mesh _mesh) {
            mesh = _mesh;
            Window.OnLoadMeshes += OnLoad;
            Window.OnRenderMeshes += OnRender;
            Window.OnUnloadMeshes += OnUnload;
        }

        public override void Start() {}

        protected unsafe void OnLoad() {
            
            vertexBufferObject = GL.GenBuffer();
            vertexArrayObject = GL.GenVertexArray();
            elementBufferObject = GL.GenBuffer();

            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

            Vertex[] vertexArray = mesh.VertexArray;

            GL.BufferData(BufferTarget.ArrayBuffer, vertexArray.Length * sizeof(Vertex), vertexArray, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.triangles.Length * sizeof(uint), mesh.triangles, BufferUsageHint.StaticDraw);

            // Vertex Position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 0);
            GL.EnableVertexAttribArray(0);

            // Vertex Colour
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, sizeof(Vertex), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // UV's
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, sizeof(Vertex), 7 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            // Normals
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 9 * sizeof(float));
            GL.EnableVertexAttribArray(3);

            // Tangents
            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 12 * sizeof(float));
            GL.EnableVertexAttribArray(4);

            // Bi Tangents
            GL.VertexAttribPointer(5, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 15 * sizeof(float));
            GL.EnableVertexAttribArray(5);

            if(material == null) {
                material = new Material();
            }
            material.OnLoad();

        }

        public override void Update() {}

        protected void OnRender() {

            Matrix4x4 model = gameObject.transform.objectMatrix;
            material.shader.SetMatrix4x4("modelMatrix", ref model);

            Matrix4x4 view = Camera.activeCamera.ViewMatrix;
            material.shader.SetMatrix4x4("viewMatrix", ref view);

            Matrix4x4 projection = Camera.activeCamera.ProjectionMatrix;
            material.shader.SetMatrix4x4("projectionMatrix", ref projection);

            Matrix4x4 normalMatrix = model.inverse.transposed;
            material.shader.SetMatrix4x4("normalMatrix", ref normalMatrix);

            Vector3 camPos = new Vector3(Camera.activeCamera.transform.position.x, Camera.activeCamera.transform.position.y, -Camera.activeCamera.transform.position.z);
            material.shader.SetVector3("viewPosition", camPos);

            material.OnRender();

            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, mesh.triangles.Length, DrawElementsType.UnsignedInt, 0);

        }

        public override void Exit() {}

        protected void OnUnload() {
            material.OnUnload();
        }

    }

}