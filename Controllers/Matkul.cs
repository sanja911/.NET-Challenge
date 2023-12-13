using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetWebApiWithEFCodeFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace Matkul_API.Controllers;

[Route("api/linq-query")]
[ApiController]
public class LinqController : ControllerBase
{
    private readonly StudentGradeDBContext _context;

    public LinqController(StudentGradeDBContext context)
    {
        _context = context;
    }


    public IActionResult GetMaxGrades()
    {
        var gradeModel = _context.Grades;
        var gradeDataList = new List<Grades>{
            new Grades { Id = 1, MhsId = 2, MataKuliah = "DBMS", GradeValue = 80 },
            new Grades { Id = 2, MhsId = 2, MataKuliah = "ALPRO", GradeValue = 90 },
            new Grades { Id = 3, MhsId = 2, MataKuliah = ".NET Core Dev", GradeValue = 50 },
            new Grades { Id = 4, MhsId = 1, MataKuliah = "DBMS", GradeValue = 70 },
            new Grades { Id = 5, MhsId = 1, MataKuliah = "ALPRO", GradeValue = 50 },
            new Grades { Id = 6, MhsId = 1, MataKuliah = ".NET Core Dev", GradeValue = 75 },
            new Grades { Id = 7, MhsId = 3, MataKuliah = "DBMS", GradeValue = 100 },
            new Grades { Id = 8, MhsId = 3, MataKuliah = "ALPRO", GradeValue = 80 },
            new Grades { Id = 9, MhsId = 3, MataKuliah = ".NET Core Dev", GradeValue = 70 },
        };
        var result = gradeDataList
            .GroupBy(g => new { g.Id, g.MhsId, g.MataKuliah })
            .Select(g => new
            {
                Id = g.Key.Id,
                MhsId = g.Key.MhsId,
                MataKuliah = g.Key.MataKuliah,
                Grade = g.Max(x => x.GradeValue)
            })
            .ToList();

        return Ok(result);
    }
}
