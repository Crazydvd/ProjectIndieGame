using System.Collections;
using System.Collections.Generic;

public class Vec3 : Vec2
{
    public float z = 0;

    public new static Vec3 Zero
    {
        get
        {
            return new Vec3(0, 0, 0);
        }
    }

    public Vec3(float pX, float pY, float pZ) : base(pX, pY)
    {
        z = pZ;
    }

    public Vec3 SetXYZ(float pX, float pY, float pZ)
    {
        SetXY(pX, pY);
        z = pZ;
        return this;
    }

    public Vec3 SetXYZ(Vec3 pVec3)
    {
        SetXY(pVec3);
        z = pVec3.z;
        return this;    
    }

    public override string ToString()
    {
        return string.Format("({0}, {1}, {2})", x, y, z);
    }

    public Vec3 Add(Vec3 pVec3)
    {
        Add(pVec3);
        z += pVec3.z;
        return this;
    }

    public Vec3 Subtract(Vec3 pVec3)
    {
        Subtract(pVec3);
        z -= pVec3.z;
        return this;
    }

    public new Vec3 Scale(float pScalar)
    {
        base.Scale(pScalar);
        z *= pScalar;
        return this;
    }

    public float Dot(Vec3 pVec)
    {
        return base.Dot(pVec) + z * pVec.z;
    }
}
