using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using APIAD.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIAD.Controllers
{
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        [Route("App")]
        public List<GroupModel> Get()
        {
            return _groupRepository.Groups();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("ManageGroup")]
        public void AddInGroup(string user, [FromBody] List<GroupModel> groups)
        {
            _groupRepository.ManageGroup(user, groups);
        }
    }
}