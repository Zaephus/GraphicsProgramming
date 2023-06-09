
using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Light : GameObject {

        // TODO: Add intensity modifier.

        public Colour colour;

        public Light(Colour _c) : base() {
            colour = _c;
        }

    }

}