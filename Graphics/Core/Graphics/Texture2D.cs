using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace ZaephusEngine {

    public class Texture2D {

        private Tuple<int, int, byte[]> imageData;

        private int handle;

        private TextureUnit textureUnit;

        public Texture2D(string _path) {
            StbImage.stbi_set_flip_vertically_on_load(1);

            using(Stream stream = File.OpenRead(_path)) {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                imageData = new Tuple<int, int, byte[]>(image.Width, image.Height, image.Data);
            }
        }

        public Texture2D() : this(Colour.white) {}
        public Texture2D(Colour _c) {
            byte[] bytes = {
                (byte)( _c.R * 255),
                (byte)( _c.G * 255),
                (byte)( _c.B * 255),
                (byte)( _c.A * 255)
            };
            imageData = new Tuple<int, int, byte[]>(1, 1, bytes);
        }

        public void Bind(TextureUnit _unit) {

            handle = GL.GenTexture();

            textureUnit = _unit;

            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imageData.Item1, imageData.Item2, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageData.Item3);

            // TODO: Add exposed parameters to change these settings.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        }

        public void Use() {
            GL.ActiveTexture(textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }

    }

}