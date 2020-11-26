namespace CDomain.Event.Model
{
    /// <summary>
    /// 用户事件模型
    /// </summary>
    public abstract class UserEventModel : EventModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; protected set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; protected set; }
    }

    /// <summary>
    /// 用户注册事件模型
    /// </summary>
    public class UserRegisteredEventModel : UserEventModel
    {
        public UserRegisteredEventModel(int id, string userName, string password, string moblie, string nickName, string avatarPath)
        {
            AggregateId = id;
            Id = id;
            UserName = userName;
            Password = password;
            Mobile = moblie;
            NickName = nickName;
            AvatarPath = avatarPath;
        }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; protected set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; protected set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string AvatarPath { get; protected set; }
    }

    /// <summary>
    /// 用户登录事件模型
    /// </summary>
    public class UserLoginedEventModel : UserEventModel
    {
        public UserLoginedEventModel(int id, string userName, string password)
        {
            AggregateId = id;
            Id = id;
            UserName = userName;
            Password = password;
        }
    }

    /// <summary>
    /// 用户退出事件模型
    /// </summary>
    public class UserLogoutedEventModel : UserEventModel
    {
        public UserLogoutedEventModel(int id, string userName)
        {
            AggregateId = id;
            Id = id;
            UserName = userName;
        }
    }
}
