using Core.Enums;

namespace Application.DTOs.Signatures
{
    public class UsersSignature
    {
        public int? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }

        public string? UserName { get; set; }
        public string? Email { get; set; }
        public UserType? UserType { get; set; }
    }
}
