
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZaephusEngine {

    public static class Random {

        private static System.Random rand = new System.Random();

        public static float Range(float _minInclusive, float _maxExclusive) {
            float temp = rand.NextSingle();
            temp = temp.Map(0, 1, _minInclusive, _maxExclusive);
            return temp;
        }

        public static int Range(int _minInclusive, int _maxInclusive) {
            return rand.Next(_minInclusive, _maxInclusive+1);
        }

    }
    
}