using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace ZaephusEngine {

    public class Texture {

        public int handle;

        public Texture(string _texturePath) {
            handle = GL.GenTexture();
            Use();

            StbImage.stbi_set_flip_vertically_on_load(1);

            ImageResult image = ImageResult.FromStream(File.OpenRead(_texturePath), ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        }

        public void Use() {
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }

    }

}