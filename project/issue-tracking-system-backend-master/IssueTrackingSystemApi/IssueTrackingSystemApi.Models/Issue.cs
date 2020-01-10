﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models
{
    public class Issue
    {
        /// <summary>
        /// 編號
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 單號
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// 受讓者
        /// </summary>
        public int? AssigneeId { get; set; }

        /// <summary>
        /// 回報者
        /// </summary>
        public int? ReporterId { get; set; }

        /// <summary>
        /// 預計處理時間
        /// </summary>
        public double EstimatedTime { get; set; }

        /// <summary>
        /// 預計開始時間
        /// </summary>
        public DateTime? EstimatedStartTime { get; set; }

        /// <summary>
        /// 預計結束時間
        /// </summary>
        public DateTime? EstimatedEndTime { get; set; }

        /// <summary>
        /// 實際開始時間
        /// </summary>
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 實際結束時間
        /// </summary>
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 解決時間
        /// </summary>
        public DateTime? ResolveTime { get; set; }

        /// <summary>
        /// 種類
        /// </summary>
        public int? KindId { get; set; }

        /// <summary>
        /// 嚴重性
        /// </summary>
        public int? ServerityId { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// 緊急性
        /// </summary>
        public int? UrgencyId { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 建立人
        /// </summary>
        public int? CreateUser { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 最後修改人
        /// </summary>
        public int? ModifyUser { get; set; }

        /// <summary>
        /// 留言
        /// </summary>
        public List<IssueComment> Comments { get; set;}

        /// <summary>
        /// 專案 Id
        /// </summary>
        public int? ProjectId { get; set; }
    }
}
