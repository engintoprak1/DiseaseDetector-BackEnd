using Business.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ReportManager : IReportService
    {
        const string SimulationReport = "Lorem ipsum dolor sit amet consectetur adipisicing elit. At voluptates enim fuga ipsa alias officiis tenetur iusto, recusandae aperiam nulla velit adipisci, mollitia, accusamus ad facere. Vel accusantium voluptatem tempora.    Tempora repellendus nisi esse ea dicta quam, delectus perferendis expedita dolorem voluptatum velit quisquam corporis amet, ipsa fuga dolores iusto pariatur voluptatem quasi sequi, labore eligendi at? Et, asperiores deserunt!";

        public IDataResult<string> GetSimulationReport()
        {
            return new SuccessDataResult<string>(data: SimulationReport);
        }
    }
}
