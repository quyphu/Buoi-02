using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Large_Integers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Khởi tạo hai số nguyên lớn
            string num1 = "123456789012345678901234567890";
            string num2 = "987654321098765432109876543210";

            // Nhân hai số
            string result = MultiplyLargeNumbers(num1, num2);

            // Hiển thị kết quả
            Console.WriteLine("Kết quả của phép nhân là:");
            Console.WriteLine(result);

            // Để chương trình không tự động đóng, chờ người dùng nhấn một phím
            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey(); // Gọi phương thức ReadKey để chờ nhấn phím
        }

        static string MultiplyLargeNumbers(string num1, string num2)
        {
            // Xử lý trường hợp đặc biệt khi có một trong hai số là "0"
            if (num1 == "0" || num2 == "0")
                return "0";

            // Độ dài của hai số
            int n = Math.Max(num1.Length, num2.Length);

            // Chia đôi các số để nhân theo thuật toán Karatsuba
            string a = num1.Substring(0, num1.Length / 2);
            string b = num1.Substring(num1.Length / 2);
            string c = num2.Substring(0, num2.Length / 2);
            string d = num2.Substring(num2.Length / 2);

            // Đệ quy để tính các giá trị con
            string ac = MultiplyLargeNumbers(a, c);
            string bd = MultiplyLargeNumbers(b, d);

            // Tính (a+b)(c+d) - ac - bd
            string ab = AddLargeNumbers(a, b);
            string cd = AddLargeNumbers(c, d);
            string abcd = MultiplyLargeNumbers(ab, cd);
            string adbc = SubtractLargeNumbers(SubtractLargeNumbers(abcd, ac), bd);

            // Tạo chuỗi kết quả
            string result = AddLargeNumbers(AddLargeNumbers(ac + new string('0', n), adbc + new string('0', n / 2)), bd);
            return result.TrimStart('0');
        }

        // Hàm cộng hai số nguyên lớn
        static string AddLargeNumbers(string num1, string num2)
        {
            StringBuilder sb = new StringBuilder();
            int carry = 0;
            int i = num1.Length - 1, j = num2.Length - 1;

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int x = i >= 0 ? num1[i--] - '0' : 0;
                int y = j >= 0 ? num2[j--] - '0' : 0;
                int sum = x + y + carry;
                sb.Insert(0, sum % 10);
                carry = sum / 10;
            }

            return sb.ToString();
        }

        // Hàm trừ hai số nguyên lớn (num1 - num2), với giả định num1 >= num2
        static string SubtractLargeNumbers(string num1, string num2)
        {
            StringBuilder sb = new StringBuilder();
            int i = num1.Length - 1, j = num2.Length - 1;
            int borrow = 0;

            while (i >= 0)
            {
                int x = num1[i--] - '0';
                int y = j >= 0 ? num2[j--] - '0' : 0;
                int diff = x - y - borrow;

                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                sb.Insert(0, diff);
            }

            // Loại bỏ các số 0 không cần thiết ở đầu
            while (sb.Length > 1 && sb[0] == '0')
                sb.Remove(0, 1);

            return sb.ToString();
        }
    }
}
