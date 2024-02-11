#nullable disable
namespace AirStock.Common.Models
{
    public class UserPermissionModel
    {
        public string GroupName { get; set; }
        public string ComponentName { get; set; }
        public string ComponentShortName { get; set; }
        public string PermissionName { get; set; }
        public bool IsGranted { get { return PermissionName == "Grant"; } }
        public int ComponentId { get; set; }
        public string TenantName { get; set; }
    }
    public class GroupUserModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantPrefix { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public string UserLoginId { get; set; }
        public string UserFullName { get; set; }
        public string CurrentUserLoginId { get; set; }
    }
}
