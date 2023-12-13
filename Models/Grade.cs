using System.ComponentModel.DataAnnotations;

namespace DotnetWebApiWithEFCodeFirst.Models
{
    public class Grades
    {
        public int Id { get; set; }
        public int MhsId { get; set; }
        public string MataKuliah { get; set; } = "";
        public int GradeValue { get; set; }
    }
}