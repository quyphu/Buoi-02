using System;

public class MergeSortExample
{
    public static void Main()
    {
        int[] arr = { 38, 27, 43, 3, 9, 82, 10 };

        Console.WriteLine("Mang ban dau:");
        PrintArray(arr);

        MergeSort(arr, 0, arr.Length - 1);

        Console.WriteLine("\nMang da sap xep:");
        PrintArray(arr);
    }

    // Hàm sắp xếp Merge Sort
    public static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            // Tính chỉ số phần tử giữa
            int mid = (left + right) / 2;

            // Đệ quy sắp xếp nửa bên trái và nửa bên phải của mảng
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);

            // Hợp nhất hai nửa đã sắp xếp
            Merge(arr, left, mid, right);
        }
    }

    // Hàm hợp nhất (merge) hai mảng đã sắp xếp
    public static void Merge(int[] arr, int left, int mid, int right)
    {
        // Tính kích thước của hai mảng con để hợp nhất
        int n1 = mid - left + 1;
        int n2 = right - mid;

        // Tạo mảng tạm thời để lưu trữ hai mảng con
        int[] leftArr = new int[n1];
        int[] rightArr = new int[n2];

        // Sao chép dữ liệu vào các mảng tạm thời
        for (int i = 0; i < n1; i++)
            leftArr[i] = arr[left + i];
        for (int j = 0; j < n2; j++)
            rightArr[j] = arr[mid + 1 + j];

        // Hợp nhất hai mảng con vào mảng chính arr

        // Chỉ số ban đầu của hai mảng con
        int indexLeft = 0, indexRight = 0;

        // Chỉ số ban đầu của mảng đã sắp xếp
        int indexMerge = left;

        while (indexLeft < n1 && indexRight < n2)
        {
            if (leftArr[indexLeft] <= rightArr[indexRight])
            {
                arr[indexMerge] = leftArr[indexLeft];
                indexLeft++;
            }
            else
            {
                arr[indexMerge] = rightArr[indexRight];
                indexRight++;
            }
            indexMerge++;
        }

        // Sao chép các phần tử còn lại của leftArr (nếu có)
        while (indexLeft < n1)
        {
            arr[indexMerge] = leftArr[indexLeft];
            indexLeft++;
            indexMerge++;
        }

        // Sao chép các phần tử còn lại của rightArr (nếu có)
        while (indexRight < n2)
        {
            arr[indexMerge] = rightArr[indexRight];
            indexRight++;
            indexMerge++;
        }
    }

    // Hàm in mảng
    public static void PrintArray(int[] arr)
    {
        foreach (var num in arr)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
        Console.ReadKey();
    }
}
