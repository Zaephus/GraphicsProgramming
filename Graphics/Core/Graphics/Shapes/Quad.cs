using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace ZaephusEngine {

    public class Quad : Mesh {

        public new Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3(0.5f, -0.5f, 0.0f),
            new Vector3(-0.5f, 0.5f, 0.0f),
            new Vector3(0.5f, 0.5f, 0.0f)
        };

        public new Colour[] vertexColours = {
            Colour.red,
            Colour.green,
            Colour.blue,
            Colour.white
        };

        public new Vector2[] uvs = {
            new Vector2(0.0f, 0.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f)
        };

        public new uint[] triangles = {
            0, 1, 2,
            1, 3, 2
        };

        Texture texture0;
        Texture texture1;

        Matrix4x4 trans;

        public Quad() {
            base.vertices = this.vertices;
            base.vertexColours = this.vertexColours;
            base.uvs = this.uvs;

            base.triangles = this.triangles;
        }

        protected override void OnLoad() {
            base.OnLoad();

            texture0 = new Texture("Textures/container.png");
            texture1 = new Texture("Textures/awesomeFace.png");

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            Matrix4x4 rotation = Matrix4x4.RotateMatrix(Quaternion.FromEuler(0, 0, 90));
            Matrix4x4 scale = Matrix4x4.ScaleMatrix(Vector3.one * 0.5f);

            trans = rotation * scale;

            int location = GL.GetUniformLocation(shader.handle, "transform");
            unsafe {
                fixed(float* matrixPtr = &trans.m00) {
                    GL.UniformMatrix4(location, 1, true, matrixPtr);
                }
            }

        }

        float rot = 90;
        float s = 0.5f;

        protected override void OnRender() {
            base.OnRender();

            texture0.Use(TextureUnit.Texture0);
            texture1.Use(TextureUnit.Texture1);

            rot += 0.01f;
            s += 0.00005f;
            Matrix4x4 rotation = Matrix4x4.RotateMatrix(Quaternion.FromEuler(0, 0, rot));
            Matrix4x4 scale = Matrix4x4.ScaleMatrix(Vector3.one * s);

            trans = rotation * scale;

            int location = GL.GetUniformLocation(shader.handle, "transform");
            unsafe {
                fixed(float* matrixPtr = &trans.m00) {
                    GL.UniformMatrix4(location, 1, true, matrixPtr);
                }
            }
            
        }

        protected override void OnUnload() {
            base.OnUnload();
        }

    }

}