using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace ZaephusEngine {

    public class Texture {

        private int handle;

        private TextureUnit textureUnit;

        public Texture(string _path) : this(_path, TextureUnit.Texture0) {}
        public Texture(string _path, TextureUnit _unit) {

            handle = GL.GenTexture();

            textureUnit = _unit;

            GL.BindTexture(TextureTarget.Texture2D, handle);

            StbImage.stbi_set_flip_vertically_on_load(1);

            using(Stream stream = File.OpenRead(_path)) {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        }

        public Texture() : this(Colour.white, TextureUnit.Texture0) {}
        public Texture(Colour _c) : this(_c, TextureUnit.Texture0) {}
        public Texture(Colour _c, TextureUnit _unit) {

            handle = GL.GenTexture();

            textureUnit = _unit;

            GL.BindTexture(TextureTarget.Texture2D, handle);

            byte[] bytes = {
                (byte)( _c.R * 255),
                (byte)( _c.G * 255),
                (byte)( _c.B * 255),
                (byte)( _c.A * 255)
            };

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 1, 1, 0, PixelFormat.Rgba, PixelType.UnsignedByte, bytes);
        
        }

        public void Use() {
            GL.ActiveTexture(textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }

    }

}