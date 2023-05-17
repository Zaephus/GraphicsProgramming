using OpenTK.Mathematics;

namespace ZaephusEngine {

    // TODO: Add IFormattable to Matrix3x3 class.
    public struct Matrix3x3 : IEquatable<Matrix3x3> {

        // The values in this class are saved as Row Major, so Matrices need to be transposed when applied to the shader.
        public float m00, m01, m02;
        public float m10, m11, m12;
        public float m20, m21, m22;

        public float this[int _row, int _column] {
            get {
                return this[_column + _row * 3];
            }

            set {
                this[_column + _row * 3] = value;
            }
        }

        public float this[int _index] {
            get {
                switch(_index) {
                    case 0:     return m00;
                    case 1:     return m01;
                    case 2:     return m02;

                    case 3:     return m10;
                    case 4:     return m11;
                    case 5:     return m12;

                    case 6:     return m20;
                    case 7:     return m21;
                    case 8:     return m22;

                    default:    throw new IndexOutOfRangeException("Invalid index.");
                }
            }
            set {
                switch(_index) {
                    case 0:     m00 = value;   break;
                    case 1:     m01 = value;   break;
                    case 2:     m02 = value;   break;

                    case 3:     m10 = value;   break;
                    case 4:     m11 = value;   break;
                    case 5:     m12 = value;   break;

                    case 6:     m20 = value;   break;
                    case 7:     m21 = value;   break;
                    case 8:     m22 = value;   break;

                    default:    throw new IndexOutOfRangeException("Invalid index.");
                }
            }
        }

        public Matrix3x3() {
            this = Matrix3x3.identity;
        }

        public Matrix3x3(float[,] _matrix) {
            m00 = _matrix[0, 0];
            m10 = _matrix[1, 0];
            m20 = _matrix[2, 0];
 
            m01 = _matrix[0, 1];
            m11 = _matrix[1, 1];
            m21 = _matrix[2, 1];

            m02 = _matrix[0, 2];
            m12 = _matrix[1, 2];
            m22 = _matrix[2, 2];
        }

        public Matrix3x3(
            float _m00, float _m01, float _m02,
            float _m10, float _m11, float _m12,
            float _m20, float _m21, float _m22
        ) {
            m00 = _m00; m01 = _m01; m02 = _m02;
            m10 = _m10; m11 = _m11; m12 = _m12;
            m20 = _m20; m21 = _m21; m22 = _m22;
        }

        public Matrix3x3(Vector3 _row0, Vector3 _row1, Vector3 _row2) {
            m00 = _row0.x;
            m01 = _row0.y;
            m02 = _row0.z;

            m10 = _row1.x;
            m11 = _row1.y;
            m12 = _row1.z;

            m20 = _row2.x;
            m21 = _row2.y;
            m22 = _row2.z;
        }

        public Vector3 GetRow(int _index) {
            switch(_index) {
                case 0:
                    return new Vector3(m00, m01, m02);
                case 1:
                    return new Vector3(m10, m11, m12);
                case 2:
                    return new Vector3(m20, m21, m22);
                default:
                    throw new IndexOutOfRangeException("Invalid row index.");
            }
        }

        public Vector3 GetColumn(int _index) {
            switch(_index) {
                case 0:
                    return new Vector3(m00, m10, m20);
                case 1:
                    return new Vector3(m01, m11, m21);
                case 2:
                    return new Vector3(m02, m12, m22);
                default:
                    throw new IndexOutOfRangeException("Invalid column index.");
            }
        }

        public float[] FloatArray {
            get {
                List<float> entries = new List<float>();
                for(int i = 0; i < 9; i++) {
                    entries.Add(this[i]);
                }
                return entries.ToArray();
            }
            set {
                this = new Matrix3x3(
                    value[0], value[1], value[2],
                    value[3], value[4], value[5],
                    value[6], value[7], value[8]
                );
            }
        }

        // TODO: Add inverse matrix.
        public Matrix3x3 inverse {
            get {
                Matrix3x3 i = this;
                i.Invert();
                return i;
            }
        }

        public void Invert() {

            
            
        }

        public Matrix3x3 transposed {
            get {
                Matrix3x3 t = this;
                t.Transpose();
                return t;
            }
        }

        public void Transpose() {
            this = new Matrix3x3(
                m00, m10, m20,
                m01, m11, m21,
                m02, m12, m22
            );
        }

        public float determinant {
            get {
                float d1 = m11*m22 - m12*m21;
                float d2 = m10*m22 - m12*m20;
                float d3 = m10*m21 - m11*m20;

                return m00*d1 - m01*d2 + m02*d3;
            }
        }

        public override int GetHashCode() {
            return GetColumn(0).GetHashCode() ^ (GetColumn(1).GetHashCode() << 2) ^ (GetColumn(2).GetHashCode() >> 2);
        }

        public override bool Equals(object _other) {
            if(!(_other is Matrix3x3)) {
                return false;
            }
            return Equals((Matrix3x3)_other);
        }

        public bool Equals(Matrix3x3 _other) {
            for(int x = 0; x < 3; x++) {
                for(int y = 0; y < 3; y++) {
                    if(_other[x, y] != this[x, y]) {
                        return false;
                    }
                }
            }
            return true;
        }

        public override string ToString() {
            return $"{m00}   {m01}   {m02}\n{m10}   {m11}   {m12}\n{m20}   {m21}   {m22}";
        }

