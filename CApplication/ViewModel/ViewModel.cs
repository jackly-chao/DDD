using System.ComponentModel.DataAnnotations;

namespace CApplication.ViewModel
{
    /// <summary>
    /// 视图模型(DTO)
    /// </summary>
    public abstract class ViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
