namespace Github_backend.Dtos
{
    public class ProjectsCreateDto
    {
        public long id { get; set; }
        
        public string name { get; set; }

        public string html_url { get; set; }

        public string description { get; set; }
        public string homepage {get;set;}
 
        public string  created_at { get; set; }
    
        public string updated_at { get; set; }

        public string language { get; set; }
    
    }
}