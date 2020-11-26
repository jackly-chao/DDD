using FluentValidation;

namespace CDomain.Command.Model.Validation
{
    /// <summary>
    /// 泛型用户命令模型验证类
    /// </summary>
    /// <typeparam name="TUserCommandModel">泛型用户命令模型</typeparam>
    public class UserCommandModelValidation<TUserCommandModel> : AbstractValidator<TUserCommandModel> where TUserCommandModel: UserCommandModel
    {
        /// <summary>
        /// 验证姓名
        /// </summary>
        protected void ValidateName()
        {
            RuleFor(d => d.UserName)
                .NotEmpty().WithMessage("姓名不能为空")//判断不能为空，如果为空则显示Message
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");//定义 Name 的长度
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        protected void ValidateMobile()
        {
            RuleFor(d => d.Mobile)
                .NotEmpty().WithMessage("手机号不能为空！")
                .Must(HaveMobile).WithMessage("手机号应该为11位！");
        }

        /// <summary>
        /// 手机号验证表达式
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        protected static bool HaveMobile(string mobile)
        {
            return mobile.Length == 11;
        }
    }

    /// <summary>
    /// 用户注册命令模型验证
    /// </summary>
    public class UserRegisterCommandModelValidation : UserCommandModelValidation<UserRegisterCommandModel>
    {
        public UserRegisterCommandModelValidation()
        {
            ValidateName();
            ValidateMobile();
        }
    }

    /// <summary>
    /// 用户登录命令模型验证
    /// </summary>
    public class UserLoginCommandModelValidation : UserCommandModelValidation<UserLoginCommandModel>
    {
        public UserLoginCommandModelValidation()
        {
            ValidateName();
            ValidateMobile();
        }
    }
}
