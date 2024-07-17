using System;

public class Program
{
    public static void Main()
    {
        string num1 = "12345678901234567890";
        string num2 = "98765432109876543210";

        string result = Multiply(num1, num2);

        Console.WriteLine("Ket qua: " + result);
    }

    // Hàm nhân hai số nguyên lớn
    public static string Multiply(string num1, string num2)
    {
        // Xử lý trường hợp đặc biệt khi có một trong hai số là "0"
        if (num1 == "0" || num2 == "0")
            return "0";

        // Độ dài của hai số nguyên lớn
        int n = Math.Max(num1.Length, num2.Length);

        // Chuyển hai số nguyên lớn thành dạng mảng int để tính toán
        int[] arr1 = new int[n];
        int[] arr2 = new int[n];

        for (int i = 0; i < num1.Length; i++)
            arr1[i] = num1[num1.Length - 1 - i] - '0';

        for (int i = 0; i < num2.Length; i++)
            arr2[i] = num2[num2.Length - 1 - i] - '0';

        // Gọi hàm đệ quy để nhân hai số
        int[] resultArr = Multiply(arr1, arr2);

        // Chuyển kết quả từ mảng int sang dạng chuỗi
        string result = "";
        for (int i = resultArr.Length - 1; i >= 0; i--)
            result += resultArr[i];

        return result;
    }

    // Hàm đệ quy để nhân hai mảng số nguyên lớn
    private static int[] Multiply(int[] arr1, int[] arr2)
    {
        int n = arr1.Length;

        // Xử lý trường hợp cơ sở khi n = 1
        if (n == 1)
        {
            int[] result = new int[2];
            int product = arr1[0] * arr2[0];

            result[0] = product % 10; // lưu số hàng đơn vị
            result[1] = product / 10; // lưu số hàng chục

            return result;
        }

        // Chia mảng thành hai nửa
        int half = n / 2;
        int[] arr1Low = new int[half];
        int[] arr1High = new int[n - half];
        int[] arr2Low = new int[half];
        int[] arr2High = new int[n - half];

        Array.Copy(arr1, 0, arr1Low, 0, half);
        Array.Copy(arr1, half, arr1High, 0, n - half);
        Array.Copy(arr2, 0, arr2Low, 0, half);
        Array.Copy(arr2, half, arr2High, 0, n - half);

        // Đệ quy nhân hai nửa của mỗi mảng
        int[] z0 = Multiply(arr1Low, arr2Low);
        int[] z2 = Multiply(arr1High, arr2High);

        // Nhân (A0 + A1)(B0 + B1) - A0B0 - A1B1 để tính z1
        int[] A0plusA1 = Add(arr1Low, arr1High);
        int[] B0plusB1 = Add(arr2Low, arr2High);
        int[] z1 = Subtract(Subtract(Multiply(A0plusA1, B0plusB1), z0), z2);

        // Tạo mảng kết quả
        int[] result = new int[2 * n];
        Array.Copy(z0, 0, result, 0, z0.Length);
        Array.Copy(z1, 0, result, half, z1.Length);
        Array.Copy(z2, 0, result, 2 * half, z2.Length);

        // Loại bỏ các số 0 không cần thiết ở đầu mảng
        int i = result.Length - 1;
        while (i > 0 && result[i] == 0)
            i--;

        Array.Resize(ref result, i + 1);

        return result;
    }

    // Hàm cộng hai mảng số nguyên lớn
    private static int[] Add(int[] arr1, int[] arr2)
    {
        int n = Math.Max(arr1.Length, arr2.Length);
        int[] result = new int[n + 1]; // Tăng kích thước mảng để xử lý trường hợp cộng có thể tăng thêm 1 chữ số

        int carry = 0;
        for (int i = 0; i < n; i++)
        {
            int sum = (i < arr1.Length ? arr1[i] : 0) + (i < arr2.Length ? arr2[i] : 0) + carry;
            result[i] = sum % 10;
            carry = sum / 10;
        }

        result[n] = carry; // Lưu số dư nếu có

        return result;
    }

    // Hàm trừ hai mảng số nguyên lớn (arr1 - arr2), với giả định arr1 >= arr2
    private static int[] Subtract(int[] arr1, int[] arr2)
    {
        int n = arr1.Length;
        int[] result = new int[n];

        int borrow = 0;
        for (int i = 0; i < n; i++)
        {
            int diff = arr1[i] - (i < arr2.Length ? arr2[i] : 0) - borrow;
            if (diff < 0)
            {
                diff += 10;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }
            result[i] = diff;
        }

        return result;
    }
}
