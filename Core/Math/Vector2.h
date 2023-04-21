#pragma once

#include <cmath>

class Vector2 {

    public:
        float x;
        float y;

        Vector2(float _x, float _y);

        float GetMagnitude();
        void Normalize();
        void Rotate();

        Vector2 operator/(float _val);

};