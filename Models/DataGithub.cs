using System.ComponentModel.DataAnnotations;

namespace Github_backend.Models
{
    public class DataGithub
    {
        
        
        public long id { get; set; }
        [Required]
        public long project_id { get; set; }
        
        [Required]
        public string  name { get; set; }
        
        [Required]
        public string html_url { get; set; }
        public string  description { get; set; }
        public string created_at { get; set; }
    }
}