using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 To2D(this Vector3 v3) => v3;

    public static Vector2 WithX(this Vector2 v2, float x) => new Vector2(x, v2.y);
    public static Vector2 WithY(this Vector2 v2, float y) => new Vector2(v2.x, y);
    public static Vector3 WithZ(this Vector2 v2, float z) => new Vector3(v2.x, v2.y, z);

    public static Vector2 WithXMul(this Vector2 v2, float x) => new Vector2(v2.x * x, v2.y);
    public static Vector2 WithYMul(this Vector2 v2, float y) => new Vector2(v2.x, v2.y * y);

    public static Vector2 WithXAdd(this Vector2 v2, float x) => new Vector2(v2.x + x, v2.y);
    public static Vector2 WithYAdd(this Vector2 v2, float y) => new Vector2(v2.x, v2.y + y);

    public static Vector3 WithX(this Vector3 v3, float x) => new Vector3(x, v3.y, v3.z);
    public static Vector3 WithY(this Vector3 v3, float y) => new Vector3(v3.x, y, v3.z);
    public static Vector3 WithZ(this Vector3 v3, float z) => new Vector3(v3.x, v3.y, z);

    public static Vector3 WithXMul(this Vector3 v3, float x) => new Vector3(v3.x * x, v3.y, v3.z);
    public static Vector3 WithYMul(this Vector3 v3, float y) => new Vector3(v3.x, v3.y * y, v3.z);
    public static Vector3 WithZMul(this Vector3 v3, float z) => new Vector3(v3.x, v3.y, v3.z * z);

    public static Vector3 WithXAdd(this Vector3 v3, float x) => new Vector3(v3.x + x, v3.y, v3.z);
    public static Vector3 WithYAdd(this Vector3 v3, float y) => new Vector3(v3.x, v3.y + y, v3.z);
    public static Vector3 WithZAdd(this Vector3 v3, float z) => new Vector3(v3.x, v3.y, v3.z + z);

    /// <summary>
    /// Multiplies two vectors component-wise.
    /// </summary>
    public static Vector2 ExScale(this Vector2 vector, Vector2 scale) => Vector2.Scale(vector, scale);
    /// <summary>
    /// Multiplies two vectors component-wise.
    /// </summary>
    public static Vector3 ExScale(this Vector3 vector, Vector3 scale) => Vector3.Scale(vector, scale);

    /// <summary>
    /// Divedes two vectors component-wise. If scale.x is 0 than scaledVector.x is 0 too
    /// </summary>
    public static Vector2 InverseScale(this Vector2 vector, Vector2 scale)
    {
        return new Vector2(
            scale.x == 0 ? 0 : vector.x / scale.x,
            scale.y == 0 ? 0 : vector.y / scale.y
        );
    }
    /// <summary>
    /// Divedes two vectors component-wise. If scale.x is 0 than scaledVector.x is 0 too
    /// </summary>
    public static Vector3 InverseScale(this Vector3 vector, Vector3 scale)
    {
        return new Vector3(
            scale.x == 0 ? 0 : vector.x / scale.x,
            scale.y == 0 ? 0 : vector.y / scale.y,
            scale.z == 0 ? 0 : vector.z / scale.z
        );
    }

    public static Vector2 FloorToInt(this Vector2 v2)
    {
        return new Vector2(
            Mathf.FloorToInt(v2.x),
            Mathf.FloorToInt(v2.y)
        );
    }

    public static Vector2 Sign(this Vector2 vector)
        => new Vector2(
            Mathf.Sign(vector.x),
            Mathf.Sign(vector.y)
        );

    public static Vector3 Sign(this Vector3 vector)
        => new Vector3(
            Mathf.Sign(vector.x),
            Mathf.Sign(vector.y),
            Mathf.Sign(vector.z)
        );

    public static Vector2 To2d(this Vector3 vector) => vector;
    public static Vector3 To3d(this Vector2 vector) => vector;

    public static Quaternion AsLookDirection2D(this Vector2 direction)
        => Quaternion.LookRotation(Vector3.forward, direction);

    public static Quaternion LookDirection(this Vector3 direction)
        => Quaternion.LookRotation(Vector3.forward, direction);
}