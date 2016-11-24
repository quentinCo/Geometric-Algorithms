using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Projet_IMA
{
    struct V2
    {
        public BigInteger x;		// coordonnées du vecteur
        public BigInteger y;

        public bool Equals(V2 other)
        {
            bool eq = ((other.x == x) && (other.y == y));
            return eq;
        }

        public bool Near(V2 o)
        {
            BigInteger Delta = BigInteger.Abs(o.x - x) + BigInteger.Abs(o.y - y);
            if (Delta < 8) return true;
            else return false;
        }

        public BigInteger Norme2()
        {
            return x * x + y * y;
        }

  

        public V2(V2 t)
        {
            x = t.x;
            y = t.y;
        }


        public V2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static V2 operator +(V2 a, V2 b)
        {
            V2 t;
            t.x = a.x + b.x;
            t.y = a.y + b.y;
            return t;
        }

        public static V2 operator -(V2 a, V2 b)
        {
            V2 t;
            t.x = a.x - b.x;
            t.y = a.y - b.y;
            return t;
        }

        public static V2 operator -(V2 a)
        {
            V2 t;
            t.x = -a.x;
            t.y = -a.y;
            return t;
        }

        public static V2 operator *(int a, V2 b)
        {
            V2 t;
            t.x = b.x * a;
            t.y = b.y * a;
            return t;
        }

        public static V2 operator *(V2 b, int a)
        {
            V2 t;
            t.x = b.x * a;
            t.y = b.y * a;
            return t;
        }

        public static V2 operator /(V2 b, int a)
        {
            V2 t;
            t.x = b.x / a;
            t.y = b.y / a;
            return t;
        }

        
        //////////////////////////////////////////////////////////////////////////////
        
        public static BigInteger operator ^(V2 a, V2 b)  // produit vectoriel
        {
            BigInteger t = a.x * b.y - a.y * b.x;
            return t;
        }

        public static BigInteger operator *(V2 a, V2 b)  // produit scalaire
        {
            return a.x*b.x+a.y*b.y;
        }
      
    }
}
