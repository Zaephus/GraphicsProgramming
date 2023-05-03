using OpenTK.Mathematics;

namespace ZaephusEngine {

    // TODO: Add IFormattable to Matrix4x4 class.
    public struct Matrix4x4 : IEquatable<Matrix4x4> {

        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        public float this[int _row, int _column] {
            get {
                return this[_column + _row * 4];
            }

            set {
                this[_column + _row * 4] = value;
            }
        }

        public float this[int _index] {
            get {
                switch(_index) {
                    case 0:     return m00;
                    case 1:     return m01;
                    case 2:     return m02;
                    case 3:     return m03;
                    case 4:     return m10;
                    case 5:     return m11;
                    case 6:     return m12;
                    case 7:     return m13;
                    case 8:     return m20;
                    case 9:     return m21;
                    case 10:    return m22;
                    case 11:    return m23;
                    case 12:    return m30;
                    case 13:    return m31;
                    case 14:    return m32;
                    case 15:    return m33;
                    default:    throw new IndexOutOfRangeException("Invalid index.");
                }
            }
            set {
                switch(_index) {
                    case 0:     m00 = value;   break;
                    case 1:     m01 = value;   break;
                    case 2:     m02 = value;   break;
                    case 3:     m03 = value;   break;
                    case 4:     m10 = value;   break;
                    case 5:     m11 = value;   break;
                    case 6:     m12 = value;   break;
                    case 7:     m13 = value;   break;
                    case 8:     m20 = value;   break;
                    case 9:     m21 = value;   break;
                    case 10:    m22 = value;   break;
                    case 11:    m23 = value;   break;
                    case 12:    m30 = value;   break;
                    case 13:    m31 = value;   break;
                    case 14:    m32 = value;   break;
                    case 15:    m33 = value;   break;
                    default:    throw new IndexOutOfRangeException("Invalid index.");
                }
            }
        }

        public Matrix4x4(float[,] _matrix) {
            m00 = _matrix[0, 0];
            m10 = _matrix[1, 0];
            m20 = _matrix[2, 0];
            m30 = _matrix[3, 0];
 
            m01 = _matrix[0, 1];
            m11 = _matrix[1, 1];
            m21 = _matrix[2, 1];
            m31 = _matrix[3, 1];

            m02 = _matrix[0, 2];
            m12 = _matrix[1, 2];
            m22 = _matrix[2, 2];
            m32 = _matrix[3, 2];

            m03 = _matrix[0, 3];
            m13 = _matrix[1, 3];
            m23 = _matrix[2, 3];
            m33 = _matrix[3, 3];
        }

        public Matrix4x4(Vector4 _row0, Vector4 _row1, Vector4 _row2, Vector4 _row3) {
            m00 = _row0.x;
            m01 = _row0.y;
            m02 = _row0.z;
            m03 = _row0.w;

            m10 = _row1.x;
            m11 = _row1.y;
            m12 = _row1.z;
            m13 = _row1.w;

            m20 = _row2.x;
            m21 = _row2.y;
            m22 = _row2.z;
            m23 = _row2.w;

            m30 = _row3.x;
            m31 = _row3.y;
            m32 = _row3.z;
            m33 = _row3.w;
        }

        public Vector4 GetColumn(int _index) {
            switch(_index) {
                case 0:
                    return new Vector4(m00, m10, m20, m30);
                case 1:
                    return new Vector4(m01, m11, m21, m31);
                case 2:
                    return new Vector4(m02, m12, m22, m32);
                case 3:
                    return new Vector4(m03, m13, m23, m33);
                default:
                    throw new IndexOutOfRangeException("Invalid column index.");
            }
        }

        public Vector4 GetRow(int _index) {
            switch(_index) {
                case 0:
                    return new Vector4(m00, m01, m02, m03);
                case 1:
                    return new Vector4(m10, m11, m12, m13);
                case 2:
                    return new Vector4(m20, m21, m22, m23);
                case 3:
                    return new Vector4(m30, m31, m32, m33);
                default:
                    throw new IndexOutOfRangeException("Invalid row index.");
            }
        }

