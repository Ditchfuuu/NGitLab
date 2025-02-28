﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NGitLab.Impl.Json;

namespace NGitLab.Models
{
    public class IssueCreate
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id;

        [Required]
        [JsonPropertyName("title")]
        public string Title;

        [JsonPropertyName("description")]
        public string Description;

        [JsonPropertyName("assignee_id")]
        public int? AssigneeId;

        [JsonPropertyName("milestone_id")]
        public int? MileStoneId;

        [JsonPropertyName("labels")]
        public string Labels;

        [JsonPropertyName("confidential")]
        public bool Confidential;

        [JsonPropertyName("due_date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? DueDate;
    }
}
