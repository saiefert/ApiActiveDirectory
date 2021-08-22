using System.ComponentModel;

namespace Model
{
    public class GroupModel
    {
        public GroupModel()
        {
        }

        public GroupModel(string groupName, string description, bool onGroup)
        {
            GroupName = groupName;
            Description = description;
            OnGroup = onGroup;
        }

        public string GroupName { get; set; }
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool OnGroup { get; set; }
    }
}