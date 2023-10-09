using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class ContactsDTO
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = "";

        [Required, MaxLength(100)]
        public string LastName { get; set; } = "";

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(100)]
        public string? Phone { get; set; } //Sđt có thể rỗng -> không cần khởi tạo

        //[Required, MaxLength(100)]
        public int SubjectID { get; set; } //Mục đích của cuộc liên hệ tới cửa hàng. Ex: "Hỗ trợ kỹ thuật", "Phản hồi sản phẩm"

        [Required,MinLength(20), MaxLength(4000)]
        public string Message { get; set; } = ""; //Thông tin chi tiết về cuộc liên hệ tới cửa hàng
    }
}
