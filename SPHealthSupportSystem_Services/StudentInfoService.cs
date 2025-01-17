using SPHealthSupportSystem_Repositories;
using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public class StudentInfoService
    {
        private readonly StudentInfoRepository _studentInfoRepository;
        public StudentInfoService()
        {
            _studentInfoRepository = new StudentInfoRepository();
        }

        public async Task<List<StudentInfo>> GetAll()
        {
            return _studentInfoRepository.GetAll();
        }
    }
}
