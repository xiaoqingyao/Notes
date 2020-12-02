using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notes.Domain.DTOS
{

    /// <summary>
    /// 修改名称的参数
    /// </summary>
    public class UpdatePropDTO
    {

        /// <summary>
        /// 要修改的对象Id
        /// </summary>
        [Required]
        public Guid Id { get; set; }


        /// <summary>
        /// 要修改的名称
        /// </summary>
        [Required]
        public string Body { get; set; }
    }
}
