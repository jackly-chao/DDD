using FluentValidation;

namespace BaseDomain.Command.Model.Validation
{
    /// <summary>
    /// 泛型命令模型验证抽象类
    /// </summary>
    /// <typeparam name="TCommandModel"></typeparam>
    public abstract class CommandModelValidationCore<TCommandModel> : AbstractValidator<TCommandModel> where TCommandModel : CommandModelCore<object>
    {
        //protected void ValidateMessageType()
        //{
        //    RuleFor(d => d.MessageType)
        //        .NotEmpty().WithMessage("信息类型不能为空");//判断不能为空，如果为空则显示Message
        //}
    }
}
