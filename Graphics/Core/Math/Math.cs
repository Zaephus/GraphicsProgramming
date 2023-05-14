

namespace ZaephusEngine {

    public static class Math {

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

        public const float epsilon = Single.Epsilon;

    }

}