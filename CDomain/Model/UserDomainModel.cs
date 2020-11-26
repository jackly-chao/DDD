namespace CDomain.Model
{
    /// <summary>
    /// 用户领域模型
    /// </summary>
    public class UserDomainModel : AggregateRoot
    {
        public UserDomainModel(int id, string userName, string password, string mobile, string nickName, string avatarPath)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Mobile = mobile;
            NickName = nickName;
            AvatarPath = avatarPath;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; private set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; private set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string AvatarPath { get; private set; }
    }
}