        public float[] FloatArray {
            get {
                List<float> entries = new List<float>();
                for(int i = 0; i < 16; i++) {
                    entries.Add(this[i]);
                }
                return entries.ToArray();
            }
            set {
                this = new Matrix4x4(
                    new Vector4(value[0], value[1], value[2], value[3]),
                    new Vector4(value[4], value[5], value[6], value[7]),
                    new Vector4(value[8], value[9], value[10], value[11]),
                    new Vector4(value[12], value[13], value[14], value[15])
                );
            }
        }

        // TODO: Add inverse matrix.

        public static Matrix4x4 ScaleMatrix(Vector3 _vec) {
            return new Matrix4x4(
                new Vector4(_vec.x, 0, 0, 0),
                new Vector4(0, _vec.y, 0, 0),
                new Vector4(0, 0, _vec.z, 0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Matrix4x4 TranslateMatrix(Vector3 _vec) {
            return new Matrix4x4(
                new Vector4(1, 0, 0, _vec.x),
                new Vector4(0, 1, 0, _vec.y),
                new Vector4(0, 0, 1, _vec.z),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Matrix4x4 RotateMatrix(Quaternion _q) {
            return new Matrix4x4(
                new Vector4(
                    (_q.w * _q.w + _q.x * _q.x - _q.y * _q.y - _q.z * _q.z),
                    (2* _q.x * _q.y - 2 * _q.w * _q.z),
                    (2* _q.x * _q.z - 2 * _q.w * _q.y),
                    0.0f
                ),
                new Vector4(
                    (2* _q.x * _q.y + 2 * _q.w * _q.z),
                    (_q.w * _q.w - _q.x * _q.x + _q.y * _q.y - _q.z * _q.z),
                    (2* _q.y * _q.z - 2 * _q.w * _q.x),
                    0.0f
                ),
                new Vector4(
                    (2* _q.x * _q.z - 2 * _q.w * _q.y),
                    (2* _q.y * _q.z + 2 * _q.w * _q.x),
                    (_q.w * _q.w - _q.x * _q.x - _q.y * _q.y + _q.z * _q.z),
                    0.0f
                ),
                new Vector4(0.0f, 0.0f, 0.0f, 1.0f)
            );
        }

        public override int GetHashCode() {
            return GetColumn(0).GetHashCode() ^ (GetColumn(1).GetHashCode() << 2) ^ (GetColumn(2).GetHashCode() >> 2) ^ (GetColumn(3).GetHashCode() >> 1);
        }

        public override bool Equals(object _other) {
            if(!(_other is Matrix4x4)) {
                return false;
            }
            return Equals((Matrix4x4)_other);
        }

        public bool Equals(Matrix4x4 _other) {
            for(int x = 0; x < 4; x++) {
                for(int y = 0; y < 4; y++) {
                    if(_other[x, y] != this[x, y]) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static Matrix4x4 operator+(Matrix4x4 _lhs, Matrix4x4 _rhs) {
            return new Matrix4x4(
                new Vector4(_lhs.m00 + _rhs.m00, _lhs.m10 + _rhs.m10, _lhs.m20 + _rhs.m20, _lhs.m30 + _rhs.m30),
                new Vector4(_lhs.m01 + _rhs.m01, _lhs.m11 + _rhs.m11, _lhs.m21 + _rhs.m21, _lhs.m31 + _rhs.m31),
                new Vector4(_lhs.m02 + _rhs.m02, _lhs.m12 + _rhs.m12, _lhs.m22 + _rhs.m22, _lhs.m32 + _rhs.m32),
                new Vector4(_lhs.m03 + _rhs.m03, _lhs.m13 + _rhs.m13, _lhs.m23 + _rhs.m23, _lhs.m33 + _rhs.m33)
            );
        }

        public static Matrix4x4 operator-(Matrix4x4 _lhs, Matrix4x4 _rhs) {
            return new Matrix4x4(
                new Vector4(_lhs.m00 - _rhs.m00, _lhs.m10 - _rhs.m10, _lhs.m20 - _rhs.m20, _lhs.m30 - _rhs.m30),
                new Vector4(_lhs.m01 - _rhs.m01, _lhs.m11 - _rhs.m11, _lhs.m21 - _rhs.m21, _lhs.m31 - _rhs.m31),
                new Vector4(_lhs.m02 - _rhs.m02, _lhs.m12 - _rhs.m12, _lhs.m22 - _rhs.m22, _lhs.m32 - _rhs.m32),
                new Vector4(_lhs.m03 - _rhs.m03, _lhs.m13 - _rhs.m13, _lhs.m23 - _rhs.m23, _lhs.m33 - _rhs.m33)
            );
        }

        public static Matrix4x4 operator*(Matrix4x4 _lhs, Matrix4x4 _rhs) {
            return new Matrix4x4(
                new Vector4(
                    (_lhs.m00 * _rhs.m00 + _lhs.m01 * _rhs.m10 + _lhs.m02 * _rhs.m20 + _lhs.m03 * _rhs.m30),
                    (_lhs.m00 * _rhs.m01 + _lhs.m01 * _rhs.m11 + _lhs.m02 * _rhs.m21 + _lhs.m03 * _rhs.m31),
                    (_lhs.m00 * _rhs.m02 + _lhs.m01 * _rhs.m12 + _lhs.m02 * _rhs.m22 + _lhs.m03 * _rhs.m32),
                    (_lhs.m00 * _rhs.m03 + _lhs.m01 * _rhs.m13 + _lhs.m02 * _rhs.m23 + _lhs.m03 * _rhs.m33)
                ),

                new Vector4(
                    (_lhs.m10 * _rhs.m00 + _lhs.m11 * _rhs.m10 + _lhs.m12 * _rhs.m20 + _lhs.m13 * _rhs.m30),
                    (_lhs.m10 * _rhs.m01 + _lhs.m11 * _rhs.m11 + _lhs.m12 * _rhs.m21 + _lhs.m13 * _rhs.m31),
                    (_lhs.m10 * _rhs.m02 + _lhs.m11 * _rhs.m12 + _lhs.m12 * _rhs.m22 + _lhs.m13 * _rhs.m32),
                    (_lhs.m10 * _rhs.m03 + _lhs.m11 * _rhs.m13 + _lhs.m12 * _rhs.m23 + _lhs.m13 * _rhs.m33)
                ),

                new Vector4(
                    (_lhs.m20 * _rhs.m00 + _lhs.m21 * _rhs.m10 + _lhs.m22 * _rhs.m20 + _lhs.m23 * _rhs.m30),
                    (_lhs.m20 * _rhs.m01 + _lhs.m21 * _rhs.m11 + _lhs.m22 * _rhs.m21 + _lhs.m23 * _rhs.m31),
                    (_lhs.m20 * _rhs.m02 + _lhs.m21 * _rhs.m12 + _lhs.m22 * _rhs.m22 + _lhs.m23 * _rhs.m32),
                    (_lhs.m20 * _rhs.m03 + _lhs.m21 * _rhs.m13 + _lhs.m22 * _rhs.m23 + _lhs.m23 * _rhs.m33)
                ),

                new Vector4(
                    (_lhs.m30 * _rhs.m00 + _lhs.m31 * _rhs.m10 + _lhs.m32 * _rhs.m20 + _lhs.m33 * _rhs.m30),
                    (_lhs.m30 * _rhs.m01 + _lhs.m31 * _rhs.m11 + _lhs.m32 * _rhs.m21 + _lhs.m33 * _rhs.m31),
                    (_lhs.m30 * _rhs.m02 + _lhs.m31 * _rhs.m12 + _lhs.m32 * _rhs.m22 + _lhs.m33 * _rhs.m32),
                    (_lhs.m30 * _rhs.m03 + _lhs.m31 * _rhs.m13 + _lhs.m32 * _rhs.m23 + _lhs.m33 * _rhs.m33)
                )
            );
        }

        // TODO: Add Matrix4x4 and Matrix4x3 multiplication.
        // TODO: Add Matrix4x4 and Matrix4x2 multiplication.

        public static Vector4 operator*(Matrix4x4 _m, Vector4 _v) {
            return new Vector4(
                (_m.m00 * _v.x + _m.m01 * _v.y + _m.m02 * _v.z + _m.m03 * _v.w),
                (_m.m10 * _v.x + _m.m11 * _v.y + _m.m12 * _v.z + _m.m13 * _v.w),
                (_m.m20 * _v.x + _m.m21 * _v.y + _m.m22 * _v.z + _m.m23 * _v.w),
                (_m.m30 * _v.x + _m.m31 * _v.y + _m.m32 * _v.z + _m.m33 * _v.w)
            );
        }

        public static Matrix4x4 operator*(Matrix4x4 _m, float _s) {
            return new Matrix4x4(
                new Vector4(_s * _m.m00, _s * _m.m10, _s * _m.m20, _s * _m.m30),
                new Vector4(_s * _m.m01, _s * _m.m11, _s * _m.m21, _s * _m.m31),
                new Vector4(_s * _m.m02, _s * _m.m12, _s * _m.m22, _s * _m.m32),
                new Vector4(_s * _m.m03, _s * _m.m13, _s * _m.m23, _s * _m.m33)
            );
        }

        public static Matrix4x4 operator*(float _s, Matrix4x4 _m) {
            return new Matrix4x4(
                new Vector4( _s * _m.m00, _s * _m.m10, _s * _m.m20, _s * _m.m30),
                new Vector4( _s * _m.m01, _s * _m.m11, _s * _m.m21, _s * _m.m31),
                new Vector4( _s * _m.m02, _s * _m.m12, _s * _m.m22, _s * _m.m32),
                new Vector4( _s * _m.m03, _s * _m.m13, _s * _m.m23, _s * _m.m33)
            );
        }

        public static Matrix4x4 operator/(Matrix4x4 _m, float _s) {
            return new Matrix4x4(
                new Vector4(_m.m00 / _s, _m.m10 / _s, _m.m20 / _s, _m.m30 / _s),
                new Vector4(_m.m01 / _s, _m.m11 / _s, _m.m21 / _s, _m.m31 / _s),
                new Vector4(_m.m02 / _s, _m.m12 / _s, _m.m22 / _s, _m.m32 / _s),
                new Vector4(_m.m03 / _s, _m.m13 / _s, _m.m23 / _s, _m.m33 / _s)
            );
        }

        public static bool operator==(Matrix4x4 _lhs, Matrix4x4 _rhs) {
            for(int x = 0; x < 4; x++) {
                for(int y = 0; y < 4; y++) {
                    if(_lhs[x, y] != _rhs[x, y]) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator!=(Matrix4x4 _lhs, Matrix4x4 _rhs) {
            return !(_lhs == _rhs);
        }

        public static implicit operator Matrix4(Matrix4x4 _m) {
            return new Matrix4(
                _m.m00, _m.m01, _m.m02, _m.m03,
                _m.m10, _m.m11, _m.m12, _m.m13,
                _m.m20, _m.m21, _m.m22, _m.m23,
                _m.m30, _m.m31, _m.m32, _m.m33
            );
        }

        public static implicit operator Matrix4x4(Matrix4 _m) {
            return new Matrix4x4( new float[,] {
                {_m.Row0.X, _m.Row0.Y, _m.Row0.Z, _m.Row0.W },
                {_m.Row1.X, _m.Row1.Y, _m.Row1.Z, _m.Row1.W },
                {_m.Row2.X, _m.Row2.Y, _m.Row2.Z, _m.Row2.W },
                {_m.Row3.X, _m.Row3.Y, _m.Row3.Z, _m.Row3.W }
            });
        }

        public static Matrix4x4 zero {
            get {
                return new Matrix4x4(
                    new Vector4(0, 0, 0, 0),
                    new Vector4(0, 0, 0, 0),
                    new Vector4(0, 0, 0, 0),
                    new Vector4(0, 0, 0, 0)
                );
            }
        }

        public static Matrix4x4 one {
            get {
                return new Matrix4x4(
                    new Vector4(1, 1, 1, 1),
                    new Vector4(1, 1, 1, 1),
                    new Vector4(1, 1, 1, 1),
                    new Vector4(1, 1, 1, 1)
                );
            }
        }

        public static Matrix4x4 identity {
            get {
                return new Matrix4x4(
                    new Vector4(1, 0, 0, 0),
                    new Vector4(0, 1, 0, 0),
                    new Vector4(0, 0, 1, 0),
                    new Vector4(0, 0, 0, 1)
                );
            }
        }

    }

}