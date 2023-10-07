using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class Contact
    {
        public int ID { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = "";

        [MaxLength(100)]
        public string LastName { get; set; } = "";

        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(100)]
        public string Phone { get; set; } = "";

        public string Subject { get; set; } = ""; //Mục đích của cuộc liên hệ tới cửa hàng. Ex: "Hỗ trợ kỹ thuật", "Phản hồi sản phẩm"

        public string Message { get; set; } = ""; //Thông tin chi tiết về cuộc liên hệ tới cửa hàng

        public DateTime CreatedAt { get; set; } = DateTime.Now; //Ngày liên hệ
    }
}
