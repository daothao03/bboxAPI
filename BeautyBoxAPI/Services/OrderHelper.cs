namespace BeautyBoxAPI.Services
{
    public class OrderHelper
    {

        /*
         Nhận được mã sản phẩm từ giỏ hàng: 9-9-7-9-6
         productID:Slsp
             9    : 3 (9 được thêm 3 lần vào giỏ hàng)
             7    : 1
             6    : 1
         */
        public static Dictionary<int, int> GetProductDictionary(string productIdentifiers)
        {
            var productDictionary = new Dictionary<int, int>();

            if (productIdentifiers.Length > 0)
            {
                string[] productIdArray = productIdentifiers.Split('-');
                foreach (var productId in productIdArray)
                {
                    try
                    {
                        int id = int.Parse(productId);

                        if (productDictionary.ContainsKey(id))
                        {
                            productDictionary[id] += 1;
                        }
                        else
                        {
                            productDictionary.Add(id, 1);
                        }
                    }
                    catch (Exception) { }
                }
            }


            return productDictionary;
        }

        //phí vận chuyển
        public static decimal ShippingFee { get; } = 30000;

        public static Dictionary<string, string> PaymentMethods { get; } = new()
        {
            { "Cash", "Thanh toán khi nhận hàng" }, //key -> server, value -> client
            { "Momo", "Thanh toán qua ví điện tử Momo" },
            { "Credit Card", "Thanh toán qua tài khoản ngân hàng" }
        };

        //trạng thái thanh toán
        public static List<string> PaymentStatuses { get; } = new()
        {
            "Chưa giải quyết", "Đã xác nhận", "Đã hủy"
        };

        public static List<string> OrderStatuses { get; } = new()
        {
            "Đã tạo đơn", "Đã xác nhận", "Đã hủy", "Đã vận chuyển", "Đã giao", "Đã hoàn trả"
        };
    }
}
