using ExamsDbDataEtl.Abstractions;
using ExamsDbDataEtl.Models;
using ExamsDbDataEtl.Models.Extractors;

namespace ExamsDbDataEtl.Transformers;

public class ProgramTransformProcess : IAsyncTransformProcess<MajorCsvModel, Models.Program>
{
    public async Task<IList<Models.Program>> TransformAsync(IList<MajorCsvModel> majors)
    {
        // Keeps track of program names
        Dictionary<string, Models.Program> programs = new();

        foreach (var major in majors)
        {
            Models.Program program;

            if (!programs.TryGetValue(major.Category, out program!))
            {
                program = new Models.Program { Name = major.Category };
                programs.Add(major.Category, program);
            }

            program.Courses.Add(new Course { Name = major.Name });
        }

        return await Task.FromResult(programs.Values.ToList());
    }
}
