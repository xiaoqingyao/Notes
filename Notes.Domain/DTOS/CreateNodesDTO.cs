using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notes.Domain.DTOS
{

    /// <summary>
    /// 创建学习笔记
    /// </summary>
    public class CreateNotesDTO
    {


        /// <summary>
        ///  名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        public string CatalogName { get; set; }


        /// <summary>
        /// 分类代码
        /// </summary>
        [Required]
        public string CatalogCode { get; set; }


        /// <summary>
        /// 年级Id
        /// </summary>
        public string GradeCode { get; set; }



        /// <summary>
        /// 年级名称
        /// </summary>
        public string GradeName { get; set; }

    

        /// <summary>
        /// 学生所在班级Id
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 分区
        /// </summary>
        public NotesComponent Section { get; set; }


        /// <summary>
        /// 页
        /// </summary>
        public NotesComponent Page { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 自建or from d4l
        /// </summary>
        public string Source { get; set; }
    }


    /// <summary>
    /// 笔记组件
    /// </summary>
    public class NotesComponent
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