        public static Matrix3x3 operator+(Matrix3x3 _lhs, Matrix3x3 _rhs) {
            return new Matrix3x3(
                _lhs.m00 + _rhs.m00, _lhs.m10 + _rhs.m10, _lhs.m20 + _rhs.m20,
                _lhs.m01 + _rhs.m01, _lhs.m11 + _rhs.m11, _lhs.m21 + _rhs.m21,
                _lhs.m02 + _rhs.m02, _lhs.m12 + _rhs.m12, _lhs.m22 + _rhs.m22
            );
        }

        public static Matrix3x3 operator-(Matrix3x3 _lhs, Matrix3x3 _rhs) {
            return new Matrix3x3(
                _lhs.m00 - _rhs.m00, _lhs.m10 - _rhs.m10, _lhs.m20 - _rhs.m20,
                _lhs.m01 - _rhs.m01, _lhs.m11 - _rhs.m11, _lhs.m21 - _rhs.m21,
                _lhs.m02 - _rhs.m02, _lhs.m12 - _rhs.m12, _lhs.m22 - _rhs.m22
            );
        }

        public static Matrix3x3 operator-(Matrix3x3 _m) {
            return -1 * _m;
        }

        public static Matrix3x3 operator*(Matrix3x3 _lhs, Matrix3x3 _rhs) {
            return new Matrix3x3(
                new Vector3(
                    (_lhs.m00 * _rhs.m00 + _lhs.m01 * _rhs.m10 + _lhs.m02 * _rhs.m20),
                    (_lhs.m00 * _rhs.m01 + _lhs.m01 * _rhs.m11 + _lhs.m02 * _rhs.m21),
                    (_lhs.m00 * _rhs.m02 + _lhs.m01 * _rhs.m12 + _lhs.m02 * _rhs.m22)
                ),

                new Vector3(
                    (_lhs.m10 * _rhs.m00 + _lhs.m11 * _rhs.m10 + _lhs.m12 * _rhs.m20),
                    (_lhs.m10 * _rhs.m01 + _lhs.m11 * _rhs.m11 + _lhs.m12 * _rhs.m21),
                    (_lhs.m10 * _rhs.m02 + _lhs.m11 * _rhs.m12 + _lhs.m12 * _rhs.m22)
                ),

                new Vector3(
                    (_lhs.m20 * _rhs.m00 + _lhs.m21 * _rhs.m10 + _lhs.m22 * _rhs.m20),
                    (_lhs.m20 * _rhs.m01 + _lhs.m21 * _rhs.m11 + _lhs.m22 * _rhs.m21),
                    (_lhs.m20 * _rhs.m02 + _lhs.m21 * _rhs.m12 + _lhs.m22 * _rhs.m22)
                )
            );
        }

        // TODO: Add Matrix4x4 and Matrix3x2 multiplication.

        public static Vector3 operator*(Matrix3x3 _m, Vector3 _v) {
            return new Vector3(
                (_m.m00 * _v.x + _m.m01 * _v.y + _m.m02 * _v.z),
                (_m.m10 * _v.x + _m.m11 * _v.y + _m.m12 * _v.z),
                (_m.m20 * _v.x + _m.m21 * _v.y + _m.m22 * _v.z)
            );
        }

        public static Matrix3x3 operator*(Matrix3x3 _m, float _s) {
            return new Matrix3x3(
                _s * _m.m00, _s * _m.m10, _s * _m.m20,
                _s * _m.m01, _s * _m.m11, _s * _m.m21,
                _s * _m.m02, _s * _m.m12, _s * _m.m22
            );
        }

        public static Matrix3x3 operator*(float _s, Matrix3x3 _m) {
            return new Matrix3x3(
                _s * _m.m00, _s * _m.m10, _s * _m.m20,
                _s * _m.m01, _s * _m.m11, _s * _m.m21,
                _s * _m.m02, _s * _m.m12, _s * _m.m22
            );
        }

        public static Matrix3x3 operator/(Matrix3x3 _m, float _s) {
            return new Matrix3x3(
                _m.m00 / _s, _m.m10 / _s, _m.m20 / _s,
                _m.m01 / _s, _m.m11 / _s, _m.m21 / _s,
                _m.m02 / _s, _m.m12 / _s, _m.m22 / _s
            );
        }

        public static bool operator==(Matrix3x3 _lhs, Matrix3x3 _rhs) {
            for(int x = 0; x < 3; x++) {
                for(int y = 0; y < 3; y++) {
                    if(_lhs[x, y] != _rhs[x, y]) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator!=(Matrix3x3 _lhs, Matrix3x3 _rhs) {
            return !(_lhs == _rhs);
        }

        public static implicit operator Matrix3(Matrix3x3 _m) {
            return new Matrix3(
                _m.m00, _m.m01, _m.m02,
                _m.m10, _m.m11, _m.m12,
                _m.m20, _m.m21, _m.m22
            );
        }

        public static implicit operator Matrix3x3(Matrix3 _m) {
            return new Matrix3x3(
                _m.Row0.X, _m.Row0.Y, _m.Row0.Z,
                _m.Row1.X, _m.Row1.Y, _m.Row1.Z,
                _m.Row2.X, _m.Row2.Y, _m.Row2.Z
            );
        }

        public static Matrix3x3 zero {
            get {
                return new Matrix3x3(
                    0, 0, 0,
                    0, 0, 0,
                    0, 0, 0
                );
            }
        }

        public static Matrix3x3 one {
            get {
                return new Matrix3x3(
                    1, 1, 1,
                    1, 1, 1,
                    1, 1, 1
                );
            }
        }

        public static Matrix3x3 identity {
            get {
                return new Matrix3x3(
                    1, 0, 0,
                    0, 1, 0,
                    0, 0, 1
                );
            }
        }

    }

}