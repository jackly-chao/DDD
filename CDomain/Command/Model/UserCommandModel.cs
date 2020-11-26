using CDomain.Command.Model.Validation;

namespace CDomain.Command.Model
{
    /// <summary>
    /// 用户命令模型
    /// </summary>
    public abstract class UserCommandModel : CommandModel
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
    /// 用户注册命令模型
    /// </summary>
    public class UserRegisterCommandModel : UserCommandModel
    {
        public UserRegisterCommandModel(int id, string userName, string password, string moblie, string nickName, string avatarPath)
        {
            AggregateId = id;
            Id = id;
            UserName = userName;
            Password = password;
            Mobile = moblie;
            NickName = nickName;
            AvatarPath = avatarPath;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserRegisterCommandModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    /// <summary>
    /// 用户登录命令模型
    /// </summary>
    public class UserLoginCommandModel : UserCommandModel
    {
        public UserLoginCommandModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserLoginCommandModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    /// <summary>
    /// 用户退出命令模型
    /// </summary>
    public class UserLogoutCommandModel : UserCommandModel
    {
        public UserLogoutCommandModel(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
