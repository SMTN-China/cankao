using MESCloud.Authorization.Users;
using MESCloud.Users.Dto;

namespace MESCloud.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public UserDto UserInfo { get; set; }
    }
}
