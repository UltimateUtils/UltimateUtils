using HelloUltimateUtils;
using UltimateUtils.EF.Pagination;
using UltimateUtils.Extensions;
using UltimateUtils.Pagination;

Console.WriteLine("Hello, UltimateUtils!");

var x = GetNumbers();
var y = GetStudents();

return;

IEnumerable<int> GetNumbers()
{
    yield return 1;
    yield return 2;
    yield return 3;
    yield return 4;
}

IEnumerable<Student> GetStudents()
{
    yield return new Student(1, "John", "Doe");
    yield return new Student(2, "Jane", "Doe");
    yield return new Student(3, "Tom", "Cruise");
}
