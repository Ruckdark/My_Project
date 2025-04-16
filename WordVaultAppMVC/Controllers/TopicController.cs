using System;
using System.Collections.Generic;
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Controllers
{
    public class TopicController
    {
        private readonly TopicRepository _topicRepository;

        public TopicController()
        {
            _topicRepository = new TopicRepository();
        }

        // Lấy danh sách chủ đề
        public List<Topic> GetTopics()
        {
            return _topicRepository.GetAllTopics();
        }

        // Thêm một chủ đề mới
        public void AddTopic(string topicName)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException("Tên chủ đề không được để trống.");
            }

            _topicRepository.AddTopic(topicName);
        }
    }
}
