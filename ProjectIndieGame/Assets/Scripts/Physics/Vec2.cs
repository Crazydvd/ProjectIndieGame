using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec2
{
    public static Vec2 zero { get { return new Vec2(0, 0); } }

    public float x = 0;
    public float y = 0;

    private static System.Random _random = new System.Random();

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }

    public Vec2 Add(Vec2 other)
    {
        x += other.x;
        y += other.y;
        return this;
    }

    public Vec2 Subtract(Vec2 other)
    {
        x -= other.x;
        y -= other.y;
        return this;
    }

    public Vec2 Scale(float pScalar)
    {
        x *= pScalar;
        y *= pScalar;
        return this;
    }

    public float Length()
    {
        //x² + y² = lVec2²      ²root(x² + y²) = lVec2
        return Mathf.Sqrt(x * x + y * y);
    }

    public Vec2 Normalize()
    {
        if (Length() != 0)
        {
            Scale(1 / Length());
        }
        return this;
    }

    public Vec2 Clone()
    {
        return new Vec2(x, y);
    }

    public void SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
    }

    public void SetXY(Vec2 pVec2)
    {
        x = pVec2.x;
        y = pVec2.y;
    }

    public static float Deg2Rad(float pDegrees)
    {
        return (Mathf.PI / 180) * pDegrees;
    }

    public static float Rad2Deg(float pRadians)
    {
        return (180 / Mathf.PI) * pRadians;
    }

    public static Vec2 GetUnitVectorDegrees(float pDegrees)
    {
        float radians = Deg2Rad(pDegrees);

        return new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public static Vec2 GetUnitVectorRadians(float pRadians)
    {
        return new Vec2(Mathf.Cos(pRadians), Mathf.Sin(pRadians));
    }

    public static Vec2 RandomUnitVector()
    {
        float direction = _random.Next(0, 360);
        return GetUnitVectorDegrees(direction);
    }

    public Vec2 SetAngleDegrees(float pDegrees)
    {
        float radians = Deg2Rad(pDegrees);
        SetXY(Mathf.Cos(radians) * Length(), Mathf.Sin(radians) * Length());
        return this;
    }

    public Vec2 SetAngleRadians(float pRadians)
    {
        SetXY(Mathf.Cos(pRadians) * Length(), Mathf.Sin(pRadians) * Length());
        return this;
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }

    public float GetAngleDegrees()
    {
        float radians = Mathf.Atan2(y, x);
        return Rad2Deg(radians);
    }

    public void RotateDegrees(float pDegrees)
    {
        float radians = Deg2Rad(pDegrees);
        SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians), x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }

    public void RotateRadians(float pRadians)
    {
        SetXY(x * Mathf.Cos(pRadians) - y * Mathf.Sin(pRadians), x * Mathf.Sin(pRadians) + y * Mathf.Cos(pRadians));
    }

    public void RotateAroundDegrees(float pPointX, float pPointY, float pDegrees)
    {
        //float radians = Deg2Rad(pDegrees);
        //SetXY(((x - pPointX) * Mathf.Cos(radians) - (y - pPointY) * Mathf.Sin(radians)) + pPointX, ((x - pPointX) * Mathf.Sin(radians) + (y - pPointY) * Mathf.Cos(radians)) + pPointY);

        x -= pPointX;
        y -= pPointY;
        RotateDegrees(pDegrees);
        x += pPointX;
        y += pPointY;
    }

    public void RotateAroundRadians(float pPointX, float pPointY, float pRadians)
    {
        //SetXY(((x - pPointX) * Mathf.Cos(pRadians) - (y - pPointY) * Mathf.Sin(pRadians)) + pPointX, ((x - pPointX) * Mathf.Sin(pRadians) + (y - pPointY) * Mathf.Cos(pRadians)) + pPointY);

        x -= pPointX;
        y -= pPointY;
        RotateRadians(pRadians);
        x += pPointX;
        y += pPointY;
    }

    public Vec2 Normal()
    {
        Vec2 vec2 = new Vec2(-y, x);
        return vec2.Normalize();
    }

    public float Dot(Vec2 pVec2)
    {
        return x * pVec2.x + y * pVec2.y;
    }

    public Vec2 Reflect(Vec2 pNormal, float pBounciness = 1)
    {
        Subtract(pNormal.Clone().Scale((1 + pBounciness) * Dot(pNormal)));
        return this;
    }

    public Vec2 ReflectJesse(Vec2 normal, float bounciness = 1)
    {
        x = x - (1 + bounciness) * (Dot(normal)) * normal.x;
        y = y - (1 + bounciness) * (Dot(normal)) * normal.y;
        return this;
    }
}
