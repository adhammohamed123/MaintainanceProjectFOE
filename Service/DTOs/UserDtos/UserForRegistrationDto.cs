using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.UserDtos
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "الاسم الرباعي مطلوب")]
        [MaxLength(100)]
        public string Name { get; init; }
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; init; }
        [Compare(nameof(Password), ErrorMessage = "كلمة المرور غير متطابقة")]
        public string ConfirmPass { get; set; }
        [StringLength(11, MinimumLength = 11)]
        public string? PhoneNumber { get; init; }
        public int DepartmentId { get; set; }
        public ICollection<string>? Roles { get; init; }
    }
}
