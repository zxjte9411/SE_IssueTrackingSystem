using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models
{
    public class User
    {
        /// <summary>
        /// �s��
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// �b��
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// �K�X
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// �q�l�H�c
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// �v��
        /// </summary>
        public int? CharactorId { get; set; }

        /// <summary>
        /// �W��
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// LineId
        /// </summary>
        public string LineId { get; set; }
    }
}