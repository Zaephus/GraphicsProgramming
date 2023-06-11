
using ZaephusEngine;

/// <summary>
/// implements improved Perlin noise in 2D. 
/// Transcribed from http://www.siafoo.net/snippet/144?nolinenos#perlin2003
/// </summary>
public static class Noise2d {
    
    private static System.Random _random = new System.Random();
    private static int[] _permutation;

    private static Vector2[] _gradients;

    static Noise2d() {
        CalculatePermutation(out _permutation);
        CalculateGradients(out _gradients);
    }

    private static void CalculatePermutation(out int[] _p)  {
        _p = Enumerable.Range(0, 256).ToArray();

        /// shuffle the array
        for(var i = 0; i < _p.Length; i++) {
            var source = _random.Next(_p.Length);

            var t = _p[i];
            _p[i] = _p[source];
            _p[source] = t;
        }
    }

    /// <summary>
    /// generate a new permutation.
    /// </summary>
    public static void Reseed() {
        CalculatePermutation(out _permutation);
    }

    private static void CalculateGradients(out Vector2[] _grad) {
        _grad = new Vector2[256];

        for(var i = 0; i < _grad.Length; i++) {
            Vector2 gradient;

            do {
                gradient = new Vector2((float)(_random.NextDouble() * 2 - 1), (float)(_random.NextDouble() * 2 - 1));
            }
            while(gradient.squaredMagnitude >= 1);

            gradient.Normalize();

            _grad[i] = gradient;
        }

    }

    private static float Drop(float _t) {
        _t = MathF.Abs(_t);
        return 1f - _t * _t * _t * (_t * (_t * 6 - 15) + 10);
    }

    private static float Q(float _u, float _v) {
        return Drop(_u) * Drop(_v);
    }

    public static float Noise(float _x, float _y) {
        var cell = new Vector2((float)MathF.Floor(_x), (float)MathF.Floor(_y));

        var total = 0f;

        var corners = new[] {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(1, 1)
        };

        foreach(var n in corners) {
            var ij = cell + n;
            var uv = new Vector2(_x - ij.x, _y - ij.y);

            var index = _permutation[(int)ij.x % _permutation.Length];
            index = _permutation[(index + (int)ij.y) % _permutation.Length];

            var grad = _gradients[index % _gradients.Length];

            total += Q(uv.x, uv.y) * Vector2.Dot(grad, uv);
        }

        return MathF.Max(MathF.Min(total, 1f), -1f);
    }

}