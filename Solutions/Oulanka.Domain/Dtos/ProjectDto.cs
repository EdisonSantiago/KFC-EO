using System;

namespace Oulanka.Domain.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdateBy { get; set; }
        public short Status { get; set; }
    }
}