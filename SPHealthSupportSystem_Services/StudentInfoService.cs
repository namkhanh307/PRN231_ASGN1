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
        private readonly TopicRepository _topicRepository;
        public StudentInfoService()
        {
            _topicRepository = new TopicRepository();
        }

        public async Task<List<Topic>> GetAll()
        {
            return _topicRepository.GetAll();
        }
    }
}
