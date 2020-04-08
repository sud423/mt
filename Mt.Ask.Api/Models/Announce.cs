using Csp.EF;
using System;

namespace Mt.Ask.Api.Models
{
    public class Announce : Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public byte Status { get; set; }

        public int UserId { get; set; }

        public int Sort { get; set; }

    }
}
