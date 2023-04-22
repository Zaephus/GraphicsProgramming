
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZaephusEngine {

    // TODO: Add IFormattable to Vector2Int class.
    public struct Vector2Int : IEquatable<Vector2Int> {

        public int x;
        public int y;

        public Vector2Int(float _x, float _y) : this((int)MathF.Round(_x), (int)MathF.Round(_y)) {}
        public Vector2Int(int _x, int _y) {
            x = _x;
            y = _y;
        }

        public float magnitude {
            get {
                return (float)MathF.Sqrt(x*x + y*y);
            }
        }

        public Vector2Int normalized {
            get {
                Vector2Int norm = new Vector2Int(x, y);
                norm.Normalize();
                return norm;
            }
        }

        public void Normalize() {
            this = this / (int)MathF.Round(magnitude);
        }

        public void Rotate(float _rad) {
            float a = x * MathF.Cos(_rad) - y * MathF.Sin(_rad);
            float b = x * MathF.Sin(_rad) + y * MathF.Cos(_rad);

            x = (int)MathF.Round(a);
            y = (int)MathF.Round(b);
        }
        
        public static float Distance(Vector2Int _lhs, Vector2Int _rhs) {
            return MathF.Sqrt(((_rhs.x - _lhs.x) * (_rhs.x - _lhs.x)) + ((_rhs.y - _lhs.y) * (_rhs.y - _lhs.y)));
        }
        
        public static float Dot(Vector2Int _lhs, Vector2Int _rhs) {
            return _lhs.x * _rhs.x + _lhs.y * _rhs.y;
        }

        public static float Cross(Vector2Int _lhs, Vector2Int _rhs) {
            return _lhs.y * _rhs.x - _lhs.x * _rhs.y;
        }

        public static float Angle(Vector2Int _from, Vector2Int _to) {
            float dot = Vector2Int.Dot(_from, _to);
            return MathF.Acos(dot / (_from.magnitude * _to.magnitude));
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        public override bool Equals(object _other) {
            if(!(_other is Vector2Int)) {
                return false;
            }
            return Equals((Vector2Int)_other);
        }

        public bool Equals(Vector2Int _other) {
            return x == _other.x && y == _other.y;
        }
        
        public override string ToString() {
            return $"x: {x}, y: {y}";
        }

        public static Vector2Int operator+(Vector2Int _lhs, Vector2Int _rhs) {
            return new Vector2Int(_lhs.x + _rhs.x, _lhs.y + _rhs.y);
        }

        public static Vector2Int operator-(Vector2Int _lhs, Vector2Int _rhs) {
            return new Vector2Int(_lhs.x - _rhs.x, _lhs.y - _rhs.y);
        }

        public static Vector2Int operator*(Vector2Int _lhs, Vector2Int _rhs) {
            return new Vector2Int(_lhs.x * _rhs.x, _lhs.y * _rhs.y);
        }

        public static Vector2Int operator*(Vector2Int _lhs, float _rhs) {
            return new Vector2Int(_lhs.x * _rhs, _lhs.y * _rhs);
        }

        public static Vector2Int operator*(float _rhs, Vector2Int _lhs) {
            return new Vector2Int(_lhs.x * _rhs, _lhs.y * _rhs);
        }

        public static Vector2Int operator/(Vector2Int _lhs, int _rhs) {
            return new Vector2Int(_lhs.x / _rhs, _lhs.y / _rhs);
        }

        public static bool operator==(Vector2Int _lhs, Vector2Int _rhs) {
            return _lhs.x == _rhs.x && _lhs.y == _rhs.y;
        }

        public static bool operator!=(Vector2Int _lhs, Vector2Int _rhs) {
            return !(_lhs == _rhs);
        }

        public static implicit operator Vector2Int(Vector3Int _v) {
            return new Vector2Int(_v.x, _v.y);
        }

        public static implicit operator Vector3Int(Vector2Int _v) {
            return new Vector3Int(_v.x, _v.y, 0);
        }

        public static Vector2Int RandomVector(int _minInclusive, int _maxInclusive) {
            return new Vector2Int(Random.Range(_minInclusive, _maxInclusive),
                                  Random.Range(_minInclusive, _maxInclusive));
        }

        public static Vector2Int zero { get { return new Vector2Int(0, 0); } }
        public static Vector2Int one { get { return new Vector2Int(1, 1); } }
        public static Vector2Int right { get { return new Vector2Int(1, 0); } }
        public static Vector2Int up { get { return new Vector2Int(0, 1); } }

    }

}