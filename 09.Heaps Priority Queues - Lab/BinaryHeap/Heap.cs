using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int n = arr.Length;
        for (int i = n / 2; i >= 0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }

        for (int i = n - 1; i > 0; i--)
        {
            SwapElements(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }

    private static void HeapifyDown(T[] arr, int parentIndex, int length)
    {
        while (parentIndex < length / 2)
        {
            int child = 2 * parentIndex + 1;

            if (child + 1 < length && IsGreater(arr[child + 1], arr[child]))
            {
                child++;
            }

            if (!IsGreater(arr[child], arr[parentIndex]))
            {
                break;
            }

            SwapElements(arr, child, parentIndex);

            parentIndex = child;
        }
    }

    private static bool IsGreater(T p1, T p2)
    {
        return p1.CompareTo(p2) > 0;
    }

    private static void SwapElements(T[] arr, int childIndex, int parentIndex)
    {
        var temp = arr[childIndex];
        arr[childIndex] = arr[parentIndex];
        arr[parentIndex] = temp;
    }
}
