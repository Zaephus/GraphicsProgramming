

namespace ZaephusEngine {

    public static class Math {

        // TODO: Add epsilon constant.
        // TODO: Replace MathF functions.

        public static float Deg2Rad {
            get {
                return MathF.PI / 180f;
            }
        }

        public static float Rad2Deg {
            get {
                return 180f / MathF.PI;
            }
        }

    }

}