using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.TourismModels.DBModels
{
    public class SampleData : DropCreateDatabaseIfModelChanges<TourismEntities>
    {
        protected override void Seed(TourismEntities context)
        {
            base.Seed(context);
        }
    }
}
