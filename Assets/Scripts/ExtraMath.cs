using System.Collections.Generic;

using UnityEngine;

internal static class ExtraMath
{
    public static int OverfillCollection<T>(int index, IReadOnlyCollection<T> collection)
    {
        if (collection is null)
        {
            throw new System.ArgumentNullException(nameof(collection));
        }

        if (collection.Count == 1)
        {
            return 0;
        }

        var max = collection.Count - 1;

        if (index < 0)
        {
            var overflow = -index;
            var remainder = overflow % max;

            return max - remainder + 1;
        }

        if (index > max)
        {
            var overflow = index - max;
            var remainder = overflow % max;

            return remainder - 1;
        }

        return index;
    }
    public static float ClampAngle(float angle, float min = 0, float max = 360)
    {
        if (angle < -360)
            angle += 360;

        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}