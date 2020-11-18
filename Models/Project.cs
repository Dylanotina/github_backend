using System;

namespace Github_backend.Models
{
    public class Project : IComparable<Project>, IEquatable<Project>
    {
        public long id { get; set; }
        
        public string name { get; set; }

        public string html_url { get; set; }

        public string description { get; set; }
 
        public string  created_at { get; set; }
    
        public string updated_at { get; set; }

        public string homepage {get;set;}

        public string language { get; set; }


        public override string ToString()
        {
            return "Id :" + id + " Name :" + name ;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
        
    // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            Project objasProject = obj as Project;
            if (objasProject == null ) return false;
            else return Equals(objasProject);
        }

        public bool Equals(Project other)
        {
            if(other == null) return false;
            return (this.id.Equals(other.id));
        }

        public int CompareTo(Project compareProject)
        {
            if(compareProject == null)
            return 1;
            else
            return this.id.CompareTo(compareProject.id);
        }

    }
}