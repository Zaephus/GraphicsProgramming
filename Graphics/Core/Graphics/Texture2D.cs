
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace ZaephusEngine {

    public enum TextureFilter {
        Linear,
        Point
    };

    public class Texture2D {

        private TextureFilter texFilter;
        public TextureFilter textureFilter {
            get {
                return texFilter;
            }
            set {
                texFilter = value;
                switch(texFilter) {
                    case TextureFilter.Linear:
                        minFilter = TextureMinFilter.Linear;
                        magFilter = TextureMagFilter.Linear;
                        break;
                    case TextureFilter.Point:
                        minFilter = TextureMinFilter.Nearest;
                        magFilter = TextureMagFilter.Nearest;
                        break;
                }
            }
        }
        private TextureMinFilter minFilter = TextureMinFilter.Linear;
        private TextureMagFilter magFilter = TextureMagFilter.Linear;

        public string name;

        public TextureWrapMode wrapMode = TextureWrapMode.Repeat;

        public bool generateMipmap = true;

        private Tuple<int, int, byte[]> imageData;

        private int handle;

        private TextureUnit textureUnit;

        public Texture2D(string _path) {
            // TODO: Expose Flip On Load variable.
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

        public void Bind(int _unit) {

            handle = GL.GenTexture();

            textureUnit = SetTextureUnit(_unit);

            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imageData.Item1, imageData.Item2, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageData.Item3);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapMode);

            if(generateMipmap) {
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }

        }

        public void Use() {
            GL.ActiveTexture(textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }

        private TextureUnit SetTextureUnit(int _unit) {
            return _unit switch {
                1  => TextureUnit.Texture1,
                2  => TextureUnit.Texture2,
                3  => TextureUnit.Texture3,
                4  => TextureUnit.Texture4,
                5  => TextureUnit.Texture5,
                6  => TextureUnit.Texture6,
                7  => TextureUnit.Texture7,
                8  => TextureUnit.Texture8,
                9  => TextureUnit.Texture9,
                10 => TextureUnit.Texture10,
                11 => TextureUnit.Texture11,
                12 => TextureUnit.Texture12,
                13 => TextureUnit.Texture13,
                14 => TextureUnit.Texture14,
                15 => TextureUnit.Texture15,
                16 => TextureUnit.Texture16,
                17 => TextureUnit.Texture17,
                18 => TextureUnit.Texture18,
                19 => TextureUnit.Texture19,
                20 => TextureUnit.Texture20,
                21 => TextureUnit.Texture21,
                22 => TextureUnit.Texture22,
                23 => TextureUnit.Texture23,
                24 => TextureUnit.Texture24,
                25 => TextureUnit.Texture25,
                26 => TextureUnit.Texture26,
                27 => TextureUnit.Texture27,
                28 => TextureUnit.Texture28,
                29 => TextureUnit.Texture29,
                30 => TextureUnit.Texture30,
                31 => TextureUnit.Texture31,
                _  => TextureUnit.Texture0
            };
        }

    }

}