using UnityEngine;

public static class Lect
{
    /// <param name="isLock">true일때 마우스를 화면 안에 가둡니다.</param>
    /// <param name="isVisible">true일때 마우스를 표시합니다.</param>
    public static void SetCursor(bool isLock, bool isVisible)
    {
        if (isLock) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;

        if (isVisible) Cursor.visible = true;
        else Cursor.visible = false;
    }

    public static Vector3 RandomPosOnCenter(Vector3 area, float minRadius, float maxRadius)
    {
        Vector2 rand = Random.insideUnitCircle * Random.Range(minRadius, maxRadius);
        Vector3 randomPos = new Vector3(area.x + rand.x, area.y, area.z + rand.y);

        return randomPos;
    }

    public static void Rotate(Transform you, Vector3 target, float rotateSpeed)
    {
        Vector3 dir = (target - you.position).normalized;
        if (dir == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(dir);
        you.rotation = Quaternion.Slerp(you.rotation, rot,
            rotateSpeed * Time.fixedDeltaTime);
    }

    // Boss Need

    /// <summary>
    /// 일정 거리보다 멀다면 true를, 가깝다면 false를 반환함
    /// </summary>
    public static bool DistanceCheck(Transform _transform, Transform _something, float _distanceBetweenSomething)
    {
        float currentDistance = Distance(_transform, _something);
        if (currentDistance > _distanceBetweenSomething)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 자주 쓰는 magnitude 쓰기 편하게
    /// </summary>
    public static float Distance(Vector3 _position1, Vector3 _position2)
    {
        return (_position1 - _position2).magnitude;
    }
    public static float Distance(Transform _transform1, Transform _transform2)
    {
        return (_transform1.position - _transform2.position).magnitude;
    }
}
