using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    internal class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly GymSystemDBContext _context;

        public HealthRecordRepository(GymSystemDBContext context)
        {
            _context = context;
        }
        public int AddHealthRecord(HealthRecord healthRecord)
        {
            _context.HealthRecords.Add(healthRecord);
            return _context.SaveChanges();


        }

        public int DeleteHealthRecord(int id)
        {
            var healthRecord = _context.HealthRecords.Find(id);
            if (healthRecord != null)
            {
                _context.HealthRecords.Remove(healthRecord);
                return _context.SaveChanges();
            }
            return 0;

        }

        public IEnumerable<HealthRecord> GetAllHealthRecord()
        {
            return _context.HealthRecords.ToList();

        }

        public HealthRecord? GetHealthRecord(int id)
        {
            return _context.HealthRecords.Find(id);

        }

        public int UpdateHealthRecord(HealthRecord healthRecord)
        {
            var existingHealthRecord = _context.HealthRecords.Find(healthRecord.Id);
            if (existingHealthRecord != null)
            {
                _context.Update(existingHealthRecord);
                return _context.SaveChanges();
            }
            return 0;

        }
    }
}
