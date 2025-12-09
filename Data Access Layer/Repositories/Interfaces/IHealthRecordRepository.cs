using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    internal interface IHealthRecordRepository
    {
        // Get all HealthRecords
        IEnumerable<HealthRecord> GetAllHealthRecord();

        //  Get HealthRecord by id
        HealthRecord? GetHealthRecord(int id);

        //  Add HealthRecord
        int AddHealthRecord(HealthRecord healthRecord);

        //  Update HealthRecord
        int UpdateHealthRecord(HealthRecord healthRecord);

        //  Delete HealthRecord
        int DeleteHealthRecord(int id);
    }
}
